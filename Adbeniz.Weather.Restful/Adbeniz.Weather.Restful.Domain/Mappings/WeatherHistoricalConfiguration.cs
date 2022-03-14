using System;
using Adbeniz.Weather.Restful.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adbeniz.Weather.Restful.Domain.Mappings
{
    public class WeatherHistoricalConfiguration :  IEntityTypeConfiguration<WeatherHistorical>
    {
		public void Configure(EntityTypeBuilder<WeatherHistorical> builder)
		{
			builder.ToTable("WeatherHistorical");
            builder.HasKey(c => c.ID);

            builder.Property(c => c.ID).ValueGeneratedOnAdd();
            builder.Property(c => c.Weather).IsRequired().HasColumnType("VARCHAR(50)").HasMaxLength(50);
            builder.Property(c => c.ThermalSensation).IsRequired().HasColumnType("VARCHAR(50)").HasMaxLength(50);
			builder.Property(c => c.CityId).IsRequired();
			builder.Property(c => c.CreateDate).IsRequired().HasDefaultValue(DateTime.UtcNow);

			builder.Metadata.FindNavigation(nameof(WeatherHistorical.City)).SetPropertyAccessMode(PropertyAccessMode.Field);
            builder.HasOne(x => x.City).WithMany(x => x.Historics).HasForeignKey(x => x.CityId);
		}
	}
}
