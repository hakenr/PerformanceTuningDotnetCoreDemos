using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;

namespace Haken.PerformanceTuningDotnetCoreDemos.CodingAndAlgorithms
{
	[SimpleJob(warmupCount: 1, launchCount: 1, iterationCount: 100, invocationCount: 10000)]
	public class DictionaryPickupRedundantChecks
	{
		private const int DictionarySize = 100_000;

		[Benchmark]
		public string PickupRedundancy()
		{
			string item = null;

			if (dictionary.ContainsKey(key))
			{
				item = dictionary[key];
			}

			return item;
		}

		[Benchmark]
		public object SimplePickup()
		{
			dictionary.TryGetValue(key, out string item);  // TryGetValue returns true, if key is found

			return item;
		}

		private Dictionary<Guid, string> dictionary;
		private Guid key;

		[IterationSetup]
		public void IterationSetup()
		{
			dictionary = new Dictionary<Guid, string>();
			foreach (var guid in Enumerable.Range(0, DictionarySize).Select(g => Guid.NewGuid()))
			{
				dictionary.Add(guid, "item");
			}
			key = dictionary.Keys.First();
		}
	}
}

//|           Method |     Mean |     Error |   StdDev |
//|----------------- |---------:|----------:|---------:|
//| PickupRedundancy | 32.24 ns | 1.1244 ns | 3.298 ns |
//|     SimplePickup | 16.46 ns | 0.5866 ns | 1.693 ns |