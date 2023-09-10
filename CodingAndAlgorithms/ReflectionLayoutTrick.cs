using System.Reflection;
using System.Runtime.InteropServices;
using BenchmarkDotNet.Attributes;

namespace Haken.PerformanceTuningDotnetCoreDemos.CodingAndAlgorithms
{
	[SimpleJob(warmupCount: 1, launchCount: 1, iterationCount: 4)]
	public class ReflectionLayoutTrick
	{
		[Params(1000000)]
		public int Iterations { get; set; }


		[Benchmark]
		public MyClass Reflection()
		{
			for (int i = 0; i < Iterations; i++)
			{
				var fieldInfo = typeof(MyClass).GetField("_myProperty", BindingFlags.NonPublic | BindingFlags.Instance);
				fieldInfo.SetValue(instance, i);
			}
			return instance;
		}


		[Benchmark]
		public MyClass ReflectionWithCachedFieldInfo()
		{
			var fieldInfo = typeof(MyClass).GetField("_myProperty", BindingFlags.NonPublic | BindingFlags.Instance);

			for (int i = 0; i < Iterations; i++)
			{
				fieldInfo.SetValue(instance, i);
			}
			return instance;
		}



		[Benchmark]
		public MyClass ExplicitLayoutReflectionAdapter()
		{
			for (int i = 0; i < Iterations; i++)
			{
				var adapter = new MyClassReflectionAdapter() { O1 = instance };
				adapter.O2.MyProperty = i;
			}
			return instance;
		}

		private MyClass instance = new MyClass();

		public class MyClass
		{
			private int _myProperty;
		}

		// THE TRICK/HACK (credits: https://twitter.com/korifey_ad/status/1169210959426183168)
		public class MyClassWithSameLayout
		{
			public int MyProperty { get; set; }
		}

		[StructLayout(LayoutKind.Explicit)]
		public struct MyClassReflectionAdapter
		{
			[FieldOffset(0)]
			public object O1;

			[FieldOffset(0)]
			public MyClassWithSameLayout O2;
		}
	}
}

//|                          Method | Iterations |         Mean |        Error |       StdDev |
//|-------------------------------- |----------- |-------------:|-------------:|-------------:|
//|                      Reflection |    1000000 | 162,520.1 us | 29,618.44 us | 4,583.486 us |
//|   ReflectionWithCachedFieldInfo |    1000000 |  98,069.3 us |  4,783.20 us |   740.206 us |
//| ExplicitLayoutReflectionAdapter |    1000000 |     604.3 us |     26.51 us |     4.103 us |