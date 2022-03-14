using System;
using Adbeniz.Weather.Restful.Infrastructure.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace Adbeniz.Weather.Restful.Infrastructure.Extensions
{
    public static class ApplicationBuilderMiddlewareExtensions
    {
		public static IApplicationBuilder UseErrorHandlingMiddleware(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<ErrorHandlingMiddleware>();
		}
    }
}
