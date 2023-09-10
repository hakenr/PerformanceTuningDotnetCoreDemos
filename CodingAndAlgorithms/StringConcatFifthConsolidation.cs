using BenchmarkDotNet.Attributes;

namespace Haken.PerformanceTuningDotnetCoreDemos.CodingAndAlgorithms
{
	[SimpleJob(warmupCount: 1, launchCount: 1, iterationCount: 200, invocationCount: 1000000)]
	[MemoryDiagnoser]
	public class StringConcatFifthConsolidation
	{
		private string s1 = "Blah";
		private string s2 = "Dooh";
		private string s3 = "Hey!!!!!!!";
		private string s4 = "More...";
		private string s5 = "Yeah";

		[Benchmark]
		public string MultipleConcats()             // See IL SPY - C# vs. IL code
		{
			var s = s1 + s2 + s3 + s4;              // Concat(string, string, string, string)
			return s + s5;                          // Concat(string, string)	
		}

		[Benchmark]
		public string ConsolidatedConcats()
		{
			return s1 + s2 + s3 + s4 + s5;          // build string[] + Concat(string[])
		}
	}
}

//net7
//|				  Method |		Mean |	   Error |    StdDev |    Median |    Gen0 |  Allocated |
//| -------------------- | ---------:| ---------:| ---------:| ---------:| -------:| ----------:|
//| MultipleConcats      |  26.30 ns |  0.212 ns |  0.855 ns |  26.14 ns |  0.0180 |      152 B |
//| ConsolidatedConcats  |  33.90 ns |  0.207 ns |  0.849 ns |  33.85 ns |  0.0170 |      144 B |

//net2.2
//|              Method |     Mean |     Error |   StdDev |  Gen 0 | Gen 1 | Gen 2 | Allocated |
//|-------------------- |---------:|----------:|---------:|-------:|------:|------:|----------:|
//|     MultipleConcats | 44.86 ns | 0.2618 ns | 1.074 ns | 0.0400 |     - |     - |     168 B |
//| ConsolidatedConcats | 52.26 ns | 0.2751 ns | 1.094 ns | 0.0360 |     - |     - |     152 B |