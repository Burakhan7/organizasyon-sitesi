using System.ComponentModel.DataAnnotations;

namespace OrganizasyonSitesi.Models.ViewModels;

public class EtkinlikFormViewModel
{
    public int Id { get; set; }   // 0 = yeni kayıt, >0 = düzenleme

    [Required(ErrorMessage = "Başlık zorunlu.")]
    [MaxLength(150)]
    [Display(Name = "Başlık")]
    public string Baslik { get; set; } = string.Empty;

    [MaxLength(2000)]
    [Display(Name = "Açıklama")]
    public string? Aciklama { get; set; }

    [MaxLength(150)]
    [Display(Name = "Mekan")]
    public string? Mekan { get; set; }

    [Required(ErrorMessage = "Tarih zorunlu.")]
    [Display(Name = "Etkinlik Tarihi")]
    [DataType(DataType.Date)]
    public DateTime Tarih { get; set; } = DateTime.Today;

    [Required(ErrorMessage = "Hizmet türü seçin.")]
    [Display(Name = "Hizmet Türü")]
    public int HizmetId { get; set; }

    [Display(Name = "Yayında")]
    public bool YayindaMi { get; set; }
}