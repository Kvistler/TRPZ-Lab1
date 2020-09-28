using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace MyDictionary
{
	public class Dictionary<TKey, TValue> : ICollection<KeyValuePair<TKey, TValue>>, IDictionary<TKey, TValue>
	{
		public delegate void EventHandler(string message);
		public event EventHandler Notify;

		private KeyValuePair<TKey, TValue>[] record = new KeyValuePair<TKey, TValue>[500];
		public TValue this[TKey key]
		{
			get
			{
				for (int i = 0; i < Count; i++)
				{
					if (Comparer.Equals(key, record[i].Key))
					{
						return record[i].Value;
					}
				}
				throw new KeyNotFoundException();
			}
			set
			{
				Add(key, value);
			}
		}

		public int Count { get; private set; } = 0;
		public ICollection<TKey> Keys { get; } = new List<TKey>();
		public ICollection<TValue> Values { get; } = new List<TValue>();
		public IEqualityComparer<TKey> Comparer { get; private set; } = EqualityComparer<TKey>.Default;
		public bool IsReadOnly { get; } = false;

		public Dictionary()
		{
		}
		public Dictionary(IEqualityComparer<TKey> equalityComparer)
		{
			Comparer = equalityComparer;
		}

		public bool ContainsKey(TKey key)
		{
			for (int i = 0; i < Count; i++)
			{
				if (Comparer.Equals(record[i].Key, key))
					return true;
			}
			return false;
		}
		public void Add(TKey key, TValue value)
		{
			if (!ContainsKey(key))
			{
				record[Count] = new KeyValuePair<TKey, TValue>(key, value);
				Count++;
				Keys.Add(key);
				Values.Add(value);
				Notify?.Invoke($"Added key {key}, value {value}");
			}
			else
			{
				throw new ArgumentException($"Key {key} is already in the dictionary");
			}
		}
		public void Add(KeyValuePair<TKey, TValue> item)
		{
			Add(item.Key, item.Value);
		}
		public bool Remove(TKey key)
		{
			for (int i = 0; i < Count; i++)
			{
				if (Comparer.Equals(record[i].Key, key))
				{
					Keys.Remove(key);
					Values.Remove(record[i].Value);
					Notify?.Invoke($"Removed key {key}, value {record[i].Value}");
					record = record.Where(val => !Comparer.Equals(val.Key, key)).ToArray();
					Count--;
					return true;
				}
			}
			return false;
		}
		public bool Remove(KeyValuePair<TKey, TValue> item)
		{
			return Remove(item.Key);
		}
		public void Clear()
		{
			record = new KeyValuePair<TKey, TValue>[500];
			Count = 0;
			Keys.Clear();
			Values.Clear();
			Notify?.Invoke($"Dictionary cleared");
		}
		public bool Contains(KeyValuePair<TKey, TValue> item)
		{
			return ContainsKey(item.Key);
		}
		public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			for (int i = arrayIndex; i < Count + arrayIndex; i++)
			{
				array[i] = record[i];
			}
		}
		IEnumerator IEnumerable.GetEnumerator()
		{
			return record.GetEnumerator();
		}
		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			return GetEnumerator();
		}
		public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
		{
			for (int i = 0; i < Count; i++)
			{
				if (Comparer.Equals(record[i].Key, key))
				{
					value = record[i].Value;
					return true;
				}
			}
			throw new KeyNotFoundException();
		}
	}
}
