using System.ComponentModel.DataAnnotations;

namespace OrganizasyonSitesi.Models.Entities;

public class Referans
{
    public int Id { get; set; }

    [Required, MaxLength(150)]
    public string MusteriAdi { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? Yorum { get; set; }

    [MaxLength(300)]
    public string? LogoUrl { get; set; }

    public bool AktifMi { get; set; } = true;
}