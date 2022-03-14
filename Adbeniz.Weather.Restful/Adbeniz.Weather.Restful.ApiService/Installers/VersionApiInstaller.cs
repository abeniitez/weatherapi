using System;
using Adbeniz.Weather.Restful.ApiService.Installers.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Adbeniz.Weather.Restful.ApiService.Installers
{
    public class VersionApiInstaller: IInstallerServiceCollection
	{
		public void InstallServices(IServiceCollection services, IConfiguration configuration)
		{
			services.AddApiVersioning(option =>
			{
				option.ReportApiVersions = true;
				option.AssumeDefaultVersionWhenUnspecified = true;
				option.DefaultApiVersion = new ApiVersion(int.Parse(configuration.GetSection("Api:Version").Value), 0);
			});
		}
	}
}
