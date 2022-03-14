using System;

namespace Adbeniz.Weather.Restful.Application.Models
{
    public class WeatherByCityNameHandlerRequestModel
    {
		public WeatherByCityNameHandlerRequestModel(string ciudad, bool conHistorico)
		{
			Ciudad = ciudad;
			ConHistorico = conHistorico;
		}

		public string Ciudad { get; set; }
		public bool ConHistorico { get; set; }
    }
}
