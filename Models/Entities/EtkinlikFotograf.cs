using System.ComponentModel.DataAnnotations;

namespace OrganizasyonSitesi.Models.Entities;

public class EtkinlikFotograf
{
    public int Id { get; set; }

    [Required, MaxLength(300)]
    public string DosyaYolu { get; set; } = string.Empty;

    [MaxLength(200)]
    public string? AltMetin { get; set; }

    public bool KapakMi { get; set; } = false;

    public int SiraNo { get; set; }

    // --- İlişki ---
    public int EtkinlikId { get; set; }
    public Etkinlik? Etkinlik { get; set; }
}