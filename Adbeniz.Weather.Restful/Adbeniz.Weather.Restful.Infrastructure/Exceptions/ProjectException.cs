using System;
using System.Collections.Generic;
using Adbeniz.Weather.Restful.Infrastructure.Bootstrappers;
using Adbeniz.Weather.Restful.Infrastructure.Models;

namespace Adbeniz.Weather.Restful.Infrastructure.Exceptions
{
    public class ProjectException : ApplicationException
	{
		protected ErrorModel ErrorModel { get; set; }

		public ProjectException()
		{
		}

		public ProjectException(string message) : base(message)
		{
		}

		public ProjectException(string message, Exception inner) : base(message, inner)
		{
		}

		public bool WithLogError { get; set; } = false;

		public IList<ValidationError> ValidationError { get; set; }

		public string Module { get; set; }

		public string Detail { get; set; }
	}
}
