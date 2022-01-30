using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.PitchDeckAppDbContext.EntityTypeConfigurations
{
    public class PitchDeckTypeConfiguration : IEntityTypeConfiguration<PitchDeckDb>
    {
        public void Configure(EntityTypeBuilder<PitchDeckDb> builder)
        {
            builder.ToTable("PitchDeck");

            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.Images);
        }
    }
}
