using OrganizasyonSitesi.Models.Entities;

namespace OrganizasyonSitesi.Models.ViewModels;

public class AnasayfaViewModel
{
    public List<Hizmet> Hizmetler { get; set; } = new();
    public List<Referans> Referanslar { get; set; } = new();
}