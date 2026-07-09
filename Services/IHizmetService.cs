using OrganizasyonSitesi.Models.Entities;

namespace OrganizasyonSitesi.Services;

public interface IHizmetService
{
    Task<List<Hizmet>> AktifHizmetleriGetirAsync();
    Task<List<Referans>> AktifReferanslariGetirAsync();
    Task IletisimMesajiKaydetAsync(IletisimMesaji mesaj);
}