using System;
using System.Linq;
using System.Reflection;
using Adbeniz.Weather.Restful.Application;
using Adbeniz.Weather.Restful.Infrastructure;
using Adbeniz.Weather.Restful.Infrastructure.Services;
using Autofac;

namespace Adbeniz.Weather.Restful.ApiService.Configurations
{
    public class ServicesAutofacModule : Autofac.Module
    {
		private static Assembly[] ApplicationAssemblies
			=> new[] { typeof(DummyApplication), typeof(DummyInfrastructure) }.Select(x => x.Assembly).ToArray();

		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterAssemblyTypes(ApplicationAssemblies)
				.Where(x => typeof(IService).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
				.AsImplementedInterfaces()
				.InstancePerLifetimeScope();
		}
    }
}
