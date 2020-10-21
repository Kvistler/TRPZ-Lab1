using System.Collections.Generic;

namespace Lab1
{
	class Program
	{
		static void Main(string[] args)
		{
			var dictionary = new Dictionary.Dictionary<int, string>();
			dictionary.Notify += DisplayMessage;

			dictionary.Add(55, "asdasdad");
			System.Console.WriteLine("\n");
			System.Console.WriteLine(dictionary[55]);
			dictionary.Remove(55);

			var list = dictionary.Keys;
			System.Console.WriteLine(list.Contains(55));

			System.Console.WriteLine();
			System.Console.WriteLine();

			dictionary.Add(1, "111");
			dictionary.Add(2, "222");
			dictionary.Add(3, "333");
			KeyValuePair<int, string>[] array = {
				new KeyValuePair<int, string>(5, "555"),
				new KeyValuePair<int, string>(6, "555"),
				new KeyValuePair<int, string>(7, "555"),
			};

			dictionary.CopyTo(array, 1);

			foreach (var item in array)
			{
				System.Console.WriteLine(item);
			}
		}
		static void DisplayMessage(string message)
		{
			System.Console.WriteLine(message);
		}
	}
}
