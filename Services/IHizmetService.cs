using OrganizasyonSitesi.Models.Entities;
using OrganizasyonSitesi.Models.ViewModels;

namespace OrganizasyonSitesi.Services;

public interface IHizmetService
{
    Task<List<Hizmet>> AktifHizmetleriGetirAsync();
    Task<List<Referans>> AktifReferanslariGetirAsync();
    Task IletisimMesajiKaydetAsync(IletisimMesaji mesaj);
    Task<PanelOzetViewModel> PanelOzetGetirAsync();
    Task<List<IletisimMesaji>> MesajlariGetirAsync();
    Task OkunduIsaretleAsync(int mesajId, bool okundu);
}