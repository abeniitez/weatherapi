using System.Collections.Generic;
using Adbeniz.Weather.Restful.Infrastructure.Data;

namespace Adbeniz.Weather.Restful.Domain.Entities
{
	public class City: EntityBase
    {
		public string Name { get; set; }
		public string Country { get; set; }
		public bool Enable { get; set; }
		public List<WeatherHistorical> Historics { get; set; }
    }
}