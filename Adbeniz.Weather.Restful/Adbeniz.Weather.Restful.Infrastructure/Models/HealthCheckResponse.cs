using System;
using System.Collections.Generic;

namespace Adbeniz.Weather.Restful.Infrastructure.Models
{
    public class HealthCheckResponse
    {
		public string Status { get; set; }

		public IEnumerable<HealthCheck> Checks { get; set; }

		public TimeSpan Duration { get; set; }
    }
}
