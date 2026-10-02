using KisiselHarcamaTakip.Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KisiselHarcamaTakip.Infrastructure.Migrations;

[DbContext(typeof(ApplicationDbContext))]
[Migration("20260930130000_InitialCreate")]
public partial class InitialCreate : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Kategoriler",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                Ad = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
            },
            constraints: table => table.PrimaryKey("PK_Kategoriler", x => x.Id));

        migrationBuilder.CreateTable(
            name: "Islemler",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                Aciklama = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                Tutar = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                Tarih = table.Column<DateTime>(type: "TEXT", nullable: false),
                Tur = table.Column<int>(type: "INTEGER", nullable: false),
                KategoriId = table.Column<int>(type: "INTEGER", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Islemler", x => x.Id);
                table.ForeignKey(
                    name: "FK_Islemler_Kategoriler_KategoriId",
                    column: x => x.KategoriId,
                    principalTable: "Kategoriler",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.SetNull);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Islemler_KategoriId",
            table: "Islemler",
            column: "KategoriId");

        migrationBuilder.CreateIndex(
            name: "IX_Kategoriler_Ad",
            table: "Kategoriler",
            column: "Ad",
            unique: true);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "Islemler");
        migrationBuilder.DropTable(name: "Kategoriler");
    }
}
