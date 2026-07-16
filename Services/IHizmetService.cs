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
    Task<List<Hizmet>> TumHizmetleriGetirAsync();          // admin: pasifler dahil
    Task<Hizmet?> HizmetGetirAsync(int id);
    Task HizmetEkleAsync(HizmetFormViewModel form);
    Task HizmetGuncelleAsync(HizmetFormViewModel form);
    Task<(bool basarili, string? hata)> HizmetSilAsync(int id);
    Task<Hizmet?> SlugIleHizmetGetirAsync(string slug);
    Task HizmetFotografSilAsync(int fotografId);
    Task <List<Referans>>TumReferanslariGetirAsync();
    Task <Referans?>ReferansGetirAsync(int id);
    Task ReferansEkleAsync(ReferansFormViewModel form);
    Task ReferansGuncelleAsync(ReferansFormViewModel form);
    Task ReferansSilAsync(int id);



}