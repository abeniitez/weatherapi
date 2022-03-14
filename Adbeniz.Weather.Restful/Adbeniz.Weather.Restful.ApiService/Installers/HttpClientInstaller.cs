using System;
using Adbeniz.Weather.Restful.ApiService.Installers.Contracts;
using Adbeniz.Weather.Restful.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Adbeniz.Weather.Restful.ApiService.Installers
{
    public class HttpClientInstaller : IInstallerServiceCollection
    {
		public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient("openweather", httpClient =>
            {
                httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
                httpClient.Timeout = TimeSpan.FromSeconds(30);
                httpClient.BaseAddress = new Uri(configuration.GetValue<string>("OpenWeatherConfiguration:UrlBase"));
            });

            services.AddTransient<OpenWeatherClientService>();
        }
    }
}
