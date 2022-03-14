using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Adbeniz.Weather.Restful.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Adbeniz.Weather.Restful.ApiService.Filters
{
    public class ValidationFilter: IAsyncActionFilter
	{
		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			if (!context.ModelState.IsValid)
			{
				KeyValuePair<string, IEnumerable<string>>[] errorsInModelState = context.ModelState
					.Where(x => x.Value.Errors.Count > 0)
					.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(x => x.ErrorMessage)).ToArray();

				var errorResponse = new ErrorResponse();

				foreach (KeyValuePair<string, IEnumerable<string>> error in errorsInModelState)
				{
					foreach (var subError in error.Value)
					{
						var errorModel = new ErrorModelValidation
						{
							FieldName = error.Key,
							Message = subError
						};

						errorResponse.Errors.Add(errorModel);
					}
				}

				context.Result = new BadRequestObjectResult(errorResponse);
				return;
			}

			await next();
		}
	}
}
