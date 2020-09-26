using System.Collections.Generic;

namespace Lab1
{
	public class KeyValueNode<TKey, TValue>
	{
		public KeyValueNode(KeyValuePair<TKey, TValue> data)
		{
			Data = data;
		}
		public KeyValuePair<TKey, TValue> Data { get; set; }
		public KeyValueNode<TKey, TValue> Next { get; set; }
	}
}
