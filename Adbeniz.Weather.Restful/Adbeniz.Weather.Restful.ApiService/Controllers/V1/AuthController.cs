using System;
using System.Threading.Tasks;
using Adbeniz.Weather.Restful.ApiService.Routes;
using Adbeniz.Weather.Restful.Application.Handlers.Users;
using Adbeniz.Weather.Restful.Application.Models.Authenticate;
using Adbeniz.Weather.Restful.Infrastructure.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Adbeniz.Weather.Restful.ApiService.Controllers.V1
{
	[Route(ApiRoutes.Base)]
	[ApiVersion(ApiRoutes.VersionOne)]
	[ApiController]
    public class AuthController : ControllerBase
    {
		private readonly IMediator mediator;

		public AuthController(IMediator mediator)
		{
			this.mediator = mediator;
		}

		[HttpPost("")]
		[MapToApiVersion(ApiRoutes.VersionOne)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ErrorModel),StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> Post([FromBody]AuthenticateRequest model)
		{
			var result = await mediator.Send(new AuthenticateHandlerRequest(model));
			return Ok(result);
		}


	}
}
