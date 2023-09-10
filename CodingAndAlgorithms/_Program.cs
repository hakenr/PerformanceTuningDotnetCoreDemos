﻿using BenchmarkDotNet.Running;

namespace Haken.PerformanceTuningDotnetCoreDemos.CodingAndAlgorithms
{
	public class Program
	{
		public static void Main(string[] args)
		{
			// !! Quit Microsoft Teams as it eats your CPU !!

			// strings
			BenchmarkRunner.Run<StringConcatenation>();
			//BenchmarkRunner.Run<StringBuilderCapacityPreinitialization>();
			// related: 
			//BenchmarkRunner.Run<CollectionCapacityPreinitialization>();
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
			// related: BenchmarkRunner.Run<StringBuilderCapacityPreinitialization>();

			// redundancy
			//BenchmarkRunner.Run<DictionaryPickupRedundantChecks>();
			//BenchmarkRunner.Run<DictionaryRemoveRedundantChecks>();
			//BenchmarkRunner.Run<RedundantCasting>();

			//BenchmarkRunner.Run<BoxingUnboxing>();
			//BenchmarkRunner.Run<Caching>();
			//BenchmarkRunner.Run<Finalization>();

			// exception handling, parsing
			//BenchmarkRunner.Run<TryCatch>();

			// reflection
			//BenchmarkRunner.Run<Reflection>();
			//BenchmarkRunner.Run<ReflectionLayoutTrick>();
		}
	}
}
