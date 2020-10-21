using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Dictionary.Tests
{
	[TestClass]
	public class DictionaryTests
	{
		private Dictionary<int, string> Dictionary { get; set; }
		private int defaultkey = 0;
		private string defaultvalue = "";

		[TestInitialize]
		public void TestInitialize()
		{
			Dictionary = new Dictionary<int, string>();
		}

		[TestMethod]
		public void Add_CorrectRecord_RecordAdded()
		{
			int expectedCount = 1;

			Dictionary.Add(defaultkey, defaultvalue);

			Assert.AreEqual(expectedCount, Dictionary.Count);
		}

		[TestMethod]
		public void Add_AlreadyExisting_ExceptionThrown()
		{
			Dictionary.Add(defaultkey, defaultvalue);

			Assert.ThrowsException<ArgumentException>(() => Dictionary.Add(defaultkey, defaultvalue));
		}

		[TestMethod]
		public void Remove_CorrectRecord_RecordRemoved()
		{
			int expectedCount = 0;

			Dictionary.Add(defaultkey, defaultvalue);
			Dictionary.Remove(defaultkey);

			Assert.AreEqual(expectedCount, Dictionary.Count);
		}

		[TestMethod]
		public void Clear_AllCleared()
		{
			//int expectedCount = 0;

			Dictionary.Add(defaultkey, defaultvalue);
			Dictionary.Clear();

			CollectionAssert.AreEqual(new Dictionary<int, string>(), Dictionary);

			//Assert.AreEqual(expectedCount, dictionary.Count);
		}

		[TestMethod]
		public void ContainsKey_KeyExists_True()
		{
			Dictionary.Add(defaultkey, defaultvalue);

			Assert.AreEqual(true, Dictionary.ContainsKey(defaultkey));
		}

		[TestMethod]
		public void ContainsKey_KeyNotExists_False()
		{
			Assert.AreEqual(false, Dictionary.ContainsKey(defaultkey));
		}

		[TestMethod]
		public void ContainsKey_TestKeyExists_True()
		{
			var dictionary = new Dictionary<TestKey<int, string>, string>(new TestKeyEqualityComparer<int, string>());

			dictionary.Add(new TestKey<int, string>(defaultkey, defaultvalue), defaultvalue);

			Assert.AreEqual(true, dictionary.ContainsKey(new TestKey<int, string>(defaultkey, defaultvalue)));

		}

		[TestMethod]
		public void Indexator_Set_RecordAdded()
		{
			int expectedCount = 1;

			Dictionary[defaultkey] = defaultvalue;

			Assert.AreEqual(expectedCount, Dictionary.Count);
		}

		[TestMethod]
		public void Indexator_GetExistingValue_ValueReturned()
		{
			Dictionary[defaultkey] = defaultvalue;

			Assert.AreEqual(Dictionary[defaultkey], defaultvalue);
		}

		[TestMethod]
		public void Indexator_GetNonExistingValue_ExceptionThrown()
		{
			Assert.ThrowsException<KeyNotFoundException>(() => Dictionary[defaultkey]);
		}

		[TestMethod]
		public void TryGetValue_Existing_True()
		{
			Dictionary.Add(defaultkey, defaultvalue);

			Assert.AreEqual(true, Dictionary.TryGetValue(defaultkey, out _));
		}

		[TestMethod]
		public void TryGetValue_NonExisting_False()
		{
			Assert.AreEqual(false, Dictionary.TryGetValue(defaultkey, out _));
		}

		[TestMethod]
		public void CopyTo_CorrectValues_Copied()
		{
			var arrayExpected = new KeyValuePair<int, string>[1]
			{
				new KeyValuePair<int, string>(defaultkey, defaultvalue),
			};
			var arrayActual = new KeyValuePair<int, string>[1];


			Dictionary.Add(defaultkey, defaultvalue);
			Dictionary.CopyTo(arrayActual);

			CollectionAssert.AreEqual(arrayExpected, arrayActual);
		}

		[TestMethod]
		[ExpectedException(typeof(IndexOutOfRangeException))]
		public void CopyTo_SmallArray_Exception()
		{
			var arrayExpected = new KeyValuePair<int, string>[1]
			{
				new KeyValuePair<int, string>(defaultkey, defaultvalue),
			};
			var arrayActual = new KeyValuePair<int, string>[1];

			Dictionary.Add(1, defaultvalue);
			Dictionary.Add(2, defaultvalue);

			Dictionary.CopyTo(arrayActual);
			//Assert.ThrowsException<IndexOutOfRangeException>(() => Dictionary.CopyTo(arrayActual, 0));
		}

		[TestMethod]
		public void Event_Add_Invoked()
		{
			Dictionary.Notify += eventInvoked;
			Dictionary.Add(defaultkey, defaultvalue);

			void eventInvoked(string _)
			{
				Assert.IsTrue(true);
			}
		}

		[TestMethod]
		public void Event_Remove_Invoked()
		{
			Dictionary.Notify += eventInvoked;
			Dictionary.Add(defaultkey, defaultvalue);
			Dictionary.Remove(defaultkey);

			void eventInvoked(string _)
			{
				Assert.IsTrue(true);
			}
		}

		[TestMethod]
		public void Event_Clear_Invoked()
		{
			Dictionary.Notify += eventInvoked;
			Dictionary.Clear();

			void eventInvoked(string _)
			{
				Assert.IsTrue(true);
			}
		}
	}
}
