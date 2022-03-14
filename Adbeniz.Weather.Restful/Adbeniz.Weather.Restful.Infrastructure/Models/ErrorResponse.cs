using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Adbeniz.Weather.Restful.Infrastructure.Models
{
    public class ErrorResponse
    {
		public ErrorResponse() { }
		public ErrorResponse(ErrorModelValidation error)
		{
			Errors.Add(error);
		}

		public List<ErrorModelValidation> Errors { get; set; } = new List<ErrorModelValidation>();
    }
}

