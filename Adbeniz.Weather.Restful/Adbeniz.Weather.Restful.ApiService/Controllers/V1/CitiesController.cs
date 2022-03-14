using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Adbeniz.Weather.Restful.ApiService.Routes;
using Adbeniz.Weather.Restful.Application.Handlers.Cities;
using Adbeniz.Weather.Restful.Application.Models;
using Adbeniz.Weather.Restful.Application.Models.Cities;
using Adbeniz.Weather.Restful.Infrastructure.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Adbeniz.Weather.Restful.ApiService.Controllers.V1
{
	[Route(ApiRoutes.Base)]
	[ApiVersion(ApiRoutes.VersionOne)]
	[ApiController]
    public class CitiesController : ControllerBase
    {
		private readonly IMediator mediator;

		public CitiesController(IMediator mediator)
		{
			this.mediator = mediator;
		}

		[HttpPost("")]
		[MapToApiVersion(ApiRoutes.VersionOne)]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> Post([FromBody]CityCreateRequestModel model)
		{
			CityCreateHandlerResponse result = await mediator.Send(new CityCreateHandlerRequest(model));
			return Ok(result);
		}

		[HttpGet("")]
		[MapToApiVersion(ApiRoutes.VersionOne)]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetAll()
		{
			IEnumerable<CityResponseModel> result = await mediator.Send(new CityGetAllHandlerRequest());
			return Ok(result);
		}
	}
}
