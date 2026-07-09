using Microsoft.EntityFrameworkCore;
using OrganizasyonSitesi.Data;
using OrganizasyonSitesi.Models.Entities;

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
}