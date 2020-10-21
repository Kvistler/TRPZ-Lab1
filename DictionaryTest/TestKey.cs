using System.Collections.Generic;

namespace DictionaryTest
{
	internal class TestKey<TFirst, TSecond>
	{
		public TFirst First { get; set; }
		public TSecond Second { get; set; }
		public TestKey(TFirst first, TSecond second)
		{
			First = first;
			Second = second;
		}
	}

	internal class TestKeyEqualityComparer<TFirst, TSecond> : IEqualityComparer<TestKey<TFirst, TSecond>>
	{
		public bool Equals(TestKey<TFirst, TSecond> x, TestKey<TFirst, TSecond> y)
		{
			return x.First.Equals(y.First) && x.Second.Equals(y.Second);
		}

		public int GetHashCode(TestKey<TFirst, TSecond> obj)
		{
			return obj.GetHashCode();
		}
	}
}
