using OrganizasyonSitesi.Models.Entities;
using OrganizasyonSitesi.Models.ViewModels;

namespace OrganizasyonSitesi.Services;

public interface IEtkinlikService
{
    Task<List<Etkinlik>> TumunuGetirAsync();
    Task<Etkinlik?> GetirAsync(int id);
    Task EkleAsync(EtkinlikFormViewModel form);
    Task GuncelleAsync(EtkinlikFormViewModel form);
    Task SilAsync(int id);
    Task<List<Etkinlik>> YayindakileriGetirAsync();
    Task<Etkinlik?> SlugIleGetirAsync(string slug);
}