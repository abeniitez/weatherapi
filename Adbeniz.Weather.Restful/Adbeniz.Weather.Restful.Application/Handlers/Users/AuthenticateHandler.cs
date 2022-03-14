using System;
using System.Threading;
using System.Threading.Tasks;
using Adbeniz.Weather.Restful.Application.Models.Authenticate;
using Adbeniz.Weather.Restful.Application.Services;
using Adbeniz.Weather.Restful.Infrastructure.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Adbeniz.Weather.Restful.Application.Handlers.Users
{
	public class AuthenticateHandlerRequest : IRequest<AuthenticateHandlerResponse>
    {
		public AuthenticateHandlerRequest(AuthenticateRequest model)
		{
			Model = model;
		}

		public AuthenticateRequest Model { get; set; }
    }

	public class AuthenticateHandlerResponse
	{
		public AuthenticateHandlerResponse(string status, AuthenticateResult result)
		{
			Status = status;
			Result = result;
		}

		public string Status { get; set; }
		public AuthenticateResult Result { get; set; }
	}


    public class AuthenticateHandler : IRequestHandler<AuthenticateHandlerRequest, AuthenticateHandlerResponse>
    {
		private readonly ILogger<AuthenticateHandler> logger;
		private readonly IUserService userService;

		public static string ERROR_LOGIN = "usuario y/o contraseña incorrecta";
		public static string STATUS_200OK = "ok";

		public AuthenticateHandler(ILogger<AuthenticateHandler> logger, IUserService userService)
		{
			this.logger = logger;
			this.userService = userService;
		}

		public async Task<AuthenticateHandlerResponse> Handle(AuthenticateHandlerRequest request, CancellationToken cancellationToken)
		{
			logger.LogInformation("Inicio authenticate handler");
			var result = await userService.Authenticate(request.Model);

			if(result == null)
			{
				logger.LogInformation("usuario y/o contraseña incorrecta");
				//throw new UnauthorizedAccessProjectException("usuario y/o contraseña incorrecta");
				return new AuthenticateHandlerResponse(ERROR_LOGIN, new AuthenticateResult());
			}

			return new AuthenticateHandlerResponse(STATUS_200OK, result);
		}
	}
}
