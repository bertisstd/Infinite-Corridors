
namespace Bertis
{
	using System;
	using System.Collections.Generic;

	static public class CollectionExtension
	{
		static private int BinarySearchInternal<TL, TS>(
			IList<TL>         list,
			TS                searching,
			Func<TL, TS, int> compare,
			int               startIndex,
			int               length)
		{
			var low = startIndex;
			var high = startIndex + length - 1;

			while (low <= high)
			{
				var i = low + ((high - low) >> 1);
				var comp = compare(list[i], searching);

				if (comp == 0)
				{
					return i;
				}
				else if (comp < 0)
				{
					low = i + 1;
				}
				else
				{
					high = i - 1;
				}
			}

			return ~low;
		}

		static public int BinarySearch<TL, TS>(this
		IList<TL>         list,
		TS                searching,
		Func<TL, TS, int> compare)
		{
			if (list == null)
				throw new ArgumentNullException(nameof(list));
			if (compare == null)
				throw new ArgumentNullException(nameof(compare));

			return BinarySearchInternal(list, searching, compare, 0, list.Count);
		}

		static public int BinarySearch<TL, TS>(
			IList<TL>         list,
			TS                searching,
			Func<TL, TS, int> compare,
			int               startIndex,
			int               length)
		{
			if (list == null)
				throw new ArgumentNullException(nameof(list));
			if (compare == null)
				throw new ArgumentNullException(nameof(compare));

			if (((uint)startIndex + (uint)length) >= (uint)list.Count)
			{
				if ((uint)startIndex >= (uint)list.Count)
					throw new ArgumentOutOfRangeException(nameof(startIndex));
				else
					throw new ArgumentException(nameof(length));
			}

			return BinarySearchInternal(list, searching, compare, startIndex, length);
		}

	}
}