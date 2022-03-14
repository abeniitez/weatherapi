using System;
using System.Threading.Tasks;
using Adbeniz.Weather.Restful.Infrastructure.Services;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Adbeniz.Weather.Restful.Application.Services
{
	public interface IResponseCacheService
	{
		Task CacheResponseAsync(string cacheKey, object response, TimeSpan timeTimeLive);
		Task<string> GetCachedResponseAsync(string cacheKey);
	}

    public class ResponseCacheService: IResponseCacheService, IService
	{
		private readonly IDistributedCache distributedCache;

		public ResponseCacheService(IDistributedCache distributedCache)
		{
			this.distributedCache = distributedCache;
		}

		public async Task CacheResponseAsync(string cacheKey, object response, TimeSpan timeTimeLive)
		{
			if (response == null)
			{
				return;
			}

			var serializedResponse = JsonConvert.SerializeObject(response);

			await distributedCache.SetStringAsync(cacheKey, serializedResponse, new DistributedCacheEntryOptions
			{
				AbsoluteExpirationRelativeToNow = timeTimeLive
			});
		}

		public async Task<string> GetCachedResponseAsync(string cacheKey)
		{
			var cachedResponse = await distributedCache.GetStringAsync(cacheKey);
			return string.IsNullOrEmpty(cachedResponse) ? null : cachedResponse;
		}
	}
}
