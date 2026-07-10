using System.ComponentModel.DataAnnotations;

namespace OrganizasyonSitesi.Models.ViewModels;

public class IletisimFormViewModel
{
    [Required(ErrorMessage = "Adınızı girmeniz gerekiyor.")]
    [MaxLength(100)]
    [Display(Name = "Ad Soyad")]
    public string AdSoyad { get; set; } = string.Empty;
     
    [Required(ErrorMessage = "E-posta adresi zorunlu.")]
    [EmailAddress(ErrorMessage = "Geçerli bir e-posta girin.")]
    [MaxLength(150)]
    [Display(Name = "E-posta")]
    public string Email { get; set; } = string.Empty;

    [MaxLength(20)]
    [Display(Name = "Telefon (opsiyonel)")]
    public string? Telefon { get; set; }

    [MaxLength(100)]
    [Display(Name = "Etkinlik Türü")]
    public string? EtkinlikTuru { get; set; }

    [Required(ErrorMessage = "Mesajınızı yazın.")]
    [MaxLength(2000)]
    [Display(Name = "Mesajınız")]
    public string Mesaj { get; set; } = string.Empty;
}