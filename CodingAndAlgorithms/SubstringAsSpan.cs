using System;
using System.Text;
using BenchmarkDotNet.Attributes;

namespace Haken.PerformanceTuningDotnetCoreDemos.CodingAndAlgorithms
{
	[SimpleJob(warmupCount: 1, launchCount: 1, iterationCount: 200, invocationCount: 100000)]
	[MemoryDiagnoser]
	public class SubstringAsSpan
	{
		private string inputString = "BlahBlahBlahBlahFooBarDaahBlahBlahBlahBlahFooBarDaahBlahBlahBlahBlahFooBarDaah";
		private StringBuilder sb;

		[Benchmark]
		public StringBuilder Substring()
		{
			sb.Append(inputString.Substring(5, 20));

			return sb;
		}

		[Benchmark(Baseline = true)]
		public StringBuilder AsSpan()
		{
			sb.Append(inputString.AsSpan(5, 20));

			return sb;
		}

		[IterationSetup]
		public void IterationSetup()
		{
			sb = new StringBuilder();
		}
	}
}

// https://github.com/dotnet/corefx/issues/40739
// any time string.Substring is used as an argument to something where there's an equivalent overload that takes a ReadOnlySpan<char>
// (e.g. StringBuilder.Append(string) vs StringBuilder.Append(ReadOnlySpan<char>))

//|    Method |     Mean |     Error |    StdDev |   Median | Ratio | RatioSD |  Gen 0 |  Gen 1 | Gen 2 | Allocated |
//|---------- |---------:|----------:|----------:|---------:|------:|--------:|-------:|-------:|------:|----------:|
//| Substring | 47.22 ns | 1.7734 ns | 7.4698 ns | 44.61 ns |  4.26 |    0.71 | 0.0200 | 0.0100 |     - |     112 B |
//|    AsSpan | 11.17 ns | 0.2275 ns | 0.8990 ns | 10.86 ns |  1.00 |    0.00 |      - |      - |     - |      40 B |
