using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haken.PerformanceTuningDotnetCoreDemos.CodingAndAlgorithms
{
	[SimpleJob(warmupCount: 1, launchCount: 1, targetCount: 10, invocationCount: 100_000_000)]
	public class BoxingUnboxing
    {
        [Benchmark]
		public int WithBoxing()
		{
			int  a = 10;						// primitive value-type in stack
			int r = DoSomethingObject(a);       // has to be boxed to reference-type object
			return r;
		}
		private int DoSomethingObject(object o)
		{
			return (int)o;						// and unboxed to int as we want to use it again
		}


		[Benchmark]
		public int WithoutBoxing()
		{
			int a = 10;
			int r = DoSomethingInt(a);
			return r;
		}
		private int DoSomethingInt(int o)
		{
			return o;
		}


		[Benchmark]
		public int Generic()					// about the same IL code as without boxing
		{
			int a = 10;
			int r = DoSomethingGeneric(a);
			return r;
		}
		private T DoSomethingGeneric<T>(T o)
		{
			return o;
		}
	}
}
