using Infrastructure.Entities;
using Infrastructure.PitchDeckAppDbContext.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.PitchDeckAppDbContext
{
    public class PitchDeckDbContext : DbContext
    {
        public PitchDeckDbContext(DbContextOptions<PitchDeckDbContext> options) : base(options)
        {

        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            if (this.ChangeTracker.HasChanges())
            {
                var entries = this.ChangeTracker.Entries<BaseEntity>();

                foreach (var entity in entries)
                {
                    if (entity.State == EntityState.Added)
                    {
                        entity.Entity.CreateDate = DateTime.UtcNow;
                    }
                    else if (entity.State == EntityState.Modified)
                    {
                        entity.Entity.LastUpdateDate = DateTime.UtcNow;
                    }
                }

                return await base.SaveChangesAsync(cancellationToken);
            }
            else
            {
                await Task.CompletedTask;
            }
            return 0;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ImageDbTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PitchDeckTypeConfiguration());
        }
    }
}
