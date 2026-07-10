using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrganizasyonSitesi.Migrations
{
    /// <inheritdoc />
    public partial class EtkinlikSemasiEklendi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DetayAciklama",
                table: "Hizmetler",
                type: "nvarchar(4000)",
                maxLength: 4000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Hizmetler",
                type: "nvarchar(160)",
                maxLength: 160,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Etkinlikler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Baslik = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(160)", maxLength: 160, nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    Mekan = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Tarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    YayindaMi = table.Column<bool>(type: "bit", nullable: false),
                    HizmetId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Etkinlikler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Etkinlikler_Hizmetler_HizmetId",
                        column: x => x.HizmetId,
                        principalTable: "Hizmetler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EtkinlikFotograflari",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DosyaYolu = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    AltMetin = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    KapakMi = table.Column<bool>(type: "bit", nullable: false),
                    SiraNo = table.Column<int>(type: "int", nullable: false),
                    EtkinlikId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EtkinlikFotograflari", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EtkinlikFotograflari_Etkinlikler_EtkinlikId",
                        column: x => x.EtkinlikId,
                        principalTable: "Etkinlikler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Hizmetler",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DetayAciklama", "Slug" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Hizmetler",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DetayAciklama", "Slug" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Hizmetler",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DetayAciklama", "Slug" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Hizmetler",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DetayAciklama", "Slug" },
                values: new object[] { null, null });

            migrationBuilder.CreateIndex(
                name: "IX_EtkinlikFotograflari_EtkinlikId",
                table: "EtkinlikFotograflari",
                column: "EtkinlikId");

            migrationBuilder.CreateIndex(
                name: "IX_Etkinlikler_HizmetId",
                table: "Etkinlikler",
                column: "HizmetId");

            migrationBuilder.CreateIndex(
                name: "IX_Etkinlikler_Slug",
                table: "Etkinlikler",
                column: "Slug",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EtkinlikFotograflari");

            migrationBuilder.DropTable(
                name: "Etkinlikler");

            migrationBuilder.DropColumn(
                name: "DetayAciklama",
                table: "Hizmetler");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Hizmetler");
        }
    }
}
