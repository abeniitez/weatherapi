using System;
using Adbeniz.Weather.Restful.ApiService.Configurations;
using Adbeniz.Weather.Restful.ApiService.Installers.Extensions;
using Adbeniz.Weather.Restful.Infrastructure.Extensions;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Adbeniz.Weather.Restful.ApiService
{
	public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
			services.InstallServicesInAssembly(Configuration);

			DependencyInjectionBootstrapper.InitializeContainer(builder =>
			{
				builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>().SingleInstance();
				builder.Populate(services);
			});

			ILifetimeScope container = DependencyInjectionBootstrapper.GetContainer();
			return new AutofacServiceProvider(container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
           if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			app.UseCors(x =>
            {
                x.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
            });

			app.InstallApplicationInAssembly(Configuration);

			app.UseErrorHandlingMiddleware();
			app.UseMvc();
			app.UseHttpLogging();
        }
    }
}
