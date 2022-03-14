using Adbeniz.Weather.Restful.ApiService.Installers.Contracts;
using Adbeniz.Weather.Restful.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Adbeniz.Weather.Restful.ApiService.Installers
{
    public class DbInstaller: IInstallerServiceCollection
	{
		public void InstallServices(IServiceCollection services, IConfiguration configuration)
		{
			services.AddEntityFrameworkSqlServer().AddDbContext<ClimasDbContext>
				(options => options.UseSqlServer(configuration.GetConnectionString("DbConnection")));
		}
	}
}
