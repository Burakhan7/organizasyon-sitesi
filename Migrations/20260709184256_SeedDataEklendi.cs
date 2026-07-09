using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OrganizasyonSitesi.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataEklendi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Hizmetler",
                columns: new[] { "Id", "Aciklama", "AktifMi", "Baslik", "GorselUrl", "SiraNo" },
                values: new object[,]
                {
                    { 1, "Lansman, bayi toplantısı, gala gecesi ve şirket organizasyonlarında uçtan uca planlama.", true, "Kurumsal Etkinlikler", null, 1 },
                    { 2, "Nişandan kına gecesine, salon süslemesinden orkestraya hayalinizdeki düğünü kurgular, siz sadece anın tadını çıkarırsınız.", true, "Düğün Organizasyonu", null, 2 },
                    { 3, "Sahne kurulumu, ses-ışık sistemleri, sanatçı yönetimi ve güvenlik koordinasyonu dahil büyük ölçekli etkinlik yönetimi.", true, "Festival ve Konser", null, 3 },
                    { 4, "Mağaza açılışı, ürün lansmanı ve basın etkinliklerinde markanızı en doğru şekilde sahneye koyuyoruz.", true, "Açılış ve Lansman", null, 4 }
                });

            migrationBuilder.InsertData(
                table: "Referanslar",
                columns: new[] { "Id", "AktifMi", "LogoUrl", "MusteriAdi", "Yorum" },
                values: new object[,]
                {
                    { 1, true, null, "Yılmaz Holding", "Bayi toplantımız kusursuz geçti, her detay düşünülmüştü." },
                    { 2, true, null, "Elif & Mert", "Düğünümüz hayal ettiğimizden de güzeldi, iyi ki sizi seçmişiz!" },
                    { 3, true, null, "TechNova Yazılım", "Ürün lansmanımızda basın ve konuk yönetimi profesyonelceydi." }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Hizmetler",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Hizmetler",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Hizmetler",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Hizmetler",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Referanslar",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Referanslar",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Referanslar",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
