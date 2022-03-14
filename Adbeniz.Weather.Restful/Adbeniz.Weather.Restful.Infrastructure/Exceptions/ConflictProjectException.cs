using System;

namespace Adbeniz.Weather.Restful.Infrastructure.Exceptions
{
    public class ConflictProjectException : ProjectException
    {
		public ConflictProjectException()
		{
		}

		public ConflictProjectException(string message)
			: base(message)
		{
		}

		public ConflictProjectException(string message, Exception inner)
			: base(message, inner)
		{
		}
    }
}
