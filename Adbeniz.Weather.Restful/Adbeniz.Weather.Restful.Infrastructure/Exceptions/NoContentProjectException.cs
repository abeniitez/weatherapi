using System;

namespace Adbeniz.Weather.Restful.Infrastructure.Exceptions
{
    public class NoContentProjectException : ProjectException
	{
		public NoContentProjectException()
		{
		}

		public NoContentProjectException(string message)
			: base(message)
		{
		}

		public NoContentProjectException(string message, Exception inner)
			: base(message, inner)
		{
		}
	}
}
