using AssetQuote.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AssetQuote.Infrastructure.Data
{
    public class AssetContext : DbContext
    {
        public DbSet<Asset> Asset { get; set; }

        public AssetContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("filename=../db2.sqlite");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssetContext).Assembly);
        }

        public async Task<bool> Commit()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.State == EntityState.Added || entry.State == EntityState.Modified))
            {
                if (entry.State == EntityState.Added)
                    entry.Property("CreateAt").CurrentValue = DateTime.Now;

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("UpdatedAt").CurrentValue = DateTime.Now;
                    entry.Property("CreateAt").IsModified = false;
                }
            }

            return await base.SaveChangesAsync() > 0;
        }
    }
}
