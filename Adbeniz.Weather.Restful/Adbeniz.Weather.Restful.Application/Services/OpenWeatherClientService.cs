using System;
using Adbeniz.Weather.Restful.Infrastructure.Services;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Adbeniz.Weather.Restful.Application.Models;
using Adbeniz.Weather.Restful.Application.Configurations;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;
using Adbeniz.Weather.Restful.Infrastructure.Extensions;

namespace Adbeniz.Weather.Restful.Application.Services
{
	public interface IOpenWeatherClientService
	{
		Task<OpenWeatherGetResultModel> GetWeatherDataAsync(string CityName);
	}

    public class OpenWeatherClientService : IOpenWeatherClientService, IService
    {
		private readonly ILogger<OpenWeatherClientService> logger;
		private readonly IHttpClientFactory httpClientFactory;
		private readonly OpenWeatherConfiguration openWeatherConfiguration;

		private const string HttpClient = "openweather";

		public OpenWeatherClientService(
			ILogger<OpenWeatherClientService> logger,
			IHttpClientFactory httpClientFactory,
			IOptions<OpenWeatherConfiguration> options)
		{
			this.logger = logger;
			this.httpClientFactory = httpClientFactory;
			this.openWeatherConfiguration = options.Value;
		}

		public async Task<OpenWeatherGetResultModel> GetWeatherDataAsync(string CityName)
        {
            using (HttpClient httpClient = httpClientFactory.CreateClient(HttpClient))
            {
                using (HttpResponseMessage httpResponseMessage = await httpClient.GetAsync($"data/2.5/weather?q={CityName}&appid={openWeatherConfiguration.ApiKey}"))
                {
                    var response = await httpResponseMessage.GetContentWithStatusCodeValidated();

                    return Deserialize<OpenWeatherGetResultModel>(response);
                }
            }
        }

		private StringContent Serealize<T>(T objeto)
        {
            var jsonPeticion = JsonConvert.SerializeObject(objeto , Formatting.Indented);

            return new StringContent(jsonPeticion, Encoding.UTF8, "application/json");
        }

        private T Deserialize<T>(string json)
            => JsonConvert.DeserializeObject<T>(json);
	}
}
