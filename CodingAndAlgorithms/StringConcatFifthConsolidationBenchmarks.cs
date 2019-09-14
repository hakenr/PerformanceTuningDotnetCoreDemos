using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haken.PerformanceTuningDotnetCoreDemos.CodingAndAlgorithms
{
	[SimpleJob(warmupCount: 1, launchCount: 1, targetCount: 200, invocationCount: 1000000)]
	[MemoryDiagnoser]
	public class StringConcatFifthConsolidationBenchmarks
    {
		private string s1 = "Blah";
		private string s2 = "Dooh";
		private string s3 = "Hey!!!!!!!";
		private string s4 = "More...";
		private string s5 = "Yeah";

		[Benchmark]
		public string MultipleConcats()			    // See IL SPY - C# vs. IL code
		{
			var s = s1 + s2 + s3 + s4;			    // Concat(string, string, string, string)
			return s + s5;							// Concat(string, string)	
		}

		[Benchmark]
		public string ConsolidatedConcats()
		{
			return s1 + s2 + s3 + s4 + s5;			// build string[] + Concat(string[])
		}
	}
}

//|              Method |     Mean |     Error |   StdDev |  Gen 0 | Gen 1 | Gen 2 | Allocated |
//|-------------------- |---------:|----------:|---------:|-------:|------:|------:|----------:|
//|     MultipleConcats | 44.86 ns | 0.2618 ns | 1.074 ns | 0.0400 |     - |     - |     168 B |
//| ConsolidatedConcats | 52.26 ns | 0.2751 ns | 1.094 ns | 0.0360 |     - |     - |     152 B |