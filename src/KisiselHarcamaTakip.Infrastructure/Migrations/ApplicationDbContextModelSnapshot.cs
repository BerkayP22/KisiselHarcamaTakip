using KisiselHarcamaTakip.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace KisiselHarcamaTakip.Infrastructure.Migrations;

[DbContext(typeof(ApplicationDbContext))]
public partial class ApplicationDbContextModelSnapshot : ModelSnapshot
{
    protected override void BuildModel(ModelBuilder modelBuilder)
    {
#pragma warning disable 612, 618
        modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

        modelBuilder.Entity("KisiselHarcamaTakip.Core.Entities.Kategori", entity =>
        {
            entity.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("INTEGER").HasAnnotation("Sqlite:Autoincrement", true);
            entity.Property<string>("Ad").IsRequired().HasMaxLength(100).HasColumnType("TEXT");
            entity.HasKey("Id");
            entity.HasIndex("Ad").IsUnique();
            entity.ToTable("Kategoriler");
        });

        modelBuilder.Entity("KisiselHarcamaTakip.Core.Entities.Islem", entity =>
        {
            entity.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("INTEGER").HasAnnotation("Sqlite:Autoincrement", true);
            entity.Property<string>("Aciklama").IsRequired().HasMaxLength(200).HasColumnType("TEXT");
            entity.Property<int?>("KategoriId").HasColumnType("INTEGER");
            entity.Property<DateTime>("Tarih").HasColumnType("TEXT");
            entity.Property<decimal>("Tutar").HasPrecision(18, 2).HasColumnType("TEXT");
            entity.Property<int>("Tur").HasColumnType("INTEGER");
            entity.HasKey("Id");
            entity.HasIndex("KategoriId");
            entity.ToTable("Islemler");
        });

        modelBuilder.Entity("KisiselHarcamaTakip.Core.Entities.Islem", entity =>
        {
            entity.HasOne("KisiselHarcamaTakip.Core.Entities.Kategori", "Kategori")
                .WithMany("Islemler")
                .HasForeignKey("KategoriId")
                .OnDelete(DeleteBehavior.SetNull);
            entity.Navigation("Kategori");
        });

        modelBuilder.Entity("KisiselHarcamaTakip.Core.Entities.Kategori", entity =>
        {
            entity.Navigation("Islemler");
        });
#pragma warning restore 612, 618
    }
}
