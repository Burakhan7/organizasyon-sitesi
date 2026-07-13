using System.ComponentModel.DataAnnotations;

namespace OrganizasyonSitesi.Models.ViewModels;

public class HizmetFormViewModel
{
	public int Id { get; set; }

	[Required(ErrorMessage = "Başlık zorunlu.")]
	[MaxLength(150)]
	[Display(Name = "Başlık")]
	public string Baslik { get; set; } = string.Empty;

	[Required(ErrorMessage = "Kısa açıklama zorunlu.")]
	[MaxLength(1000)]
	[Display(Name = "Kısa Açıklama (ana sayfa kartı)")]
	public string Aciklama { get; set; } = string.Empty;

	[MaxLength(4000)]
	[Display(Name = "Detay Açıklama (hizmet sayfası, opsiyonel)")]
	public string? DetayAciklama { get; set; }

	[Display(Name = "Sıra No")]
	[Range(1, 999, ErrorMessage = "1-999 arası bir sıra girin.")]
	public int SiraNo { get; set; } = 1;

	[Display(Name = "Aktif (sitede görünsün)")]
	public bool AktifMi { get; set; } = true;
}