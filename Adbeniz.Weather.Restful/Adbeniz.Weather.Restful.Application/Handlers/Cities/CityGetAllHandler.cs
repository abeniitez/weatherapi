using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Adbeniz.Weather.Restful.Application.Models.Cities;
using Adbeniz.Weather.Restful.Domain;
using Adbeniz.Weather.Restful.Domain.Entities;
using Adbeniz.Weather.Restful.Infrastructure.Data.Contracts;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Adbeniz.Weather.Restful.Application.Handlers.Cities
{
    public class CityGetAllHandlerRequest : IRequest<IEnumerable<CityResponseModel>>
    {
    }
	public class CityGetAllHandler : IRequestHandler<CityGetAllHandlerRequest, IEnumerable< CityResponseModel>>
    {
		private readonly ILogger<CityGetAllHandler> logger;
		private readonly IRepositoryQuery<ClimasDbContext, City> repositoryQuery;

		public CityGetAllHandler(ILogger<CityGetAllHandler> logger, IRepositoryQuery<ClimasDbContext, City> repositoryQuery)
		{
			this.logger = logger;
			this.repositoryQuery = repositoryQuery;
		}

		public async Task<IEnumerable<CityResponseModel>> Handle(CityGetAllHandlerRequest resquest, CancellationToken cancellationToken)
		{
			logger.LogInformation("Inicio - Obtener Ciudades");
			var cities = await repositoryQuery.GetAsync(c => c.Enable == true);

			if(!cities.Any())
			{
				logger.LogInformation("No se encontraron resultados");
				return Enumerable.Empty<CityResponseModel>();
			}

			List<CityResponseModel> resultList = new List<CityResponseModel>();
			cities.OrderBy(x => x.Name).ToList().ForEach(city => resultList.Add(new CityResponseModel{
				City = city.Name,
				Country = city.Country
			}));

			return resultList;
		}
	}
}
