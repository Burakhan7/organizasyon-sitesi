namespace OrganizasyonSitesi.Services;

public static class FotografYardimcisi
{
    private static readonly string[] IzinliUzantilar = { ".jpg", ".jpeg", ".png", ".webp" };
    private const long MaksBoyut = 5 * 1024 * 1024;

    /// <summary>Dosyayı doğrular ve diske kaydeder. Başarılıysa web yolunu, değilse hata mesajını döner.</summary>
    public static async Task<(string? webYolu, string? hata)> KaydetAsync(
        IFormFile dosya, string webRootPath, string altKlasor)
    {
        var uzanti = Path.GetExtension(dosya.FileName).ToLowerInvariant();
        if (uzanti == ".jpeg") uzanti = ".jpg";

        if (!IzinliUzantilar.Contains(uzanti))
            return (null, $"{dosya.FileName}: izin verilmeyen dosya türü.");

        if (dosya.Length == 0 || dosya.Length > MaksBoyut)
            return (null, $"{dosya.FileName}: dosya boş veya 5 MB'den büyük.");

        var klasor = Path.Combine(webRootPath, "uploads", altKlasor);
        Directory.CreateDirectory(klasor);

        var yeniAd = $"{Guid.NewGuid():N}{uzanti}";
        using (var stream = new FileStream(Path.Combine(klasor, yeniAd), FileMode.Create))
        {
            await dosya.CopyToAsync(stream);
        }

        return ($"/uploads/{altKlasor.Replace('\\', '/')}/{yeniAd}", null);
    }

    public static void Sil(string webYolu, string webRootPath)
    {
        var fizikselYol = Path.Combine(webRootPath,
            webYolu.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
        if (File.Exists(fizikselYol))
            File.Delete(fizikselYol);
    }
}