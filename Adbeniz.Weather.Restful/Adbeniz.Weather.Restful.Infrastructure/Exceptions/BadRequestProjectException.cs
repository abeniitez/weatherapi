using System;

namespace Adbeniz.Weather.Restful.Infrastructure.Exceptions
{
    public class BadRequestProjectException : ProjectException
	{
		public BadRequestProjectException()
		{
		}

		public BadRequestProjectException(string message)
			: base(message)
		{
		}

		public BadRequestProjectException(string message, Exception inner)
			: base(message, inner)
		{
		}
	}
}
