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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Hizmet>().HasData(
            new Hizmet { Id = 1, Baslik = "Kurumsal Etkinlikler", Aciklama = "Lansman, bayi toplantýsý, gala gecesi ve þirket organizasyonlarýnda uçtan uca planlama.", SiraNo = 1, AktifMi = true },
            new Hizmet { Id = 2, Baslik = "Düðün Organizasyonu", Aciklama = "Niþandan kýna gecesine, salon süslemesinden orkestraya hayalinizdeki düðünü kurgular, siz sadece anýn tadýný çýkarýrsýnýz.", SiraNo = 2, AktifMi = true },
            new Hizmet { Id = 3, Baslik = "Festival ve Konser", Aciklama = "Sahne kurulumu, ses-ýþýk sistemleri, sanatçý yönetimi ve güvenlik koordinasyonu dahil büyük ölçekli etkinlik yönetimi.", SiraNo = 3, AktifMi = true },
            new Hizmet { Id = 4, Baslik = "Açýlýþ ve Lansman", Aciklama = "Maðaza açýlýþý, ürün lansmaný ve basýn etkinliklerinde markanýzý en doðru þekilde sahneye koyuyoruz.", SiraNo = 4, AktifMi = true }
        );

        modelBuilder.Entity<Referans>().HasData(
            new Referans { Id = 1, MusteriAdi = "Yýlmaz Holding", Yorum = "Bayi toplantýmýz kusursuz geçti, her detay düþünülmüþtü.", AktifMi = true },
            new Referans { Id = 2, MusteriAdi = "Elif & Mert", Yorum = "Düðünümüz hayal ettiðimizden de güzeldi, iyi ki sizi seçmiþiz!", AktifMi = true },
            new Referans { Id = 3, MusteriAdi = "TechNova Yazýlým", Yorum = "Ürün lansmanýmýzda basýn ve konuk yönetimi profesyonelceydi.", AktifMi = true }
        );
    }
}