using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haken.PerformanceTuningDotnetCoreDemos.CodingAndAlgorithms
{
	[SimpleJob(warmupCount: 1, launchCount: 1, targetCount: 30, invocationCount: 1_000_000)]
	public class RedundantCasting
    {
        [Benchmark]
		public string DoubleCasting()
		{
			if (obj is string)
			{
				return (string)obj;
			}
			return "default";
		}

		[Benchmark]
		public string AsCasting()
		{
			var s = obj as string;
			if (s != null)
			{
				return s;
			}
			return "default";
		}


		[Benchmark]
		public string IsInlineCasting()
		{
			if (obj is string s)
			{
				return s;
			}
			return "default";
		}

		private object obj = "Blaah";
    }
}

//|          Method |      Mean |     Error |    StdDev |
//|---------------- |----------:|----------:|----------:|
//|   DoubleCasting | 1.5354 ns | 0.3877 ns | 0.5803 ns |
//|       AsCasting | 0.9594 ns | 0.2480 ns | 0.3711 ns |
//| IsInlineCasting | 0.7779 ns | 0.2015 ns | 0.2890 ns |