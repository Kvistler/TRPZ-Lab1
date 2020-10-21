using System;
using System.Collections;
using System.Collections.Generic;

namespace Dictionary
{
	public class Dictionary<TKey, TValue> : ICollection<KeyValuePair<TKey, TValue>>, IDictionary<TKey, TValue>, ICollection
	{
		public delegate void EventHandler(string message);
		public event EventHandler Notify;

		KeyValueNode<TKey, TValue> head;
		KeyValueNode<TKey, TValue> tail;

		public int Count { get; private set; }
		public IEqualityComparer<TKey> Comparer { get; private set; } = EqualityComparer<TKey>.Default;

		public ICollection<TKey> Keys { get; } = new List<TKey>();
		public ICollection<TValue> Values { get; } = new List<TValue>();

		public bool IsSynchronized { get; } = false;
		public bool IsReadOnly { get; } = false;
		public object SyncRoot { get; }

		public TValue this[TKey key]
		{
			get
			{
				TValue value;
				if (TryGetValue(key, out value))
					return value;
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
					Values.Remove(this[key]);
					Keys.Remove(key);
					Count--;
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
					Notify?.Invoke($"Removed key {key}");
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
			KeyValueNode<TKey, TValue> current = head;
			while (current != null)
			{
				yield return current.Data;
				current = current.Next;
			}
		}
		public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex = 0)
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
		public void CopyTo(Array array, int index = 0)
		{
			CopyTo((KeyValuePair<TKey, TValue>[])array, index);
		}
		public bool TryGetValue(TKey key, out TValue value)
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
			value = default;
			return false;
		}
	}
}