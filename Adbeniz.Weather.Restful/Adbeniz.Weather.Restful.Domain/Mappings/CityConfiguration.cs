using System;
using Adbeniz.Weather.Restful.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adbeniz.Weather.Restful.Domain.Mappings
{
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
		public void Configure(EntityTypeBuilder<City> builder)
		{
			builder.ToTable("Cities");
            builder.HasKey(c => c.ID);

            builder.Property(c => c.ID).ValueGeneratedOnAdd();
            builder.Property(c => c.Name).IsRequired().HasColumnType("VARCHAR(50)").HasMaxLength(50);
            builder.Property(c => c.Country).IsRequired().HasColumnType("VARCHAR(50)").HasMaxLength(50);
			builder.Property(c => c.Enable).IsRequired().HasDefaultValue(false);
		}
    }
}
