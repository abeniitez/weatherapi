using System;

namespace Adbeniz.Weather.Restful.Infrastructure.Exceptions
{
   public class ForbiddenProjectException : ProjectException
	{
		public ForbiddenProjectException()
		{
		}

		public ForbiddenProjectException(string message) : base(message)
		{
		}

		public ForbiddenProjectException(string message, Exception inner) : base(message, inner)
		{
		}
	}
}
