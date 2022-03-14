using System;
using System.Threading;
using System.Threading.Tasks;
using Adbeniz.Weather.Restful.Application.Models;
using Adbeniz.Weather.Restful.Application.Services;
using Adbeniz.Weather.Restful.Domain;
using Adbeniz.Weather.Restful.Domain.Entities;
using Adbeniz.Weather.Restful.Infrastructure.Data.Contracts;
using Adbeniz.Weather.Restful.Infrastructure.Exceptions;
using Adbeniz.Weather.Restful.Infrastructure.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Adbeniz.Weather.Restful.Application.Handlers.Weathers
{
    public class WeatherByCityHandlerRequest : IRequest<WeatherResponseModel>
    {
		public WeatherByCityHandlerRequest(string city)
		{
			City = city;
		}

		public string City { get; set; }
    }

	public class WeatherByCityHandler : IRequestHandler<WeatherByCityHandlerRequest, WeatherResponseModel>
    {
		private readonly ILogger<WeatherByCityHandler> logger;
		private readonly IRepositoryCommand<ClimasDbContext, WeatherHistorical> weatherRepositoryCommand;
		private readonly IRepositoryQuery<ClimasDbContext, City> cityRepositoryQuery;
		private readonly IUnitOfWork<ClimasDbContext> unitOfWork;
		private readonly OpenWeatherClientService openWeatherClientService;

		public WeatherByCityHandler(
			ILogger<WeatherByCityHandler> logger,
			IRepositoryCommand<ClimasDbContext, WeatherHistorical> weatherRepositoryCommand,
			IRepositoryQuery<ClimasDbContext, City> cityRepositoryQuery,
			IUnitOfWork<ClimasDbContext> unitOfWork,
			OpenWeatherClientService openWeatherClientService)
		{
			this.logger = logger;
			this.weatherRepositoryCommand = weatherRepositoryCommand;
			this.cityRepositoryQuery = cityRepositoryQuery;
			this.unitOfWork = unitOfWork;
			this.openWeatherClientService = openWeatherClientService;
		}

		public async Task<WeatherResponseModel> Handle(WeatherByCityHandlerRequest request, CancellationToken cancellationToken)
		{
			var city = await cityRepositoryQuery.GetFirstOrDefaultAsync(c => c.Name.ToLower() == request.City.ToLower());

			if( city == null)
			{
				logger.LogInformation($"No se encontro la ciudad {request.City}");
				throw new NotFoundProjectException($"No se encontro la ciudad {request.City}");
			}

			var weatherDataResult = await openWeatherClientService.GetWeatherDataAsync(request.City.ToLower());

			var historico = weatherRepositoryCommand.Create(new WeatherHistorical{
				CityId =city.ID,
				Weather = ObjectEssential.ConvertToCelcius(weatherDataResult.Main.Temp),
				ThermalSensation = ObjectEssential.ConvertToCelcius(weatherDataResult.Main.FeelsLike),
				CreateDate = DateTime.Now
			});

			await unitOfWork.SaveChangesAsync();

			var responseModel = new WeatherResponseModel{
				City = weatherDataResult.Name,
				Country = weatherDataResult.Sys.Country,
				Weather = ObjectEssential.ConvertToCelcius(weatherDataResult.Main.Temp),
				ThermalSensation = ObjectEssential.ConvertToCelcius(weatherDataResult.Main.FeelsLike)
			};

			return responseModel;
		}
    }
}
