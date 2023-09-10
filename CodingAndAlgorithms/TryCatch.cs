using BenchmarkDotNet.Attributes;

namespace Haken.PerformanceTuningDotnetCoreDemos.CodingAndAlgorithms
{
	[SimpleJob(warmupCount: 1, launchCount: 1, iterationCount: 30, invocationCount: 10_000)]
	public class TryCatch
	{
		[Params("123", "invalid")]
		public string Input { get; set; }

		[Benchmark]
		public int TryCatchParse()
		{
			try
			{
				return int.Parse(Input);
			}
			catch
			{
				return 0;
			}
		}


		[Benchmark]
		public int TryParse()
		{
			if (int.TryParse(Input, out int result))
			{
				return result;
			}
			return 0;
		}
	}
}

//|          Method |   Input |         Mean |      Error |     StdDev |       Median |
//|---------------- |-------- |-------------:|-----------:|-----------:|-------------:|
//|   TryCatchParse |     123 |     65.29 ns |   3.949 ns |   5.788 ns |     62.05 ns |
//|        TryParse |     123 |     72.32 ns |   2.972 ns |   4.262 ns |     70.57 ns |
//|   TryCatchParse | invalid | 25,339.76 ns | 162.527 ns | 243.263 ns | 25,244.92 ns |
//|        TryParse | invalid |     53.57 ns |   3.995 ns |   5.600 ns |     51.37 ns |