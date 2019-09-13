using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haken.PerformanceTuningDotnetCoreDemos.CodingAndAlgorithms
{
	[SimpleJob(warmupCount: 1, launchCount: 1, targetCount: 1)]
	[MemoryDiagnoser]
	public class StringConcatenationBenchmarks
	{
		[Params(1, 2, 3, 50, 100, 1000, 100_000)]
		public int Concatenations { get; set; }

		[Benchmark]
		public string StringConcat()
		{
			var str = string.Empty;

			for (int i = 0; i < Concatenations; i++)
			{
				str += "A";
			}

			return str;
		}

		[Benchmark]
		public string StringBuilder()
		{
			var sb = new StringBuilder();

			for (int i = 0; i < Concatenations; i++)
			{
				sb.Append("A");
			}

			return sb.ToString();
		}
	}
}

//|        Method | Concatenations |                 Mean | Error |        Gen 0 |        Gen 1 |        Gen 2 |     Allocated |
//|-------------- |--------------- |---------------------:|------:|-------------:|-------------:|-------------:|--------------:|
//|  StringConcat |              1 |             2.631 ns |    NA |            - |            - |            - |             - |
//| StringBuilder |              1 |            31.953 ns |    NA |       0.0324 |            - |            - |         136 B |
//|  StringConcat |              2 |            22.231 ns |    NA |       0.0076 |            - |            - |          32 B |
//| StringBuilder |              2 |            36.523 ns |    NA |       0.0324 |            - |            - |         136 B |
//|  StringConcat |              3 |            42.754 ns |    NA |       0.0152 |            - |            - |          64 B |
//| StringBuilder |              3 |            41.424 ns |    NA |       0.0324 |            - |            - |         136 B |
//|  StringConcat |             50 |         1,301.984 ns |    NA |       0.9441 |            - |            - |        3968 B |
//| StringBuilder |             50 |           376.232 ns |    NA |       0.1121 |            - |            - |         472 B |
//|  StringConcat |            100 |         3,184.552 ns |    NA |       3.0899 |            - |            - |       12968 B |
//| StringBuilder |            100 |           677.129 ns |    NA |       0.1841 |            - |            - |         776 B |
//|  StringConcat |           1000 |       144,060.449 ns |    NA |     245.3613 |            - |            - |     1029968 B |
//| StringBuilder |           1000 |         4,653.029 ns |    NA |       1.0910 |            - |            - |        4584 B |
//|  StringConcat |         100000 | 1,346,306,200.000 ns |    NA | 2973000.0000 | 2546000.0000 | 2546000.0000 | 10002999968 B |
//| StringBuilder |         100000 |       440,806.348 ns |    NA |      62.0117 |      62.0117 |      62.0117 |      410000 B |
