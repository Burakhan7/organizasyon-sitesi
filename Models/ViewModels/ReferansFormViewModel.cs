namespace OrganizasyonSitesi.Models.ViewModels;
using System.ComponentModel.DataAnnotations;

public class ReferansFormViewModel
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Müşteri adı zorunlu.")]
    [MaxLength(150)]
    [Display(Name = "Müşteri Adı")]
    public string MusteriAdi { get; set; } = string.Empty;

    [MaxLength(500)]
    [Display(Name = "Yorum")]
    public string? Yorum { get; set; }
    [Display(Name = "Aktif (sitede görünsün)")]
    public bool AktifMi { get; set; } = true;

}