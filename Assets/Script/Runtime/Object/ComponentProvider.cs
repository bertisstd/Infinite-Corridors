
namespace Bertis.Runtime
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public abstract class ComponentProvider<T> : IReadOnlyList<ComponentCache<T>> where T: Component
	{
		static private readonly Func<ComponentCache<T>, T, int> s_Compare;

		private readonly List<ComponentCache<T>> m_Collection;
		private ComponentCache<T> m_Cache;

		protected ComponentProvider()
		{
			m_Collection = new();
		}

		public ComponentCache<T> this[int index]
		{
			get => m_Collection[index];
		}

		public int Count
		{
			get => m_Collection.Count;
		}

		static ComponentProvider()
		{
			s_Compare = Compare;
		}

		public T Provide(T scheme)
		{
			if (scheme == null)
				throw new ArgumentNullException(nameof(scheme));

			ComponentCache<T> cache;

			if (m_Cache != null && Compare(m_Cache, scheme) == 0)
			{
				cache = m_Cache;
				goto __return;
			}

			var index = m_Collection.BinarySearch(scheme, s_Compare);
			if (index >= 0)
			{
				cache = m_Collection[index];
			}
			else
			{
				cache = Create(scheme);
				m_Collection.Insert(~index, cache);
			}

			m_Cache = cache;

		__return:
			return cache.Provide();
		}

		protected abstract ComponentCache<T> Create(T scheme);

		static private int Compare(ComponentCache<T> cache, T scheme)
		{
			return cache.Scheme.GetInstanceID()
				.CompareTo(scheme.GetInstanceID());
		}

		public IEnumerator<ComponentCache<T>> GetEnumerator()
		{
			return ((IEnumerable<ComponentCache<T>>)m_Collection).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return m_Collection.GetEnumerator();
		}

	}
}