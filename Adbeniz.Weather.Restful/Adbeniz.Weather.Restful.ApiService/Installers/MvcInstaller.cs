using System;
using Adbeniz.Weather.Restful.ApiService.Filters;
using Adbeniz.Weather.Restful.ApiService.Installers.Contracts;
using Adbeniz.Weather.Restful.Application;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Adbeniz.Weather.Restful.ApiService.Installers
{
    public class MvcInstaller: IInstallerServiceCollection
	{
		public void InstallServices(IServiceCollection services, IConfiguration configuration)
		{
			services.AddControllers();
			services.AddCors();
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
			services.AddMvc(options =>
				{
					options.EnableEndpointRouting = false;
					options.Filters.Add<ValidationFilter>();
				})
				.AddFluentValidation(mvcConfiguration => mvcConfiguration.RegisterValidatorsFromAssemblyContaining<DummyApplication>());
		}
	}
}
