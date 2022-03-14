using System;
using Adbeniz.Weather.Restful.Infrastructure.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace Adbeniz.Weather.Restful.Infrastructure.Extensions
{
    public static class HttpLoggerMiddlewareExtensions
    {
		public static IApplicationBuilder UseHttpLogging(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<HttpLoggerMiddleware>();
		}
    }
}
