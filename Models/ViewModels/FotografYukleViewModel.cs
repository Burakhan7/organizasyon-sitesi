using System.ComponentModel.DataAnnotations;

namespace OrganizasyonSitesi.Models.ViewModels;

public class FotografYukleViewModel
{
    public int EtkinlikId { get; set; }

    [Required(ErrorMessage = "En az bir fotoğraf seçin.")]
    [Display(Name = "Fotoğraflar")]
    public List<IFormFile> Dosyalar { get; set; } = new();

    [MaxLength(200)]
    [Display(Name = "Alt Metin (tümüne uygulanır, opsiyonel)")]
    public string? AltMetin { get; set; }
}