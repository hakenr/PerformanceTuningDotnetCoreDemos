using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Running;
using System;

namespace Haken.PerformanceTuningDotnetCoreDemos.CodingAndAlgorithms
{
	public class Program
	{
		public static void Main(string[] args)
		{
			// strings
			//BenchmarkRunner.Run<StringConcatenationBenchmarks>();
			//BenchmarkRunner.Run<SubstringAsSpanBenchmarks>();
			//BenchmarkRunner.Run<StringConcatConsolidationBenchmarks>();
			//BenchmarkRunner.Run<StringConcatConditionConsolidationBenchmarks>();
			BenchmarkRunner.Run<StringConcatFifthConsolidationBenchmarks>();

			// data structures
			//BenchmarkRunner.Run<SearchInCollectionBenchmarks>();

			// reflection
			//BenchmarkRunner.Run<ReflectionBenchmarks>();
			//BenchmarkRunner.Run<ReflectionLayoutTrickBenchmarks>();
		}
	}
}
