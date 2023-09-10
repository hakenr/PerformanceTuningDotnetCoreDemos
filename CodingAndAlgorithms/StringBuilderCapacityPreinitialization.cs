using System.Text;
using BenchmarkDotNet.Attributes;

namespace Haken.PerformanceTuningDotnetCoreDemos.CodingAndAlgorithms
{
	[SimpleJob(warmupCount: 1, launchCount: 1, iterationCount: 10, invocationCount: 10)]
	[MemoryDiagnoser]
	public class StringBuilderCapacityPreinitialization
	{
		[Params(100, 1_000, 10_000, 100_000, 500_000)]
		public int Size { get; set; }

		[Benchmark]
		public object StringBuilderPlain()
		{
			var sb = new StringBuilder();
			for (int i = 0; i < Size; i++)
			{
				sb.Append(item);
			}
			return sb;
		}

		[Benchmark]
		public object StringBuilderPreinitialized()
		{
			var sb = new StringBuilder(Size * item.Length); // <---- Initial Capacity
			for (int i = 0; i < Size; i++)
			{
				sb.Append(item);
			}
			return sb;
		}

		private const string item = "AAAAAABBBBBBBBBBBCCCCCCCCCCCCCCCDDDDDDDDDDDDDDDDDDDEEEEEEEEEEEEEEEFFFFFFFFFFFFF";
	}
}

//|             Method |   Size |          Mean |         Error |      StdDev |      Gen 0 |     Gen 1 |     Gen 2 |   Allocated |
//|------------------- |------- |--------------:|--------------:|------------:|-----------:|----------:|----------:|------------:|
//|              Plain |    100 |      2.872 us |     0.8337 us |   0.5514 us |          - |         - |         - |    20.39 KB |
//| SizePreinitialized |    100 |      4.739 us |     3.4581 us |   2.0579 us |          - |         - |         - |     15.5 KB |
//|              Plain |   1000 |     21.023 us |     9.7450 us |   5.7991 us |          - |         - |         - |   161.65 KB |
//| SizePreinitialized |   1000 |     16.419 us |     0.4529 us |   0.2369 us |          - |         - |         - |   154.37 KB |
//|              Plain |  10000 |    590.448 us |   122.7981 us |  81.2233 us |   200.0000 |  100.0000 |         - |  1558.53 KB |
//| SizePreinitialized |  10000 |    491.339 us |   161.5632 us | 106.8640 us |   200.0000 |  200.0000 |  200.0000 |  1543.04 KB |
//|              Plain | 100000 | 14,883.093 us |   851.6420 us | 506.7985 us |  2900.0000 | 1300.0000 |  400.0000 | 15513.82 KB |
//| SizePreinitialized | 100000 |  4,655.937 us |   427.9260 us | 254.6519 us |   300.0000 |  300.0000 |  300.0000 | 15429.76 KB |
//|              Plain | 500000 | 82,677.844 us | 1,093.3673 us | 723.1948 us | 13700.0000 | 7000.0000 | 1100.0000 | 77512.88 KB |
//| SizePreinitialized | 500000 | 35,666.473 us |   772.1598 us | 510.7359 us |   200.0000 |  200.0000 |  200.0000 | 77148.51 KB |
