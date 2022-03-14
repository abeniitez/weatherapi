using System;
using Adbeniz.Weather.Restful.ApiService.Installers.Contracts;
using Adbeniz.Weather.Restful.Application.Configurations;
using Adbeniz.Weather.Restful.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Adbeniz.Weather.Restful.ApiService.Installers
{
    public class CacheInstaller: IInstallerServiceCollection
	{
		public void InstallServices(IServiceCollection services, IConfiguration configuration)
		{
			var redisCacheSettings = new RedisCacheSettings();
			configuration.GetSection(nameof(RedisCacheSettings)).Bind(redisCacheSettings);
			services.AddSingleton(redisCacheSettings);

			if (!redisCacheSettings.Enabled)
			{
				return;
			}

			services.AddStackExchangeRedisCache(options => options.Configuration = redisCacheSettings.ConnectionString);
			services.AddSingleton<IConnectionMultiplexer>(_ => ConnectionMultiplexer.Connect(redisCacheSettings.ConnectionString));
			services.AddSingleton<IResponseCacheService, ResponseCacheService>();
		}
	}
}
