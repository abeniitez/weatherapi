using System.Collections.Generic;
using System.Linq;
using Adbeniz.Weather.Restful.Infrastructure.Bootstrappers;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Adbeniz.Weather.Restful.Infrastructure.Extensions
{
	public static class ModelStateExtension
    {
		public static IList<ValidationError> AllErrors(this ModelStateDictionary modelState)
		{
			var result = new List<ValidationError>();

			var erroneousFields = modelState.Where(ms => ms.Value.Errors.Any())
											.Select(x => new { x.Key, x.Value.Errors });

			foreach (var erroneousField in erroneousFields)
			{
				var fieldKey = erroneousField.Key;

				IEnumerable<ValidationError> fieldErrors = erroneousField.Errors
								   .Select(error => new ValidationError(fieldKey, error.ErrorMessage));

				result.AddRange(fieldErrors);
			}

			return result;
		}
    }
}
