using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace WebApi.Controllers
{
	[ApiController]
	public class ValuesController : ControllerBase
	{
		[HttpGet("api/values")]
		public ActionResult<IEnumerable<string>> GetDirect()
		{
			return PickupData();
		}

		// https://docs.microsoft.com/en-us/aspnet/core/performance/caching/middleware?view=aspnetcore-2.2
		[ResponseCache(Duration = 10, Location = ResponseCacheLocation.Any)]
		[HttpGet("api/values/responsecache")]
		public ActionResult<IEnumerable<string>> GetWithResponseCache()
		{
			return PickupData();
		}

		[HttpGet("api/values/datacache")]
		public ActionResult<IEnumerable<string>> GetWithDataCache([FromServices]IMemoryCache cache)
		{
			const string cacheKey = "something";

			var result = cache.Get<string[]>(cacheKey);          // double-checked locking pattern
			if (result == null)
			{
				lock (cacheLock)
				{
					result = cache.Get<string[]>(cacheKey);
					if (result == null)
					{
						result = PickupData();
						cache.Set(cacheKey, result, DateTimeOffset.Now.AddMinutes(1));
					}
				}
			}

			return result;
		}
		private static object cacheLock = new object();


		private string[] PickupData()
		{
			Thread.Sleep(90); // simulates DB call, WebApi call, expensive computation, ...
			return new string[] { "value1", "value2" };
		}
	}
}
