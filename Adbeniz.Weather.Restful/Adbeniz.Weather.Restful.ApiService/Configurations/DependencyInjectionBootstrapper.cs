using System;
using Autofac;
using Autofac.Features.Variance;
using NLog;

namespace Adbeniz.Weather.Restful.ApiService.Configurations
{
    public class DependencyInjectionBootstrapper
    {
		private static readonly Logger Logger = LogManager.GetLogger(typeof(DependencyInjectionBootstrapper).FullName);

		private static IContainer container;

		public static IContainer GetContainer()
		{
			if (container == null)
			{
				throw new Exception("InitializeContainer was not called");
			}

			return container;
		}

		public static void InitializeContainer(Action<ContainerBuilder> configurator = null)
		{
			var builder = new ContainerBuilder();

			builder.RegisterSource(new ContravariantRegistrationSource());
			builder.RegisterModule(new DataAutofacModule());
			builder.RegisterModule(new MediatrAutofacModule());
			builder.RegisterModule(new ServicesAutofacModule());
			builder.RegisterModule(new MappersAutofacModule());

			configurator?.Invoke(builder);

			container = builder.Build();
		}
    }
}
