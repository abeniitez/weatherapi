using System;

namespace Adbeniz.Weather.Restful.Application.Models
{
    public class WeatherResponseModel
    {
		public string Country { get; set; }
		public string City { get; set; }
		public string Weather { get; set; }
		public string ThermalSensation { get; set; }
    }
}
