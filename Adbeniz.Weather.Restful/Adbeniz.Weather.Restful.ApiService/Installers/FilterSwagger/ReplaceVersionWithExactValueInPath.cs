using System.Collections.Generic;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Adbeniz.Weather.Restful.ApiService.Installers.FilterSwagger
{
	public class ReplaceVersionWithExactValueInPath: IDocumentFilter
	{
		public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
		{
			var paths = new OpenApiPaths();
			foreach (KeyValuePair<string, OpenApiPathItem> path in swaggerDoc.Paths)
			{
				paths.Add(path.Key.Replace("v{version}", swaggerDoc.Info.Version), path.Value);
			}
			swaggerDoc.Paths = paths;
		}
	}
}
