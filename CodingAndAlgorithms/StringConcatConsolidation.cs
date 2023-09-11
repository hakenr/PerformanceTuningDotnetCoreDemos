﻿using BenchmarkDotNet.Attributes;

namespace Haken.PerformanceTuningDotnetCoreDemos.CodingAndAlgorithms
{
	[SimpleJob(warmupCount: 1, launchCount: 1, iterationCount: 200, invocationCount: 1000000)]
	[MemoryDiagnoser]
	public class StringConcatConsolidation
	{
		private string s1 = "Blah";
		private string s2 = "Dooh";
		private string s3 = "Hey!!!!!!!";

		[Benchmark]
		public string MultipleConcats()
		{
			var s = s1 + s2;
			return s + s3;
		}

		[Benchmark]
		public string ConsolidatedConcats()
		{
			return s1 + s2 + s3;
		}
	}
}

// https://learn.microsoft.com/en-us/dotnet/api/system.string.concat?view=net-7.0

//|              Method |     Mean |     Error |    StdDev |  Gen 0 | Gen 1 | Gen 2 | Allocated |
//|-------------------- |---------:|----------:|----------:|-------:|------:|------:|----------:|
//|     MultipleConcats | 30.71 ns | 0.1997 ns | 0.8191 ns | 0.0260 |     - |     - |     112 B |
//| ConsolidatedConcats | 20.40 ns | 0.1674 ns | 0.6901 ns | 0.0150 |     - |     - |      64 B |