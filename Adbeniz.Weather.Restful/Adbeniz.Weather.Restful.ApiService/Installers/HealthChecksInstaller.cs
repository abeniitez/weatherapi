using System;
using Adbeniz.Weather.Restful.ApiService.Installers.Contracts;
using Adbeniz.Weather.Restful.Domain;
using HealthChecks.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Adbeniz.Weather.Restful.ApiService.Installers
{
    public class HealthChecksInstaller: IInstallerServiceCollection
	{
		public void InstallServices(IServiceCollection services, IConfiguration configuration)
		{
			services.AddHealthChecks()
				.AddDbContextCheck<ClimasDbContext>()
				.AddCheck<RedisHealthCheck>("Redis");
		}
	}
}
