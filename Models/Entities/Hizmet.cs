using System.ComponentModel.DataAnnotations;

namespace OrganizasyonSitesi.Models.Entities;

public class Hizmet
{
    public int Id { get; set; }

    [Required, MaxLength(150)]
    public string Baslik { get; set; } = string.Empty;

    [Required, MaxLength(1000)]
    public string Aciklama { get; set; } = string.Empty;

    [MaxLength(300)]
    public string? GorselUrl { get; set; }

    public bool AktifMi { get; set; } = true;

    public int SiraNo { get; set; }
}