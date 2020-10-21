using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace MyDictionary
{
	public class Dictionary<TKey, TValue> : ICollection<KeyValuePair<TKey, TValue>>, IDictionary<TKey, TValue>
	{
		public delegate void EventHandler(string message);
		public event EventHandler Notify;

		KeyValueNode<TKey, TValue> head;
		KeyValueNode<TKey, TValue> tail;

		public int Count { get; private set; }
		public bool IsReadOnly { get; } = false;
		public IEqualityComparer<TKey> Comparer { get; private set; } = EqualityComparer<TKey>.Default;

		public ICollection<TKey> Keys { get; } = new List<TKey>();
		public ICollection<TValue> Values { get; } = new List<TValue>();

		public TValue this[TKey key]
		{
			get
			{
				KeyValueNode<TKey, TValue> current = head;
				while (current != null)
				{
					if (Comparer.Equals(current.Data.Key, key))
						return current.Data.Value;
					current = current.Next;
				}
				throw new KeyNotFoundException();
			}
			set
			{
				Add(key, value);
			}
		}

		public Dictionary()
		{
		}
		public Dictionary(IEqualityComparer<TKey> equalityComparer)
		{
			Comparer = equalityComparer;
		}

		public void Add(KeyValuePair<TKey, TValue> data)
		{
			KeyValueNode<TKey, TValue> node = new KeyValueNode<TKey, TValue>(data);
			if (!Contains(data))
			{
				if (head == null)
					head = node;
				else
					tail.Next = node;
				tail = node;
				Count++;
				Keys.Add(data.Key);
				Values.Add(data.Value);
				Notify?.Invoke($"Added key {data.Key}, value {data.Value}");
			}
			else
			{
				throw new ArgumentException($"Key {data.Key} is already in the dictionary");
			}
		}
		public void Add(TKey key, TValue value)
		{
			Add(new KeyValuePair<TKey, TValue>(key, value));
		}
		public bool Remove(KeyValuePair<TKey, TValue> data)
		{
			return Remove(data.Key);
		}
		public bool Remove(TKey key)
		{
			KeyValueNode<TKey, TValue> current = head;
			KeyValueNode<TKey, TValue> previous = null;

			while (current != null)
			{
				if (Comparer.Equals(current.Data.Key, key))
				{
					if (previous != null)
					{
						previous.Next = current.Next;
						if (current.Next == null)
							tail = previous;
					}
					else
					{
						head = head.Next;
						if (head == null)
							tail = null;
					}
					Count--;
					Values.Remove(this[key]);
					Keys.Remove(key);
					Notify?.Invoke($"Removed key {key}, value {this[key]}");
					return true;
				}

				previous = current;
				current = current.Next;
			}
			return false;
		}
		public void Clear()
		{
			head = null;
			tail = null;
			Count = 0;
			Keys.Clear();
			Values.Clear();
			Notify?.Invoke($"Dictionary cleared");
		}
		public bool Contains(KeyValuePair<TKey, TValue> data)
		{
			return ContainsKey(data.Key);
		}
		public bool ContainsKey(TKey key)
		{
			KeyValueNode<TKey, TValue> current = head;
			while (current != null)
			{
				if (Comparer.Equals(current.Data.Key, key))
					return true;
				current = current.Next;
			}
			return false;
		}
		IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
		{
			KeyValueNode<TKey, TValue> current = head;
			while (current != null)
			{
				yield return current.Data;
				current = current.Next;
			}
		}
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)this).GetEnumerator();
		}
		public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			KeyValueNode<TKey, TValue> current = head;
			int i = arrayIndex;
			while (current != null && i < Count + arrayIndex)
			{
				array[i] = current.Data;
				current = current.Next;
				i++;
			}
		}
		public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
		{
			KeyValueNode<TKey, TValue> current = head;
			while (current != null)
			{
				if (Comparer.Equals(current.Data.Key, key))
				{
					value = current.Data.Value;
					return true;
				}
				current = current.Next;
			}
			throw new KeyNotFoundException();
		}
	}
}