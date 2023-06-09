
namespace Bertis.Runtime
{
	using System.Collections;
	using System.Collections.Generic;

	public abstract class Cache<T> : IReadOnlyList<T> where T : IStatusProvider
	{
		private readonly List<T> m_Collection;

		protected Cache()
		{
			m_Collection = new();
		}

		public T this[int index]
		{
			get => m_Collection[index];
		}

		public CollectionParams Params { get; init; }

		public int Count
		{
			get => m_Collection.Count;
		}
		public int ActiveCount
		{
			get
			{
				var ret = 0;
				for (var i = m_Collection.Count; --i >= 0;)
				{
					if (m_Collection[i].Active)
						++ret;
				}
				return ret;
			}
		}

		public virtual void Initialize()
		{
			for (var i = Params.InitSize; --i >= 0;)
			{
				CreateInternal();
			}
		}

		public T Provide()
		{
			for (var i = m_Collection.Count; --i >= 0;)
			{
				if (!m_Collection[i].Active)
				{
					return m_Collection[i];
				}
			}

			for (var i = Params.Accretion; --i >= 0;)
			{
				CreateInternal();
			}

			return m_Collection[^1];
		}

		private T CreateInternal()
		{
			var ret = Create();
			m_Collection.Add(ret);
			return ret;
		}

		protected abstract T Create();

		public IEnumerator<T> GetEnumerator()
		{
			return ((IEnumerable<T>)m_Collection).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return m_Collection.GetEnumerator();
		}

	}
}