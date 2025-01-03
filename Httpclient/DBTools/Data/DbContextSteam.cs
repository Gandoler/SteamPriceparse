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
            entity.ToTable("SteamItems"); // Название таблицы
            entity.Property(e => e.ItemType).IsRequired().HasMaxLength(50);
            entity.Property(e => e.ItemName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Quality)
                .IsRequired()
                .HasConversion(
                    v => v.ToString(), // Сохранение как строка
                    v => (ItemQuality)Enum.Parse(typeof(ItemQuality), v)); // Чтение из строки
  
            
        });
    }

    public DbSet<SteamItem> Items { get; set; }
}