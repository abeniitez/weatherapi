using System.Linq;
using System.Reflection;
using Adbeniz.Weather.Restful.Application;
using Adbeniz.Weather.Restful.Application.Mappers;
using Adbeniz.Weather.Restful.Infrastructure;
using Autofac;
using MediatR;

namespace Adbeniz.Weather.Restful.ApiService.Configurations
{
    public class MappersAutofacModule: Autofac.Module
	{
		private static Assembly[] ApplicationAssemblies
			=> new[] { typeof(DummyApplication), typeof(DummyInfrastructure) }.Select(x => x.Assembly).ToArray();

		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces();

			builder
				.RegisterAssemblyTypes(ApplicationAssemblies)
				.AsClosedTypesOf(typeof(IMapperEntityModel<,>))
				.AsImplementedInterfaces()
				.InstancePerLifetimeScope();
		}
	}
}
