using BenchmarkDotNet.Running;
using System;

namespace Haken.PerformanceTuningDotnetCoreDemos.CodingAndAlgorithms
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var summmary = BenchmarkRunner.Run<StringConcatenationBenchmarks>();
		}
	}
}
