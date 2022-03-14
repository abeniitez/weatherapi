using System;
using Adbeniz.Weather.Restful.Infrastructure.Data;

namespace Adbeniz.Weather.Restful.Domain.Entities
{
    public class WeatherHistorical : EntityBase
    {
		public string Weather { get; set; }
		public string ThermalSensation { get; set; }
		public virtual int CityId { get; set; }
		public DateTime CreateDate { get; set; }
		public virtual City City { get; set; }
    }
}
