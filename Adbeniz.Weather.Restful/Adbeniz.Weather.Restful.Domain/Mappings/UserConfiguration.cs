using System;
using Adbeniz.Weather.Restful.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adbeniz.Weather.Restful.Domain.Mappings
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.ToTable("Users");
            builder.HasKey(c => c.ID);

            builder.Property(c => c.ID).ValueGeneratedOnAdd();
            builder.Property(c => c.Email).IsRequired().HasColumnType("VARCHAR(150)").HasMaxLength(50);
            builder.Property(c => c.Password).IsRequired().HasColumnType("VARCHAR(150)").HasMaxLength(50);
			builder.Property(c => c.FullName).IsRequired().HasColumnType("VARCHAR(150)").HasMaxLength(50);
			builder.Property(c => c.DateOfBirth).HasDefaultValueSql("GETDATE()");
		}
    }
}
