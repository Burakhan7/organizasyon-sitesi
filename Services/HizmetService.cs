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
}