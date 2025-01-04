using Dbtools.models;
using Httpclient.AppSettings;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Dbtools.Data;

public class DbContextSteam: DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        string connectionString= ConnectionStringManager.GetConnectionString();
        Log.Information("Connecting to Steam"+
                        " with connection string {connectionString}");
        optionsBuilder.UseMySql(connectionString
        , ServerVersion.AutoDetect(ConnectionStringManager.GetConnectionString()));
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<SteamItem>(entity =>
        {
            entity.ToTable("SteamItems");
            entity.Property(e => e.ItemType).IsRequired().HasMaxLength(50);
            entity.Property(e => e.ItemName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Quality)
                .IsRequired()
                .HasConversion(
                    v => v.ToString(),
                    v => (ItemQuality)Enum.Parse(typeof(ItemQuality), v));
        
            entity.HasOne(si => si.PriceInfo) // Один SteamItem
                .WithOne(pi => pi.SteamItem) // Связан с одним ItemPriceInfo
                .HasForeignKey<ItemPriceInfo>(pi => pi.ItemId) // Внешний ключ
                .OnDelete(DeleteBehavior.Cascade); // Каскадное удаление
        });

        modelBuilder.Entity<ItemPriceInfo>(entity =>
        {
            entity.ToTable("ItemPriceInfos");
            entity.Property(e => e.MinPrice).IsRequired().HasColumnType("decimal(18, 2)");
            entity.Property(e => e.SalesVolume).IsRequired();
            entity.Property(e => e.AvgPrice).IsRequired().HasColumnType("decimal(18, 2)");
        });
    }

    public DbSet<SteamItem> Items { get; set; }
    public DbSet<ItemPriceInfo> ItemPriceInfos { get; set; }
}