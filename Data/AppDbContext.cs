using Microsoft.EntityFrameworkCore;
using OrganizasyonSitesi.Models.Entities;

namespace OrganizasyonSitesi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Hizmet> Hizmetler { get; set; }
    public DbSet<Referans> Referanslar { get; set; }
    public DbSet<IletisimMesaji> IletisimMesajlari { get; set; }
    public DbSet<Etkinlik> Etkinlikler { get; set; }
    public DbSet<EtkinlikFotograf> EtkinlikFotograflari { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Etkinlik silinince fotoğrafları da silinsin (cascade)
        modelBuilder.Entity<EtkinlikFotograf>()
            .HasOne(f => f.Etkinlik)
            .WithMany(e => e.Fotograflar)
            .HasForeignKey(f => f.EtkinlikId)
            .OnDelete(DeleteBehavior.Cascade);

        // Hizmet silinince etkinlikler silinMEsin (restrict): önce etkinlikleri taşı/sil
        modelBuilder.Entity<Etkinlik>()
            .HasOne(e => e.Hizmet)
            .WithMany(h => h.Etkinlikler)
            .HasForeignKey(e => e.HizmetId)
            .OnDelete(DeleteBehavior.Restrict);

        // Slug'lar benzersiz olsun (URL çakışmasın)
        modelBuilder.Entity<Etkinlik>().HasIndex(e => e.Slug).IsUnique();

        modelBuilder.Entity<Hizmet>().HasData(
            new Hizmet { Id = 1, Baslik = "Kurumsal Etkinlikler", Aciklama = "Lansman, bayi toplantısı, gala gecesi ve şirket organizasyonlarında uçtan uca planlama.", SiraNo = 1, AktifMi = true },
            new Hizmet { Id = 2, Baslik = "Düğün Organizasyonu", Aciklama = "Nişandan kına gecesine, salon süslemesinden orkestraya hayalinizdeki düğünü kurgular, siz sadece anın tadını çıkarırsınız.", SiraNo = 2, AktifMi = true },
            new Hizmet { Id = 3, Baslik = "Festival ve Konser", Aciklama = "Sahne kurulumu, ses-ışık sistemleri, sanatçı yönetimi ve güvenlik koordinasyonu dahil büyük ölçekli etkinlik yönetimi.", SiraNo = 3, AktifMi = true },
            new Hizmet { Id = 4, Baslik = "Açılış ve Lansman", Aciklama = "Mağaza açılışı, ürün lansmanı ve basın etkinliklerinde markanızı en doğru şekilde sahneye koyuyoruz.", SiraNo = 4, AktifMi = true }
        );

        modelBuilder.Entity<Referans>().HasData(
            new Referans { Id = 1, MusteriAdi = "Yılmaz Holding", Yorum = "Bayi toplantımız kusursuz geçti, her detay düşünülmüştü.", AktifMi = true },
            new Referans { Id = 2, MusteriAdi = "Elif & Mert", Yorum = "Düğünümüz hayal ettiğimizden de güzeldi, iyi ki sizi seçmişiz!", AktifMi = true },
            new Referans { Id = 3, MusteriAdi = "TechNova Yazılım", Yorum = "Ürün lansmanımızda basın ve konuk yönetimi profesyonelceydi.", AktifMi = true }
        );
    }
}