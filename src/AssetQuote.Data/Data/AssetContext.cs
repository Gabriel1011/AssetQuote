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

        public AssetContext(DbContextOptions<AssetContext> option) : base(option)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssetContext).Assembly);
            MapearPropriedadesEsquecidas(modelBuilder);
        }

        public async Task DetachAllEntities()
        {
            this.ChangeTracker.Clear();

            var changedEntriesCopy = this.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added ||
                            e.State == EntityState.Modified ||
                            e.State == EntityState.Deleted)
                .ToList();

            foreach (var entry in changedEntriesCopy)
                entry.State = EntityState.Detached;

            await Task.CompletedTask;
        }

        public async Task Commit()
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

            await base.SaveChangesAsync();

            await DetachAllEntities();
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
