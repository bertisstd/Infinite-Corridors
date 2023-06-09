
namespace Bertis
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	[System.Serializable]
	public sealed class Set<T> : IReadOnlyList<T>
	{
		[SerializeField]
		private T[] m_Collection;
		[SerializeField]
		private Mode m_Mode;
		[SerializeField, ReadOnly]
		private int m_LastIndex;

		private int m_CurrIndex;

		public int Count
		{
			get => m_Collection.Length;
		}

		public T this[int index]
		{
			get => m_Collection[index];
		}

		public T Gen()
		{
			switch (m_Collection.Length)
			{
				case 0:
					return default(T);

				case 1:
					return m_Collection[0];

				default:
					break;
			}

			return m_Mode switch
			{
				Mode.NonRepRandom => GenNonRepRandom(),
				Mode.FullyRandom  => GenFullyRandom(),
				Mode.Sequential   => GetNext(),
				_ => throw new EnumIndexException(m_Mode)
			};
		}

		private T GenNonRepRandom()
		{
			var index = RNG.GenInt32(m_LastIndex, m_Collection.Length);
			var ret = m_Collection[index];

			(m_Collection[index], m_Collection[m_CurrIndex]) = (m_Collection[m_CurrIndex], ret);

			if (++m_CurrIndex >= m_LastIndex)
				m_CurrIndex = 0;

			return ret;
		}

		private T GenFullyRandom()
		{
			return m_Collection[RNG.GenInt32(m_Collection.Length)];
		}

		private T GetNext()
		{
			var ret = m_Collection[m_CurrIndex];

			if (++m_CurrIndex >= m_Collection.Length)
				m_CurrIndex = 0;

			return ret;
		}

		public IEnumerator<T> GetEnumerator()
		{
			return ((IEnumerable<T>)m_Collection).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return m_Collection.GetEnumerator();
		}

		private enum Mode
		{
			NonRepRandom,
			FullyRandom,
			Sequential
		}

	}
}