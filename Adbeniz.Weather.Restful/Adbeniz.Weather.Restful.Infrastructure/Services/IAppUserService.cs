using System;

namespace Adbeniz.Weather.Restful.Infrastructure.Services
{
    public interface IAppUserService
	{
		string GetUserId();
		string GetUserName();
	}


	public class AppUserService : IAppUserService, IService
	{
		public string GetUserId()
		{
			return string.Empty;
		}

		public string GetUserName()
		{
			return string.Empty;
		}
	}
}
