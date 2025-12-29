// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using static CryptoAnalysis;

public class StockAnalysisContext : DbContext
{
    public DbSet<CombinedStockData> CombinedStockData { get; set; }
    public DbSet<CryptoData> CombinedCryptoData { get; set; }

    public StockAnalysisContext()
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
       => options.UseSqlServer("Server=DESKTOP-BR4G4L7;Database=StockAnalysis;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CombinedStockData>().HasKey(c => new { c.Symbol, c.Date });
        modelBuilder.Entity<CryptoData>().HasKey(c => new { c.BaseCurrency, c.Date });
        base.OnModelCreating(modelBuilder);
    }
}
