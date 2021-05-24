using AssetQuote.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AssetQuote.Infrastructure.Data
{
  public class AssetContext : DbContext
    {
        public AssetContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("filename=../db.sqlite");
        }
        public DbSet<Asset> Asset { get; set; }
    }
}
