using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Haken.PerformanceTuningDotnetCoreDemos.CodingAndAlgorithms
{
	[ShortRunJob]
	public class Caching
    {
		[Benchmark]
		public string GetValueDirect()
		{
			return PickupValue();
		}

		[Benchmark]
		public string GetValueCached()
		{
			string cacheKey = "something";

			string result = (string)cache.Get(cacheKey);		  // double-checked locking
			if (result == null)
			{
				lock (cacheLock)
				{
					result = (string)cache.Get(cacheKey);
					if (result == null)
					{
						result = PickupValue();
						cache.Set(cacheKey, result, DateTimeOffset.Now.AddMinutes(1));
					}
				}
			}

			return result;
		}

		private string PickupValue()
		{
			Thread.Sleep(20);       // DB call / remote API call / expensive computation / ...
			return "value";
		}

		private ObjectCache cache = new MemoryCache("my");
		private object cacheLock = new object();
    }
}

//|         Method |            Mean |            Error |         StdDev |
//|--------------- |----------------:|-----------------:|---------------:|
//| GetValueDirect | 20,680,722.9 ns | 1,786,235.265 ns | 97,909.5822 ns |
//| GetValueCached |        145.9 ns |         2.259 ns |      0.1238 ns |