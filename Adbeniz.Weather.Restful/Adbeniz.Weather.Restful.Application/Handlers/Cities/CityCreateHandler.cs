using System;
using System.Threading;
using System.Threading.Tasks;
using Adbeniz.Weather.Restful.Application.Models.Cities;
using Adbeniz.Weather.Restful.Domain;
using Adbeniz.Weather.Restful.Domain.Entities;
using Adbeniz.Weather.Restful.Infrastructure.Data.Contracts;
using Adbeniz.Weather.Restful.Infrastructure.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Adbeniz.Weather.Restful.Application.Handlers.Cities
{
    public class CityCreateHandlerRequest : IRequest<CityCreateHandlerResponse>
    {
		public CityCreateHandlerRequest(CityCreateRequestModel model)
		{
			Model = model;
		}

		public CityCreateRequestModel Model { get; set; }
	}

	public class CityCreateHandlerResponse
    {
		public CityCreateHandlerResponse(int cityId)
		{
			CityId = cityId;
		}

		public int CityId { get; set; }

	}

	public class CityCreateHandler : IRequestHandler<CityCreateHandlerRequest, CityCreateHandlerResponse>
    {
		private readonly ILogger<CityCreateHandler> logger;
		private readonly IRepositoryCommand<ClimasDbContext, City> repositoryCommand;
		private readonly IUnitOfWork<ClimasDbContext> unitOfWork;

		public CityCreateHandler(ILogger<CityCreateHandler> logger, IRepositoryCommand<ClimasDbContext, City> repositoryCommand, IUnitOfWork<ClimasDbContext> unitOfWork)
		{
			this.logger = logger;
			this.repositoryCommand = repositoryCommand;
			this.unitOfWork = unitOfWork;
		}

		public async Task<CityCreateHandlerResponse> Handle(CityCreateHandlerRequest request, CancellationToken cancellationToken)
		{
			logger.LogInformation("Comienzo de Alta de Ciudad");
			var city = await repositoryCommand.GetFirstOrDefaultAsync(x => x.Name == request.Model.City && x.Country == request.Model.Country);
			if(city != null)
			{
				logger.LogError($"Ya existe la ciudad {request.Model.City} en el pais {request.Model.Country}");

				throw new ConflictProjectException($"Ya existe la ciudad {request.Model.City} en el pais {request.Model.Country}");
			}

			var newCity = repositoryCommand.Create(new City{
				Name = request.Model.City,
				Country = request.Model.Country,
				Enable = true
			});

			await unitOfWork.SaveChangesAsync();
			logger.LogInformation("La ciudad se genero correctamente");

			return new CityCreateHandlerResponse(newCity.ID);
		}
    }
}
