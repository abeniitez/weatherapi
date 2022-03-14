using System;
using System.IdentityModel.Tokens;
using System.Text;
using Adbeniz.Weather.Restful.ApiService.Installers.Contracts;
using Adbeniz.Weather.Restful.Application.Configurations;
using Adbeniz.Weather.Restful.Application.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Adbeniz.Weather.Restful.ApiService.Installers
{
    public class AuthenticateInstaller : IInstallerServiceCollection
    {
		public void InstallServices(IServiceCollection services, IConfiguration configuration)
		{
			services.AddScoped<IUserService, UserService>();

		}

    }
}
