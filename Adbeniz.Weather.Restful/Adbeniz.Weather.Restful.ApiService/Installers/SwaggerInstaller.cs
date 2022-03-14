using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Adbeniz.Weather.Restful.ApiService.Installers.Contracts;
using Adbeniz.Weather.Restful.ApiService.Installers.FilterSwagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Adbeniz.Weather.Restful.ApiService.Installers
{
    public class SwaggerInstaller: IInstallerApplicationBuilder, IInstallerServiceCollection
	{
		public void InstallServices(IServiceCollection services, IConfiguration configuration)
		{
			var contact = new OpenApiContact() { Name = configuration.GetSection("SwaggerConfiguration:ContactName").Value, Email = configuration.GetSection("SwaggerConfiguration:ContactMail").Value };

			services.AddSwaggerGen(swagger =>
			{
				swagger.SwaggerDoc(configuration.GetSection("SwaggerConfiguration:DocNameV1").Value,
					new OpenApiInfo
					{
						Title = configuration.GetSection("SwaggerConfiguration:DocInfoTitle").Value,
						Version = configuration.GetSection("SwaggerConfiguration:DocInfoVersionV1").Value,
						Description = configuration.GetSection("SwaggerConfiguration:DocInfoDescription").Value,
						Contact = contact
					}
				);

				swagger.SwaggerDoc(configuration.GetSection("SwaggerConfiguration:DocNameV2").Value,
					new OpenApiInfo
					{
						Title = configuration.GetSection("SwaggerConfiguration:DocInfoTitle").Value,
						Version = configuration.GetSection("SwaggerConfiguration:DocInfoVersionV2").Value,
						Description = configuration.GetSection("SwaggerConfiguration:DocInfoDescription").Value,
						Contact = contact
					}
				);

				swagger.OperationFilter<RemoveVersionFromParameter>();
				swagger.DocumentFilter<ReplaceVersionWithExactValueInPath>();

				swagger.DocInclusionPredicate((version, desc) =>
				{
					if(!desc.TryGetMethodInfo(out MethodInfo methodInfo))
					{
						return false;
					}
					IEnumerable<ApiVersion> versions = methodInfo.DeclaringType
						.GetCustomAttributes(true)
						.OfType<ApiVersionAttribute>()
						.SelectMany(attr => attr.Versions);
					return versions.Any(v => $"v{v.ToString()}" == version);
					;
				});
			});
		}

		public void InstallApplication(IApplicationBuilder app, IConfiguration configuration)
		{
			app.UseSwagger();

			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint(configuration.GetSection("SwaggerConfiguration:EndpointUrlV1").Value, configuration.GetSection("SwaggerConfiguration:EndpointDescriptionV1").Value);
				c.SwaggerEndpoint(configuration.GetSection("SwaggerConfiguration:EndpointUrlV2").Value, configuration.GetSection("SwaggerConfiguration:EndpointDescriptionV2").Value);
			});
		}
	}
}
