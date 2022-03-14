using System;

namespace Adbeniz.Weather.Restful.Infrastructure.Exceptions
{
    public class UnauthorizedAccessProjectException : ProjectException
	{
		public UnauthorizedAccessProjectException()
		{
		}

		public UnauthorizedAccessProjectException(string message) : base(message)
		{
		}

		public UnauthorizedAccessProjectException(string message, Exception inner) : base(message, inner)
		{
		}
	}
}
