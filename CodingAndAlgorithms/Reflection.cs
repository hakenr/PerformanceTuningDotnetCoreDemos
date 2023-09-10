using BenchmarkDotNet.Attributes;

namespace Haken.PerformanceTuningDotnetCoreDemos.CodingAndAlgorithms
{
	[SimpleJob(warmupCount: 1, launchCount: 1, iterationCount: 5)]
	public class Reflection
	{
		[Params(1000000)]
		public int Iterations { get; set; }


		[Benchmark]
		public MyClass DirectAssignment()
		{
			for (int i = 0; i < Iterations; i++)
			{
				instance.MyProperty = i;
			}

			return instance;
		}


		[Benchmark]
		public MyClass SimpleReflection()
		{
			for (int i = 0; i < Iterations; i++)
			{
				var propInfo = typeof(MyClass).GetProperty("MyProperty");
				propInfo.SetValue(instance, i, null);
			}
			return instance;
		}


		[Benchmark]
		public MyClass ReflectionWithCachedPropertyInfo()
		{
			var propInfo = typeof(MyClass).GetProperty("MyProperty");

			for (int i = 0; i < Iterations; i++)
			{
				propInfo.SetValue(instance, i, null);
			}
			return instance;
		}

		[Benchmark]
		public MyClass Dynamic()
		{
			dynamic dyn = instance;
			for (int i = 0; i < Iterations; i++)
			{
				dyn.MyProperty = i;
			}
			return instance;
		}


		private MyClass instance = new MyClass();

		public class MyClass
		{
			public int MyProperty { get; set; }
		}
	}
}

//|                           Method | Iterations |         Mean |        Error |       StdDev |
//|--------------------------------- |----------- |-------------:|-------------:|-------------:|
//|                 DirectAssignment |    1000000 |     527.5 us |     8.385 us |     2.178 us |
//|                 SimpleReflection |    1000000 | 219,436.0 us | 4,135.933 us | 1,074.089 us |
//| ReflectionWithCachedPropertyInfo |    1000000 | 147,181.5 us | 1,873.153 us |   486.452 us |
//|                          Dynamic |    1000000 |   5,752.0 us |   113.322 us |    29.429 us |