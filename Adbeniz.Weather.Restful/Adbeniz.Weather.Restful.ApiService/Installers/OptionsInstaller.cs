using System;
using Adbeniz.Weather.Restful.ApiService.Installers.Contracts;
using Adbeniz.Weather.Restful.Application.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Adbeniz.Weather.Restful.ApiService.Installers
{
    public class OptionsInstaller: IInstallerServiceCollection
	{
		public void InstallServices(IServiceCollection services, IConfiguration configuration)
		{
			services.Configure<OpenWeatherConfiguration>(configuration.GetSection("OpenWeatherConfiguration"));
			services.Configure<TokenConfiguration>(configuration.GetSection("Security"));
		}
	}
}
