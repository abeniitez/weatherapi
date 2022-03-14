using System.Collections.Generic;
using Adbeniz.Weather.Restful.Infrastructure.Bootstrappers;
using Newtonsoft.Json;

namespace Adbeniz.Weather.Restful.Infrastructure.Models
{
	public class ErrorModel
    {
		public int Code { get; set; }

		public string Message { get; set; }

		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string Detail { get; set; }

		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string Module { get; set; }

		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public IList<ValidationError> ValidationError { get; set; }
    }
}

