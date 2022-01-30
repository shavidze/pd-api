using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.PitchDeckAppDbContext.EntityTypeConfigurations
{
    public class ImageDbTypeConfiguration : IEntityTypeConfiguration<PitchDeckDb>
    {
        public void Configure(EntityTypeBuilder<PitchDeckDb> builder)
        {
            builder.ToTable("Image");

            builder.HasKey(x => x.Id);
        }
    }
}
