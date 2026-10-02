using KisiselHarcamaTakip.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace KisiselHarcamaTakip.Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Islem> Islemler => Set<Islem>();

    public DbSet<Kategori> Kategoriler => Set<Kategori>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Islem>(entity =>
        {
            entity.Property(islem => islem.Aciklama).HasMaxLength(200).IsRequired();
            entity.Property(islem => islem.Tutar).HasPrecision(18, 2);
            entity.Property(islem => islem.Tur).HasConversion<int>();
            entity.HasOne(islem => islem.Kategori)
                .WithMany(kategori => kategori.Islemler)
                .HasForeignKey(islem => islem.KategoriId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<Kategori>(entity =>
        {
            entity.Property(kategori => kategori.Ad).HasMaxLength(100).IsRequired();
            entity.HasIndex(kategori => kategori.Ad).IsUnique();
        });
    }
}
