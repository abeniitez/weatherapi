using System;
using Adbeniz.Weather.Restful.Domain;
using Adbeniz.Weather.Restful.Infrastructure.Data;
using Adbeniz.Weather.Restful.Infrastructure.Data.Contracts;
using Autofac;
using Microsoft.EntityFrameworkCore;

namespace Adbeniz.Weather.Restful.ApiService.Configurations
{
    public class DataAutofacModule: Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<ClimasDbContext>()
				.As(typeof(DbContext))
				.InstancePerLifetimeScope();

			builder.RegisterGeneric(typeof(UnitOfWork<>))
				.As(typeof(IUnitOfWork<>))
				.InstancePerLifetimeScope();

			builder.RegisterGeneric(typeof(Repository<,>))
				.As(typeof(IRepositoryQuery<,>))
				.WithParameter("traking", false);

			builder.RegisterGeneric(typeof(Repository<,>))
				.As(typeof(IRepositoryCommand<,>))
				.WithParameter("traking", false);
		}
	}
}
