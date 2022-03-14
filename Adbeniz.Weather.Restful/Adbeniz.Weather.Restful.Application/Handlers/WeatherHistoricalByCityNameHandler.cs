using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Adbeniz.Weather.Restful.Application.Models;
using Adbeniz.Weather.Restful.Domain;
using Adbeniz.Weather.Restful.Domain.Entities;
using Adbeniz.Weather.Restful.Infrastructure.Data.Contracts;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Adbeniz.Weather.Restful.Application.Handlers
{
	public class WeatherHistoricalByCityNameHandlerRequest : IRequest<IEnumerable<WeatherResponseModel>>
	{
		public WeatherHistoricalByCityNameHandlerRequest(string city)
		{
			City = city;
		}

		public string City { get; set; }
	}

    public class WeatherHistoricalByCityNameHandler : IRequestHandler<WeatherHistoricalByCityNameHandlerRequest, IEnumerable<WeatherResponseModel>>
    {
		private readonly ILogger<WeatherHistoricalByCityNameHandler> logger;
		private readonly IRepositoryQuery<ClimasDbContext, WeatherHistorical> repositoryQuery;

		public WeatherHistoricalByCityNameHandler(ILogger<WeatherHistoricalByCityNameHandler> logger, IRepositoryQuery<ClimasDbContext, WeatherHistorical> repositoryQuery)
		{
			this.logger = logger;
			this.repositoryQuery = repositoryQuery;
		}

		public async Task<IEnumerable<WeatherResponseModel>> Handle(WeatherHistoricalByCityNameHandlerRequest request, CancellationToken cancellationToken)
		{
			logger.LogInformation("Inicio - obtener historial de consultas de clima por ciudad");
			var historical = await repositoryQuery.GetAsync(x=> x.City.Name.ToLower() == request.City.ToLower(),null,c=>c.City);

			if(!historical.Any())
			{
				logger.LogInformation($"No se encontroron registros para {request.City}");
				return Enumerable.Empty<WeatherResponseModel>();
			}

			List<WeatherResponseModel> resultList = new List<WeatherResponseModel>();

			historical.OrderByDescending(x=>x.CreateDate).ToList().ForEach(weatherQuery => resultList.Add(new WeatherResponseModel{
				Country =  weatherQuery.City.Country,
				City = weatherQuery.City.Name,
				Weather = weatherQuery.Weather,
				ThermalSensation = weatherQuery.ThermalSensation
			}));

			return resultList;
		}
    }
}
