using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Adbeniz.Weather.Restful.ApiService.Routes;
using Adbeniz.Weather.Restful.Application.Handlers;
using Adbeniz.Weather.Restful.Application.Handlers.Weathers;
using Adbeniz.Weather.Restful.Application.Models;
using Adbeniz.Weather.Restful.Infrastructure.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Adbeniz.Weather.Restful.ApiService.Controllers.V1
{
	[Route(ApiRoutes.Base)]
	[ApiVersion(ApiRoutes.VersionOne)]
	[ApiController]
    public class WeatherController : ControllerBase
    {
		private readonly IMediator mediator;

		public WeatherController(IMediator mediator)
		{
			this.mediator = mediator;
		}

		[HttpGet("{ciudad}")]
		[MapToApiVersion(ApiRoutes.VersionOne)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> Get(string ciudad)
		{
			WeatherResponseModel result = await mediator.Send(new WeatherByCityHandlerRequest(ciudad));
			return Ok(result);
		}

		[HttpGet("{ciudad}/historical")]
		[MapToApiVersion(ApiRoutes.VersionOne)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetHistorical(string ciudad)
		{
			IEnumerable<WeatherResponseModel> result = await mediator.Send(new WeatherHistoricalByCityNameHandlerRequest(ciudad));
			return Ok(result);
		}
	}
}
