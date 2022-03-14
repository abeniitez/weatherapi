using System;
using System.Net;
using System.Threading.Tasks;
using Adbeniz.Weather.Restful.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Adbeniz.Weather.Restful.Infrastructure.Middlewares
{
    public class ErrorHandlingMiddleware
    {
		private readonly RequestDelegate next;
		private readonly ILogger<ErrorHandlingMiddleware> logger;

		public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
		{
			this.next = next;
			this.logger = logger;
		}

		public async Task Invoke(HttpContext context)
		{
			try
			{
				await next(context);
			}
			catch (Exception ex)
			{
				await HandleExceptionAsync(context, ex);
			}
		}

		private Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
			logger.Log(GetLevelLogException(exception), 1000, exception, "The request could not be processed.");

			var errorModel = new Models.ErrorModel
			{
				Code = -1,
				Message = "The server encountered an internal error."
			};

			if (exception is ProjectException || exception is TimeoutException)
			{
				context.Response.StatusCode = (int)GetHttpStatusCodeByExceptionType(exception);
			}
			else
			{
				context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

				errorModel.Module = exception.StackTrace;

				if (exception.InnerException != null)
				{
					errorModel.Detail = exception.InnerException.Message;
				}
			}

			if (exception is ProjectException)
			{
				var projectException = exception as ProjectException;

				errorModel.Code = context.Response.StatusCode;
				errorModel.Detail = projectException.Detail;
				errorModel.Module = projectException.Module;
				errorModel.Message = projectException.Message;

				if (projectException.ValidationError != null && projectException.ValidationError.Count > 0)
				{
					errorModel.ValidationError = projectException.ValidationError;
				}
			}

			var result = JsonConvert.SerializeObject(errorModel);

			context.Response.ContentType = "application/json;charset=utf-8";

			return context.Response.WriteAsync(result);
		}

		private LogLevel GetLevelLogException(Exception exception)
		{
			if (exception is ProjectException)
			{
				var projectException = exception as ProjectException;
				return projectException.WithLogError
						? LogLevel.Error : LogLevel.Information;
			}

			return LogLevel.Error;
		}

		private HttpStatusCode GetHttpStatusCodeByExceptionType(Exception exception)
		{
			if (exception is BadRequestProjectException)
			{
				return HttpStatusCode.BadRequest;
			}

			if (exception is ForbiddenProjectException)
			{
				return HttpStatusCode.Forbidden;
			}

			if (exception is NotFoundProjectException)
			{
				return HttpStatusCode.NotFound;
			}

			if (exception is NoContentProjectException)
			{
				return HttpStatusCode.NoContent;
			}

			if (exception is TimeoutProjectException || exception is TimeoutException)
			{
				return HttpStatusCode.RequestTimeout;
			}

			if (exception is UnauthorizedAccessProjectException)
			{
				return HttpStatusCode.Unauthorized;
			}

			if (exception is ServiceUnavailableProjectException)
			{
				return HttpStatusCode.ServiceUnavailable;
			}

			if (exception is ProjectException)
			{
				return HttpStatusCode.InternalServerError;
			}

			return HttpStatusCode.InternalServerError;
		}
    }
}
