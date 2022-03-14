using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;

namespace Adbeniz.Weather.Restful.ApiService
{
    public class Program
    {
        		public static async Task Main(string[] args)
		{
			Logger logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
			try
			{
				logger.Info("init service api");
				await CreateHostBuilder(args).Build().RunAsync();
			}
			catch (Exception exception)
			{
				logger.Error(exception, "Stopped program because of exception");
				throw;
			}
			finally
			{
				LogManager.Shutdown();
			}
		}

		public static IWebHostBuilder CreateHostBuilder(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
			.UseUrls("http://localhost:1400/")
			.ConfigureLogging(logging =>
			{
				logging.ClearProviders();
				logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
			})
			.ConfigureAppConfiguration((hostingContext, config) =>
			{
				IWebHostEnvironment env = hostingContext.HostingEnvironment;

				config.AddJsonFile("appsetting.json", optional: true, reloadOnChange: true)
						.AddJsonFile($"appsetting.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
			})
			.UseNLog()
			.UseStartup<Startup>();
    }
}
