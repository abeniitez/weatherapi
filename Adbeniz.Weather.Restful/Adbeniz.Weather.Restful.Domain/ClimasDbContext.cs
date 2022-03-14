using System;
using Adbeniz.Weather.Restful.Domain.Entities;
using Adbeniz.Weather.Restful.Domain.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Adbeniz.Weather.Restful.Domain
{
	public partial class ClimasDbContext : DbContext
	{
		public ClimasDbContext()
		{
		}

		public ClimasDbContext(DbContextOptions<ClimasDbContext> options)
            : base(options)
        {
        }

		public virtual DbSet<City> Cities { get; set; }
		public virtual DbSet<WeatherHistorical> Historics { get; set; }
		public virtual DbSet<User> Users { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=51.222.14.127;Initial Catalog=climas;Persist Security Info=True;User ID=usuarioclima;Password=UsuarioClima123;MultipleActiveResultSets=true;");
            }
        }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.ApplyConfiguration(new CityConfiguration());
			modelBuilder.ApplyConfiguration(new WeatherHistoricalConfiguration());
			modelBuilder.ApplyConfiguration(new UserConfiguration());
		}
	}
}
