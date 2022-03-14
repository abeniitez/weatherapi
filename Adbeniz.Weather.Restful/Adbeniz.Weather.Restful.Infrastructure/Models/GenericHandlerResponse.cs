using System;

namespace Adbeniz.Weather.Restful.Infrastructure.Models
{
    public class GenericHandlerResponse<TResponse>
    {
		public GenericHandlerResponse(TResponse values)
		{
			Values = values;
		}

		public TResponse Values { get; }
    }
}
