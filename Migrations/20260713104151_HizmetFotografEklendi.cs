using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrganizasyonSitesi.Migrations
{
    /// <inheritdoc />
    public partial class HizmetFotografEklendi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HizmetFotograflari",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DosyaYolu = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    AltMetin = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SiraNo = table.Column<int>(type: "int", nullable: false),
                    HizmetId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HizmetFotograflari", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HizmetFotograflari_Hizmetler_HizmetId",
                        column: x => x.HizmetId,
                        principalTable: "Hizmetler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HizmetFotograflari_HizmetId",
                table: "HizmetFotograflari",
                column: "HizmetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HizmetFotograflari");
        }
    }
}
