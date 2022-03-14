using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Adbeniz.Weather.Restful.Infrastructure.Exceptions;
using Adbeniz.Weather.Restful.Infrastructure.Models;
using Newtonsoft.Json;

namespace Adbeniz.Weather.Restful.Infrastructure.Extensions
{
	public static class ResolveResponseExtension
    {
        public static async Task<string> GetContentWithStatusCodeValidated(this HttpResponseMessage httpResponseMessage)
        {
            var content = await httpResponseMessage.Content.ReadAsStringAsync();

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return content;
            }

            ErrorModel error = JsonConvert.DeserializeObject<ErrorModel>(content);

            if (httpResponseMessage.StatusCode == HttpStatusCode.RequestTimeout)
            {
                throw new TimeoutProjectException(error.Message);
            }

            if (httpResponseMessage.StatusCode == HttpStatusCode.NotFound)
            {
                throw new NotFoundProjectException(error.Message);
            }

            if (httpResponseMessage.StatusCode == HttpStatusCode.Forbidden)
            {
                throw new ForbiddenProjectException(error.Message);
            }

            if (httpResponseMessage.StatusCode == HttpStatusCode.Conflict)
            {
                throw new ConflictProjectException(error.Message);
            }

            throw new BadRequestProjectException(content);
        }
    }
}
