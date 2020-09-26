using System.Collections.Generic;

namespace Lab1
{
	class Program
	{
		static void Main(string[] args)
		{
			var dictionary = new Dictionary<int, string>();
			var dictionaryOf = new System.Collections.Generic.Dictionary<int, string>();
			dictionary.Notify += DisplayMessage;

			dictionary.Add(5, "sssssss");
			System.Console.WriteLine(dictionary.Count);
			dictionary.Remove(5);
			System.Console.WriteLine(dictionary.Count);
			dictionary.Clear();
			dictionary.Add(77, "aaaaaaaa");
			dictionary.Add(new KeyValuePair<int, string>(44, "qweqwe"));
			System.Console.WriteLine(dictionary.Count);
			System.Console.WriteLine(dictionary[44]);
			dictionary[78] = "asdqqqqqq";
			System.Console.WriteLine(dictionary.Count + "\n" + dictionary[78]);


			var dictionaryList = new DictionaryList<int, string>();
			dictionary.Notify += DisplayMessage;

			dictionaryList.Add(55, "asdasdad");
			System.Console.WriteLine("\n");
			System.Console.WriteLine(dictionaryList[55]);
		}
		static void DisplayMessage(string message)
		{
			System.Console.WriteLine(message);
		}
	}
}
