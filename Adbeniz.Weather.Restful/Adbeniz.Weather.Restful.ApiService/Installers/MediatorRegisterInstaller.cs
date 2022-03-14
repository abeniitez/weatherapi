using System.Linq;
using System.Reflection;
using Adbeniz.Weather.Restful.ApiService.Installers.Contracts;
using Adbeniz.Weather.Restful.Application;
using Adbeniz.Weather.Restful.Domain;
using Adbeniz.Weather.Restful.Infrastructure;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Adbeniz.Weather.Restful.ApiService.Installers
{
	public class MediatorRegisterInstaller: IInstallerServiceCollection
	{
		public void InstallServices(IServiceCollection services, IConfiguration configuration)
		{
			Assembly[] applicationAssembliesMediator = new[] { typeof(DummyApplication), typeof(DummyInfrastructure) }.Select(x => x.Assembly).ToArray();
			Assembly[] domainAssembliesMediator = new[] { typeof(DummyInfrastructure), typeof(DummyDomain) }.Select(x => x.Assembly).ToArray();

			services.AddMediatR(applicationAssembliesMediator);
			services.AddMediatR(domainAssembliesMediator);
		}
	}
}
