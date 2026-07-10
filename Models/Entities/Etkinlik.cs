using System.ComponentModel.DataAnnotations;

namespace OrganizasyonSitesi.Models.Entities;

public class Etkinlik
{
	public int Id { get; set; }

	[Required, MaxLength(150)]
	public string Baslik { get; set; } = string.Empty;

	[Required, MaxLength(160)]
	public string Slug { get; set; } = string.Empty;

	[MaxLength(2000)]
	public string? Aciklama { get; set; }

	[MaxLength(150)]
	public string? Mekan { get; set; }

	public DateTime Tarih { get; set; }

	public bool YayindaMi { get; set; } = false;

	// --- İlişkiler ---
	public int HizmetId { get; set; }              // FK: her etkinlik bir hizmet türüne ait
	public Hizmet? Hizmet { get; set; }            // navigation property

	public List<EtkinlikFotograf> Fotograflar { get; set; } = new();  // 1-N ilişkinin "N" tarafı
}