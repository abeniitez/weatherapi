using System;

namespace Adbeniz.Weather.Restful.Infrastructure.Services
{
    public interface IAppDateTime
	{
		DateTime LocalNow { get; }
		DateTime UnspecifiedNow { get; }
		DateTime UtcNow { get; }
	}

	public class AppDateTime : IAppDateTime, IService
	{
		public DateTime LocalNow => DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Local);
		public DateTime UnspecifiedNow => DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
		public DateTime UtcNow => DateTime.UtcNow;
	}
}
