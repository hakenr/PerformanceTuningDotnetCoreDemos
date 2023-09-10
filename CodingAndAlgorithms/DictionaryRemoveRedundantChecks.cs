using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;

namespace Haken.PerformanceTuningDotnetCoreDemos.CodingAndAlgorithms
{
	[SimpleJob(warmupCount: 1, launchCount: 1, iterationCount: 100)]
	public class DictionaryRemoveRedundantChecks
	{
		private const int DictionarySize = 100_000;

		[Benchmark]
		public void RemoveRedundancy()
		{
			foreach (var key in keys)
			{
				if (dictionary.ContainsKey(key))
				{
					dictionary.Remove(key);
				}
			}
		}

		[Benchmark]
		public void SimpleRemove()
		{
			foreach (var key in keys)
			{
				dictionary.Remove(key);  // Remove method returns false if key is not found
			}
		}


		private Dictionary<Guid, string> dictionary;
		private List<Guid> keys;
		public IEnumerable<object> GetKeys() => keys.Cast<object>();

		[IterationSetup]
		public void IterationSetup()
		{
			keys = new List<Guid>();
			dictionary = new Dictionary<Guid, string>();
			foreach (var guid in Enumerable.Range(0, DictionarySize).Select(g => Guid.NewGuid()))
			{
				keys.Add(guid);
				dictionary.Add(guid, "item");
			}
		}
	}
}

//|           Method |     Mean |     Error |    StdDev |   Median |
//|----------------- |---------:|----------:|----------:|---------:|
//| RemoveRedundancy | 7.481 ms | 0.2388 ms | 0.6890 ms | 7.257 ms |
//|     SimpleRemove | 5.247 ms | 0.1837 ms | 0.5330 ms | 5.014 ms |