using System;
using System.Linq;
using Adbeniz.Weather.Restful.ApiService.Installers.Contracts;
using Adbeniz.Weather.Restful.Infrastructure.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Adbeniz.Weather.Restful.ApiService.Installers.Extensions
{
    public static class InstallerExtensions
    {
		public static void InstallServicesInAssembly(this IServiceCollection services, IConfiguration configuration)
		{
			var installers = typeof(Startup).Assembly.ExportedTypes.Where(x =>
				typeof(IInstallerServiceCollection).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract).Select(Activator.CreateInstance).Cast<IInstallerServiceCollection>().ToList();

			installers.ForEach(installer => installer.InstallServices(services, configuration));
		}

		public static void InstallApplicationInAssembly(this IApplicationBuilder app, IConfiguration configuration)
		{
			var installers = typeof(Startup).Assembly.ExportedTypes.Where(x =>
				typeof(IInstallerApplicationBuilder).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract).Select(Activator.CreateInstance).Cast<IInstallerApplicationBuilder>().ToList();

			installers.ForEach(installer => installer.InstallApplication(app, configuration));

			app.UseHealthChecks("/health", new HealthCheckOptions
			{
				ResponseWriter = async (context, report) =>
				{
					context.Response.ContentType = "application/json";

					var response = new HealthCheckResponse
					{
						Status = report.Status.ToString(),
						Checks = report.Entries.Select(x => new HealthCheck
						{
							Component = x.Key,
							Status = x.Value.Status.ToString(),
							Description = x.Value.Description
						}),
						Duration = report.TotalDuration
					};

					await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
				}
			});
		}
	}
}
