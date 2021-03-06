using System;

namespace Adbeniz.Weather.Restful.Infrastructure.Exceptions
{
    public class TimeoutProjectException : ProjectException
	{
		public TimeoutProjectException() { }

		public TimeoutProjectException(string message) : base(message) { }

		public TimeoutProjectException(string message, Exception inner) : base(message, inner) { }
	}
}
