using Microsoft.EntityFrameworkCore;
using OrganizasyonSitesi.Data;
using OrganizasyonSitesi.Models.Entities;
using OrganizasyonSitesi.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;

namespace OrganizasyonSitesi.Services;

public class EtkinlikService : IEtkinlikService
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _env;

    public EtkinlikService(AppDbContext context, IWebHostEnvironment env)
    {
        _context = context;
        _env = env;
    }

    public async Task<List<Etkinlik>> TumunuGetirAsync()
    {
        return await _context.Etkinlikler
            .Include(e => e.Hizmet)                 // ilişkili hizmeti de getir
            .OrderByDescending(e => e.Tarih)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Etkinlik?> GetirAsync(int id)
    {
        return await _context.Etkinlikler
            .Include(e => e.Fotograflar)
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task EkleAsync(EtkinlikFormViewModel form)
    {
        var etkinlik = new Etkinlik
        {
            Baslik = form.Baslik,
            Slug = await BenzersizSlugUretAsync(form.Baslik),
            Aciklama = form.Aciklama,
            Mekan = form.Mekan,
            Tarih = form.Tarih,
            HizmetId = form.HizmetId,
            YayindaMi = form.YayindaMi
        };

        _context.Etkinlikler.Add(etkinlik);
        await _context.SaveChangesAsync();
    }

    public async Task GuncelleAsync(EtkinlikFormViewModel form)
    {
        var etkinlik = await _context.Etkinlikler.FindAsync(form.Id);
        if (etkinlik == null) return;

        // Başlık değiştiyse slug'ı da yenile
        if (etkinlik.Baslik != form.Baslik)
            etkinlik.Slug = await BenzersizSlugUretAsync(form.Baslik, form.Id);

        etkinlik.Baslik = form.Baslik;
        etkinlik.Aciklama = form.Aciklama;
        etkinlik.Mekan = form.Mekan;
        etkinlik.Tarih = form.Tarih;
        etkinlik.HizmetId = form.HizmetId;
        etkinlik.YayindaMi = form.YayindaMi;

        await _context.SaveChangesAsync();
    }

    public async Task SilAsync(int id)
    {
        var etkinlik = await _context.Etkinlikler.FindAsync(id);
        if (etkinlik == null) return;

        _context.Etkinlikler.Remove(etkinlik);
        await _context.SaveChangesAsync();   // cascade: fotoğraf kayıtları da gider
    }

    // --- Yardımcılar ---

    private async Task<string> BenzersizSlugUretAsync(string baslik, int? haricId = null)
    {
        var slug = SlugUret(baslik);
        var aday = slug;
        var sayac = 2;

        // Aynı slug varsa sonuna -2, -3... ekle
        while (await _context.Etkinlikler.AnyAsync(e => e.Slug == aday && e.Id != haricId))
        {
            aday = $"{slug}-{sayac}";
            sayac++;
        }

        return aday;
    }

    private static string SlugUret(string metin)
    {
        var harita = new Dictionary<char, char>
        {
            ['ç'] = 'c',
            ['ğ'] = 'g',
            ['ı'] = 'i',
            ['ö'] = 'o',
            ['ş'] = 's',
            ['ü'] = 'u',
            ['Ç'] = 'c',
            ['Ğ'] = 'g',
            ['İ'] = 'i',
            ['Ö'] = 'o',
            ['Ş'] = 's',
            ['Ü'] = 'u'
        };

        var temiz = new System.Text.StringBuilder();
        foreach (var ch in metin.Trim())
        {
            var c = harita.TryGetValue(ch, out var eslenik) ? eslenik : char.ToLowerInvariant(ch);
            if (char.IsLetterOrDigit(c)) temiz.Append(c);
            else if (c == ' ' || c == '-') temiz.Append('-');
        }

        // Ardışık tireleri tekle indir
        var sonuc = System.Text.RegularExpressions.Regex.Replace(temiz.ToString(), "-{2,}", "-").Trim('-');
        return string.IsNullOrEmpty(sonuc) ? "etkinlik" : sonuc;
    }

    public async Task<List<Etkinlik>> YayindakileriGetirAsync()
    {
        return await _context.Etkinlikler
            .Where(e => e.YayindaMi)                      // vitrin sadece yayındakileri görür
            .Include(e => e.Hizmet)
            .Include(e => e.Fotograflar.Where(f => f.KapakMi))  // sadece kapak fotoğrafı
            .OrderByDescending(e => e.Tarih)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Etkinlik?> SlugIleGetirAsync(string slug)
    {
        return await _context.Etkinlikler
            .Where(e => e.YayindaMi && e.Slug == slug)
            .Include(e => e.Hizmet)
            .Include(e => e.Fotograflar.OrderBy(f => f.SiraNo))
            .AsNoTracking()
            .FirstOrDefaultAsync();
    }

    private static readonly string[] IzinliUzantilar = { ".jpg", ".jpeg", ".png", ".webp" };
    private const long MaksBoyut = 5 * 1024 * 1024;   // 5 MB

    public async Task<(int basarili, List<string> hatalar)> FotografYukleAsync(FotografYukleViewModel form)
    {
        var hatalar = new List<string>();
        var basarili = 0;

        var etkinlik = await _context.Etkinlikler
            .Include(e => e.Fotograflar)
            .FirstOrDefaultAsync(e => e.Id == form.EtkinlikId);

        if (etkinlik == null)
        {
            hatalar.Add("Etkinlik bulunamadı.");
            return (0, hatalar);
        }

        var klasor = Path.Combine(_env.WebRootPath, "uploads", "etkinlikler", etkinlik.Id.ToString());
        Directory.CreateDirectory(klasor);   // yoksa oluşturur, varsa dokunmaz

        var siraNo = etkinlik.Fotograflar.Count > 0 ? etkinlik.Fotograflar.Max(f => f.SiraNo) + 1 : 1;

        foreach (var dosya in form.Dosyalar)
        {
            // --- Validasyon zinciri ---
            var uzanti = Path.GetExtension(dosya.FileName).ToLowerInvariant();

            if (!IzinliUzantilar.Contains(uzanti))
            {
                hatalar.Add($"{dosya.FileName}: izin verilmeyen dosya türü.");
                continue;
            }

            if (dosya.Length == 0 || dosya.Length > MaksBoyut)
            {
                hatalar.Add($"{dosya.FileName}: dosya boş veya 5 MB'den büyük.");
                continue;
            }

            // --- Güvenli dosya adı: kullanıcının adını ASLA kullanma ---
            var yeniAd = $"{Guid.NewGuid():N}{uzanti}";
            var tamYol = Path.Combine(klasor, yeniAd);

            using (var stream = new FileStream(tamYol, FileMode.Create))
            {
                await dosya.CopyToAsync(stream);
            }

            etkinlik.Fotograflar.Add(new EtkinlikFotograf
            {
                DosyaYolu = $"/uploads/etkinlikler/{etkinlik.Id}/{yeniAd}",
                AltMetin = form.AltMetin,
                SiraNo = siraNo++,
                KapakMi = !etkinlik.Fotograflar.Any(f => f.KapakMi) && basarili == 0  // ilk fotoğraf otomatik kapak
            });

            basarili++;
        }

        if (basarili > 0)
            await _context.SaveChangesAsync();

        return (basarili, hatalar);
    }

    public async Task FotografSilAsync(int fotografId)
    {
        var foto = await _context.EtkinlikFotograflari.FindAsync(fotografId);
        if (foto == null) return;

        // Önce diskteki dosyayı sil
        var fizikselYol = Path.Combine(_env.WebRootPath, foto.DosyaYolu.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
        if (File.Exists(fizikselYol))
            File.Delete(fizikselYol);

        _context.EtkinlikFotograflari.Remove(foto);
        await _context.SaveChangesAsync();
    }

    public async Task KapakYapAsync(int fotografId)
    {
        var foto = await _context.EtkinlikFotograflari.FindAsync(fotografId);
        if (foto == null) return;

        // Aynı etkinliğin diğer kapaklarını indir
        var digerleri = await _context.EtkinlikFotograflari
            .Where(f => f.EtkinlikId == foto.EtkinlikId && f.KapakMi)
            .ToListAsync();

        foreach (var d in digerleri)
            d.KapakMi = false;

        foto.KapakMi = true;
        await _context.SaveChangesAsync();
    }
}