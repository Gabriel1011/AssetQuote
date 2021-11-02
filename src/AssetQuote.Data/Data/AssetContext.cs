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
        public DbSet<BotThread> BotThread { get; set; }

        public AssetContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("filename= db2.sqlite");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssetContext).Assembly);
            MapearPropriedadesEsquecidas(modelBuilder);
        }

        public async Task<bool> Commit()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.State == EntityState.Added || entry.State == EntityState.Modified))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("CreatedAt").CurrentValue = DateTime.Now;
                    entry.Property("Active").CurrentValue = true;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("UpdatedAt").CurrentValue = DateTime.Now;
                    entry.Property("CreatedAt").IsModified = false;
                }
            }

            return await base.SaveChangesAsync() > 0;
        }

        private void MapearPropriedadesEsquecidas(ModelBuilder modelBuilder)
        {

            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                var propriedades = entity.GetProperties().Where(p => p.ClrType == typeof(string));

                foreach (var propriedade in propriedades)
                {
                    if (string.IsNullOrEmpty(propriedade.GetColumnType()) && !propriedade.GetMaxLength().HasValue)
                    {
                        //propriedade.SetMaxLength(100);
                        propriedade.SetColumnType("VARCHAR(200)");
                    }
                }
            }
        }
    }
}
