using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haken.PerformanceTuningDotnetCoreDemos.CodingAndAlgorithms
{
	[SimpleJob(warmupCount: 1, launchCount: 1, targetCount: 1, invocationCount: 100)]
	[MemoryDiagnoser]
	public class CollectionCapacityPreinitialization
    {
		[Params(100, 1_000, 10_000, 100_000, 1_000_000)]
		public int CollectionSize { get; set; }

		[Benchmark]
		public object ListPlain()
		{
			var list = new List<object>();				 // <--- Grows: 0, 4, 8, 16, 32, 64, ...
			for (int i = 0; i < CollectionSize; i++)
			{
				list.Add(item);
			}
			return list;
		}

		[Benchmark]
		public object ListPreinitialized()
		{
			var list = new List<object>(CollectionSize);  // <--- Initial Capacity
			for (int i = 0; i < CollectionSize; i++)
			{
				list.Add(item);
			}
			return list;
		}

		private object item = new object();
	}
}

//|                 Method | CollectionSize |            Mean | Error |    Gen 0 |    Gen 1 |    Gen 2 |  Allocated |
//|----------------------- |--------------- |----------------:|------:|---------:|---------:|---------:|-----------:|
//|              ListPlain |            100 |      1,100.0 ns |    NA |        - |        - |        - |     2200 B |
//| ListSizePreinitialized |            100 |        446.0 ns |    NA |        - |        - |        - |      864 B |
//|              ListPlain |           1000 |      7,020.0 ns |    NA |        - |        - |        - |    16608 B |
//| ListSizePreinitialized |           1000 |      4,266.0 ns |    NA |        - |        - |        - |     8064 B |
//|              ListPlain |          10000 |    103,291.0 ns |    NA |  40.0000 |  40.0000 |  40.0000 |   262464 B |
//| ListSizePreinitialized |          10000 |     44,976.0 ns |    NA |  10.0000 |        - |        - |    80064 B |
//|              ListPlain |         100000 |  1,251,729.0 ns |    NA | 210.0000 | 180.0000 | 180.0000 |  2098141 B |
//| ListSizePreinitialized |         100000 |    538,040.0 ns |    NA | 240.0000 | 240.0000 | 240.0000 |   800064 B |
//|              ListPlain |        1000000 | 10,449,714.0 ns |    NA | 280.0000 | 250.0000 | 250.0000 | 16778242 B |
//| ListSizePreinitialized |        1000000 |  6,200,309.0 ns |    NA | 230.0000 | 230.0000 | 230.0000 |  8001441 B |