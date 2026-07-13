using System.ComponentModel.DataAnnotations;

namespace OrganizasyonSitesi.Models.Entities;

public class HizmetFotograf
{
    public int Id { get; set; }

    [Required, MaxLength(300)]
    public string DosyaYolu { get; set; } = string.Empty;

    [MaxLength(200)]
    public string? AltMetin { get; set; }

    public int SiraNo { get; set; }

    public int HizmetId { get; set; }
    public Hizmet? Hizmet { get; set; }
}