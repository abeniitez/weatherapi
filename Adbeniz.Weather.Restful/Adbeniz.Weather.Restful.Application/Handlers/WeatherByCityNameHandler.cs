using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Adbeniz.Weather.Restful.Application.Models;
using Adbeniz.Weather.Restful.Application.Services;
using Adbeniz.Weather.Restful.Domain;
using Adbeniz.Weather.Restful.Domain.Entities;
using Adbeniz.Weather.Restful.Infrastructure.Data.Contracts;
using Adbeniz.Weather.Restful.Infrastructure.Exceptions;
using Adbeniz.Weather.Restful.Infrastructure.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Adbeniz.Weather.Restful.Application.Handlers
{
	public class WeatherByCityNameHandlerRequest : IRequest<GenericHandlerResponse<ResponseModel>>
	{
		public WeatherByCityNameHandlerRequest(WeatherByCityNameHandlerRequestModel model)
		{
			Model = model;
		}

		public WeatherByCityNameHandlerRequestModel Model { get; set; }
	}



    public class WeatherByCityNameHandler : IRequestHandler<WeatherByCityNameHandlerRequest, GenericHandlerResponse<ResponseModel>>
    {
		private readonly ILogger<WeatherByCityNameHandler> logger;
		private readonly IRepositoryCommand<ClimasDbContext, WeatherHistorical> repositoryCommand;
		private readonly IRepositoryCommand<ClimasDbContext, City> repositoryQuery;
		private readonly IUnitOfWork<ClimasDbContext> unitOfWork;

		private readonly OpenWeatherClientService openWeatherClientService;

		public WeatherByCityNameHandler(
			ILogger<WeatherByCityNameHandler> logger,
			IRepositoryCommand<ClimasDbContext, WeatherHistorical> repositoryCommand,
			IRepositoryCommand<ClimasDbContext, City> repositoryQuery,
			IUnitOfWork<ClimasDbContext> unitOfWork,
			OpenWeatherClientService openWeatherClientService)
		{
			this.logger = logger;
			this.repositoryCommand = repositoryCommand;
			this.repositoryQuery = repositoryQuery;
			this.unitOfWork = unitOfWork;
			this.openWeatherClientService = openWeatherClientService;
		}

		public async Task<GenericHandlerResponse<ResponseModel>> Handle(WeatherByCityNameHandlerRequest request, CancellationToken cancellationToken)
		{
			logger.LogInformation($"Consulta de temperatura ciudad {request.Model.Ciudad}");

			var weatherDataResult = await openWeatherClientService.GetWeatherDataAsync(request.Model.Ciudad);

			var ciudad = await repositoryQuery.GetFirstOrDefaultAsync(x => x.Name.ToLower() == request.Model.Ciudad.ToLower());

			if(ciudad == null)
			{
				logger.LogInformation($"No se encontro la ciudad {request.Model.Ciudad}");
				throw new NotFoundProjectException("No se encontraron resultados");
			}

			var historico = repositoryCommand.Create(new WeatherHistorical{
				CityId =ciudad.ID,
				Weather = weatherDataResult.Main.Temp.ToString(),
				ThermalSensation = weatherDataResult.Main.FeelsLike.ToString(),
				CreateDate = DateTime.Now
			});

			await unitOfWork.SaveChangesAsync();

			var responseModel = new ResponseModel{
				Ciudad = weatherDataResult.Name,
				Pais = weatherDataResult.Sys.Country,
				Temperatura = weatherDataResult.Main.Temp.ToString(),
				SensacionTermica = weatherDataResult.Main.FeelsLike.ToString(),
				SentTime = DateTime.Now,
			};


			return new GenericHandlerResponse<ResponseModel>(responseModel);

		}

    }
}
