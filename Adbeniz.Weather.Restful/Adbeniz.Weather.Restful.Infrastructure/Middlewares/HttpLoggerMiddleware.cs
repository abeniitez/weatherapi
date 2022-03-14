using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NLog;

namespace Adbeniz.Weather.Restful.Infrastructure.Middlewares
{
    public class HttpLoggerMiddleware
    {
		private readonly RequestDelegate next;
		private static readonly Logger Logger = LogManager.GetLogger(typeof(HttpLoggerMiddleware).Assembly.FullName);

		public HttpLoggerMiddleware(RequestDelegate next)
		{
			this.next = next;
		}

		public Task Invoke(HttpContext context)
		{
			Logger.Log(new LogEventInfo(LogLevel.Info, Logger.Name, $"Start request: {context.Request.Method} {context.Request.Path}"));

			DateTime startTime = DateTime.Now;
			return next(context).ContinueWith(task => {

				var beginRequestEvent = new LogEventInfo(LogLevel.Info, Logger.Name, $"End request: {context.Request.Method} {context.Request.Path}");

				beginRequestEvent.Properties.Add("appdata_start_time", startTime.ToString("yyyy-MM-dd HH:mm:ss.fffff"));
				beginRequestEvent.Properties.Add("appdata_status", context.Response.StatusCode);
				beginRequestEvent.Properties.Add("appdata_timetaken", ( DateTime.Now - startTime ).TotalMilliseconds);

				Logger.Log(beginRequestEvent);
			});
		}
    }
}
