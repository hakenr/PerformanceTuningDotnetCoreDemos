using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;

namespace Haken.PerformanceTuningDotnetCoreDemos.CodingAndAlgorithms
{
	[SimpleJob(warmupCount: 1, launchCount: 1, iterationCount: 1)]
	public class SearchInCollection
	{
		[Params(100, 1_000, 10_000, 50_000)]
		public int CollectionSize { get; set; }

		[Benchmark(Description = "List.Contains()      : O(n)")]
		public int Contains()
		{
			// Contains = sekvenční vyhledávání = O(n), též hledání LINQ-to-XY: .Where(), First(), Count(), ... !!!
			return hledane.Count(t => list.Contains(t));
		}


		[Benchmark(Description = "Array.BinarySearch() : O(log(n))")]
		public int BinarySearch()
		{
			// binární půlení = O(log(n))
			return hledane.Count(t => Array.BinarySearch<Guid>(sortedArray, t) >= 0);
		}


		[Benchmark(Description = "Dictionary           : O(1)")]
		public int Dictionary()
		{
			// Dictionary = Hashtable, O(1), též HashSet
			return hledane.Count(t => dictionary.ContainsKey(t));
		}


		[Benchmark(Description = "LINQ.ToLookup()      : O(1)")]
		public int ToLookup()
		{
			// ToLookup = Hashtable, O(1)
			return hledane.Count(i => lookup.Contains(i));
		}


		private List<Guid> list;
		private List<Guid> hledane;
		private Dictionary<Guid, object> dictionary;
		private ILookup<Guid, Guid> lookup;
		private Guid[] sortedArray;

		[IterationSetup]
		public void IterationSetup()
		{
			list = new List<Guid>();
			dictionary = new Dictionary<Guid, object>();
			foreach (var guid in Enumerable.Range(0, CollectionSize).Select(g => Guid.NewGuid()))
			{
				list.Add(guid);
				dictionary.Add(guid, null);
			}
			lookup = list.ToLookup(i => i);
			sortedArray = list.ToArray();
			Array.Sort(sortedArray);

			var rand = new Random();
			hledane = Enumerable.Range(0, CollectionSize / 2).Select(g => (rand.NextDouble() > 0.5) ? Guid.NewGuid() : list[rand.Next(list.Count)]).ToList();
		}
	}
}

//|                             Method | CollectionSize |             Mean | Error |
//|----------------------------------- |--------------- |-----------------:|------:|
//|      'List.Contains() : O(n)     ' |            100 |         8.700 us |    NA |
//| 'Array.BinarySearch() : O(log(n))' |            100 |         6.300 us |    NA |
//|           'Dictionary : O(1)     ' |            100 |         3.600 us |    NA |
//|      'LINQ.ToLookup() : O(1)     ' |            100 |         3.800 us |    NA |
//|      'List.Contains() : O(n)     ' |           1000 |       620.600 us |    NA |
//| 'Array.BinarySearch() : O(log(n))' |           1000 |        55.000 us |    NA |
//|           'Dictionary : O(1)     ' |           1000 |        18.500 us |    NA |
//|      'LINQ.ToLookup() : O(1)     ' |           1000 |        22.400 us |    NA |
//|      'List.Contains() : O(n)     ' |          10000 |    65,675.500 us |    NA |
//| 'Array.BinarySearch() : O(log(n))' |          10000 |       692.300 us |    NA |
//|           'Dictionary : O(1)     ' |          10000 |       354.200 us |    NA |
//|      'LINQ.ToLookup() : O(1)     ' |          10000 |       271.500 us |    NA |
//|      'List.Contains() : O(n)     ' |          50000 | 1,473,767.200 us |    NA |
//| 'Array.BinarySearch() : O(log(n))' |          50000 |     4,018.800 us |    NA |
//|           'Dictionary : O(1)     ' |          50000 |     1,682.300 us |    NA |
//|      'LINQ.ToLookup() : O(1)     ' |          50000 |     2,679.100 us |    NA |