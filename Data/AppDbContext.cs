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
}