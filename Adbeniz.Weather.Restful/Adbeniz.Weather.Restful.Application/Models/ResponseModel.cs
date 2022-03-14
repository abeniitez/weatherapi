using System;

namespace Adbeniz.Weather.Restful.Application.Models
{
    public class ResponseModel
    {
		public ResponseModel()
		{
		}

		public ResponseModel(string pais, string ciudad, string temperatura, string sensacionTermica, string statusCode)
		{
			Pais = pais;
			Ciudad = ciudad;
			Temperatura = temperatura;
			SensacionTermica = sensacionTermica;

			Error = new Error
			{
				Code = string.Empty,
				Description = string.Empty,
				DescriptionCode = string.Empty
			};

			Status = new Status()
			{
				Value = statusCode
			};
		}

		public string Pais { get; set; }
		public string Ciudad { get; set; }
		public string Temperatura { get; set; }
		public string SensacionTermica { get; set; }

		public DateTime SentTime { get; set; }
		public Status Status { get; set; }
		public Error Error { get; set; }


    }

	public class Status
	{
		public string Value { get; set; }
	}

	public class Error
	{
		public string Code { get; set; }
		public string DescriptionCode { get; set; }
		public string Description { get; set; }
	}
}
