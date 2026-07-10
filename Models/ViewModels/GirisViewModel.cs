using System.ComponentModel.DataAnnotations;

namespace OrganizasyonSitesi.Models.ViewModels;

public class GirisViewModel
{
    [Required(ErrorMessage = "E-posta zorunlu.")]
    [EmailAddress(ErrorMessage = "Geçerli bir e-posta girin.")]
    [Display(Name = "E-posta")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Şifre zorunlu.")]
    [DataType(DataType.Password)]
    [Display(Name = "Şifre")]
    public string Sifre { get; set; } = string.Empty;

    [Display(Name = "Beni hatırla")]
    public bool BeniHatirla { get; set; }
}