using System.Linq;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Adbeniz.Weather.Restful.ApiService.Installers.FilterSwagger
{
	public class RemoveVersionFromParameter: IOperationFilter
	{
		public void Apply(OpenApiOperation operation, OperationFilterContext context)
		{
			OpenApiParameter versionParameter = operation.Parameters.Single(p => p.Name == "version");
			operation.Parameters.Remove(versionParameter);
		}
	}
}
