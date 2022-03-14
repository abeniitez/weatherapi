using System;

namespace Adbeniz.Weather.Restful.Infrastructure.Exceptions
{
    public class ServiceUnavailableProjectException : ProjectException
	{
		public ServiceUnavailableProjectException()
		{
		}

		public ServiceUnavailableProjectException(string message)
			: base(message)
		{
		}

		public ServiceUnavailableProjectException(string message, Exception inner)
			: base(message, inner)
		{
		}
	}
}
