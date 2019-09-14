using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Running;
using System;

namespace Haken.PerformanceTuningDotnetCoreDemos.CodingAndAlgorithms
{
	public class Program
	{
		public static void Main(string[] args)
		{
			// !! Quit Microsoft Teams as it eats your CPU !!

			// strings
			//BenchmarkRunner.Run<StringConcatenation>();
			//BenchmarkRunner.Run<StringBuilderSizePreinitialization>();
			//BenchmarkRunner.Run<SubstringAsSpan>();
			//BenchmarkRunner.Run<StringConcatConsolidation>();
			//BenchmarkRunner.Run<StringConcatConditionConsolidation>();
			//BenchmarkRunner.Run<StringConcatFifthConsolidation>();

			// data structures
			//BenchmarkRunner.Run<SearchInCollection>();
			//BenchmarkRunner.Run<DictionaryPickupRedundantChecks>();
			//BenchmarkRunner.Run<DictionaryRemoveRedundantChecks>();
			//BenchmarkRunner.Run<CollectionEmptyCheck>();
			//BenchmarkRunner.Run<CollectionCapacityPreinitialization>();
			BenchmarkRunner.Run<StringBuilderCapacityPreinitialization>();

			// reflection
			//BenchmarkRunner.Run<Reflection>();
			//BenchmarkRunner.Run<ReflectionLayoutTrick>();
		}
	}
}
