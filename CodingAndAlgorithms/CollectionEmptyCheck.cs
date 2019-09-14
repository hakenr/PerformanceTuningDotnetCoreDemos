using BenchmarkDotNet.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haken.PerformanceTuningDotnetCoreDemos.CodingAndAlgorithms
{
	[SimpleJob(warmupCount: 1, launchCount: 1, targetCount: 5, invocationCount: 10000)]
	public class CollectionEmptyCheck
	{
		[Params(100, 1_000, 10_000, 50_000)]
		public int CollectionSize { get; set; }

		[Benchmark]
		public bool List_IsEmpty_Count()
		{
			return list.Count() > 0;
		}

		[Benchmark]
		public bool List_IsEmpty_Any()
		{
			return list.Any();
		}

		[Benchmark]
		public bool IEnumerable_IsEmpty_Count()
		{
			return ienumerable.Count() > 0;
		}

		[Benchmark]
		public bool IEnumerable_IsEmpty_Any()
		{
			return ienumerable.Any();
		}

		private List<Guid> list;
		private IEnumerable<Guid> ienumerable => list.Select(i => i);

		[IterationSetup]
		public void IterationSetup()
		{
			list = new List<Guid>();
			foreach (var guid in Enumerable.Range(0, CollectionSize).Select(g => Guid.NewGuid()))
			{
				list.Add(guid);
			}
		}
	}
}

// Count() is faster for scenarios, where you have a data-type, which knows it's length and is able to return it immediately.
// Any() is safer for generic scenarios, where it only checks existence of the first item.

//|                    Method | CollectionSize |           Mean |         Error |      StdDev |         Median |
//|-------------------------- |--------------- |---------------:|--------------:|------------:|---------------:|
//|        List_IsEmpty_Count |            100 |       4.972 ns |     0.3073 ns |   0.0798 ns |       4.940 ns |
//|          List_IsEmpty_Any |            100 |      30.204 ns |     1.9392 ns |   0.5036 ns |      30.230 ns |
//| IEnumerable_IsEmpty_Count |            100 |     332.438 ns |   156.7209 ns |  40.6999 ns |     309.930 ns |
//|   IEnumerable_IsEmpty_Any |            100 |      56.030 ns |    30.8573 ns |   8.0135 ns |      52.400 ns |
//|        List_IsEmpty_Count |           1000 |       5.789 ns |     5.9372 ns |   1.5419 ns |       4.895 ns |
//|          List_IsEmpty_Any |           1000 |      34.753 ns |    21.3905 ns |   5.5550 ns |      31.655 ns |
//| IEnumerable_IsEmpty_Count |           1000 |   2,637.594 ns |   222.4354 ns |  57.7658 ns |   2,643.780 ns |
//|   IEnumerable_IsEmpty_Any |           1000 |      63.209 ns |    38.0215 ns |   9.8741 ns |      60.805 ns |
//|        List_IsEmpty_Count |          10000 |       5.776 ns |     7.1768 ns |   1.8638 ns |       4.950 ns |
//|          List_IsEmpty_Any |          10000 |      33.320 ns |    23.4220 ns |   6.0826 ns |      29.200 ns |
//| IEnumerable_IsEmpty_Count |          10000 |  25,152.105 ns | 3,276.6937 ns | 850.9470 ns |  24,932.035 ns |
//|   IEnumerable_IsEmpty_Any |          10000 |      51.906 ns |    21.1783 ns |   5.4999 ns |      50.360 ns |
//|        List_IsEmpty_Count |          50000 |       5.039 ns |     1.2053 ns |   0.3130 ns |       4.845 ns |
//|          List_IsEmpty_Any |          50000 |      40.822 ns |    53.1023 ns |  13.7905 ns |      31.910 ns |
//| IEnumerable_IsEmpty_Count |          50000 | 123,116.129 ns | 3,212.2154 ns | 834.2021 ns | 123,084.405 ns |
//|   IEnumerable_IsEmpty_Any |          50000 |      54.646 ns |    22.2811 ns |   5.7863 ns |      51.050 ns |