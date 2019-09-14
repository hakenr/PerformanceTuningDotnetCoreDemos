using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haken.PerformanceTuningDotnetCoreDemos.CodingAndAlgorithms
{
	[SimpleJob(warmupCount: 1, launchCount: 1, targetCount: 10, invocationCount: 1_000_000)]
	[MemoryDiagnoser]
	public class Finalization
    {
		[Benchmark]
		public object NoFinalization()
		{
			return new MyClass();
		}

		[Benchmark]
		public object WithFinalization()
		{
			return new MyClassFinalized();
		}

		private class MyClass
		{
			private byte[] data = new byte[1024];
		}

		private class MyClassFinalized
		{
			private byte[] data = new byte[1024];

			~MyClassFinalized()
			{
				// NOOP
			}
		}
	}
}
//|           Method |        Mean |     Error |    StdDev |  Gen 0 |  Gen 1 |  Gen 2 | Allocated |
//|----------------- |------------:|----------:|----------:|-------:|-------:|-------:|----------:|
//|   NoFinalization |    67.47 ns |  2.454 ns |  1.623 ns | 0.2550 |      - |      - |   1.05 KB |
//| WithFinalization | 1,311.53 ns | 24.923 ns | 13.035 ns | 0.2550 | 0.1270 | 0.0020 |   1.05 KB |