using System.ComponentModel.DataAnnotations;

namespace OrganizasyonSitesi.Models.Entities;

public class IletisimMesaji
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string AdSoyad { get; set; } = string.Empty;

    [Required, MaxLength(150), EmailAddress]
    public string Email { get; set; } = string.Empty;

    [MaxLength(20)]
    public string? Telefon { get; set; }

    [MaxLength(100)]
    public string? EtkinlikTuru { get; set; }

    [Required, MaxLength(2000)]
    public string Mesaj { get; set; } = string.Empty;

    public DateTime KayitTarihi { get; set; } = DateTime.UtcNow;

    public bool OkunduMu { get; set; } = false;
}