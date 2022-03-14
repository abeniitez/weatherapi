using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Adbeniz.Weather.Restful.ApiService.Installers.Contracts
{
	public interface IInstallerApplicationBuilder
	{
		void InstallApplication(IApplicationBuilder app, IConfiguration configuration);
	}

	public interface IInstallerServiceCollection
	{
		void InstallServices(IServiceCollection services, IConfiguration configuration);
	}
}
