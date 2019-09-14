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
			//BenchmarkRunner.Run<StringConcatenation>();
			//BenchmarkRunner.Run<SubstringAsSpan>();
			//BenchmarkRunner.Run<StringConcatConsolidation>();
			//BenchmarkRunner.Run<StringConcatConditionConsolidation>();
			//BenchmarkRunner.Run<StringConcatFifthConsolidation>();

			// data structures
			//BenchmarkRunner.Run<SearchInCollection>();
			//BenchmarkRunner.Run<DictionaryPickupRedundantChecks>();
			//BenchmarkRunner.Run<DictionaryRemoveRedundantChecks>();
			BenchmarkRunner.Run<CollectionEmptyCheck>();

			// reflection
			//BenchmarkRunner.Run<Reflection>();
			//BenchmarkRunner.Run<ReflectionLayoutTrick>();
		}
	}
}
