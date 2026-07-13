using Microsoft.EntityFrameworkCore;
using OrganizasyonSitesi.Data;
using OrganizasyonSitesi.Models.Entities;
using OrganizasyonSitesi.Models.ViewModels;

namespace OrganizasyonSitesi.Services;

public class HizmetService : IHizmetService
{
    private readonly AppDbContext _context;

    public HizmetService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Hizmet>> AktifHizmetleriGetirAsync()
    {
        return await _context.Hizmetler
            .Where(h => h.AktifMi)
            .OrderBy(h => h.SiraNo)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<List<Referans>> AktifReferanslariGetirAsync()
    {
        return await _context.Referanslar
            .Where(r => r.AktifMi)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task IletisimMesajiKaydetAsync(IletisimMesaji mesaj)
    {
        _context.IletisimMesajlari.Add(mesaj);
        await _context.SaveChangesAsync();
    }

    public async Task<PanelOzetViewModel> PanelOzetGetirAsync()
    {
        return new PanelOzetViewModel
        {
            ToplamMesaj = await _context.IletisimMesajlari.CountAsync(),
            OkunmamisMesaj = await _context.IletisimMesajlari.CountAsync(m => !m.OkunduMu),
            ToplamEtkinlik = await _context.Etkinlikler.CountAsync(),
            YayindakiEtkinlik = await _context.Etkinlikler.CountAsync(e => e.YayindaMi),
            AktifHizmet = await _context.Hizmetler.CountAsync(h => h.AktifMi),
            AktifReferans = await _context.Referanslar.CountAsync(r => r.AktifMi)
        };
    }

    public async Task<List<IletisimMesaji>> MesajlariGetirAsync()
    {
        return await _context.IletisimMesajlari
            .OrderByDescending(m => m.KayitTarihi)   // en yeni en üstte
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task OkunduIsaretleAsync(int mesajId, bool okundu)
    {
        var mesaj = await _context.IletisimMesajlari.FindAsync(mesajId);
        if (mesaj == null) return;

        mesaj.OkunduMu = okundu;
        await _context.SaveChangesAsync();
    }

    public async Task<List<Hizmet>> TumHizmetleriGetirAsync()
    {
        return await _context.Hizmetler
            .OrderBy(h => h.SiraNo)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Hizmet?> HizmetGetirAsync(int id)
    {
        return await _context.Hizmetler.AsNoTracking().FirstOrDefaultAsync(h => h.Id == id);
    }

    public async Task HizmetEkleAsync(HizmetFormViewModel form)
    {
        var hizmet = new Hizmet
        {
            Baslik = form.Baslik,
            Slug = await BenzersizHizmetSlugAsync(form.Baslik),
            Aciklama = form.Aciklama,
            DetayAciklama = form.DetayAciklama,
            SiraNo = form.SiraNo,
            AktifMi = form.AktifMi
        };

        _context.Hizmetler.Add(hizmet);
        await _context.SaveChangesAsync();
    }

    public async Task HizmetGuncelleAsync(HizmetFormViewModel form)
    {
        var hizmet = await _context.Hizmetler.FindAsync(form.Id);
        if (hizmet == null) return;

        if (hizmet.Baslik != form.Baslik || string.IsNullOrEmpty(hizmet.Slug))
            hizmet.Slug = await BenzersizHizmetSlugAsync(form.Baslik, form.Id);

        hizmet.Baslik = form.Baslik;
        hizmet.Aciklama = form.Aciklama;
        hizmet.DetayAciklama = form.DetayAciklama;
        hizmet.SiraNo = form.SiraNo;
        hizmet.AktifMi = form.AktifMi;

        await _context.SaveChangesAsync();
    }

    public async Task<(bool basarili, string? hata)> HizmetSilAsync(int id)
    {
        var etkinlikSayisi = await _context.Etkinlikler.CountAsync(e => e.HizmetId == id);
        if (etkinlikSayisi > 0)
            return (false, $"Bu hizmete bağlı {etkinlikSayisi} etkinlik var. Önce onları başka hizmete taşıyın ya da silin — veya hizmeti pasife alın.");

        var hizmet = await _context.Hizmetler.FindAsync(id);
        if (hizmet == null) return (false, "Hizmet bulunamadı.");

        _context.Hizmetler.Remove(hizmet);
        await _context.SaveChangesAsync();
        return (true, null);
    }

    private async Task<string> BenzersizHizmetSlugAsync(string baslik, int? haricId = null)
    {
        var slug = SlugYardimcisi.Uret(baslik);
        var aday = slug;
        var sayac = 2;

        while (await _context.Hizmetler.AnyAsync(h => h.Slug == aday && h.Id != haricId))
            aday = $"{slug}-{sayac++}";

        return aday;
    }
}