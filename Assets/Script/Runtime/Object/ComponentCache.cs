
namespace Bertis.Runtime
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class ComponentCache<T> : Cache<ComponentCache<T>.Element>, IReadOnlyList<T> where T: Component
	{
		private readonly Func<T, bool> m_GetActive;
		private readonly Creation m_Creation;
		private readonly GameObject m_GameObject;

		public ComponentCache(Func<T, bool> getActive, T scheme)
		{
			if (getActive == null)
				throw new ArgumentNullException(nameof(getActive));
			if (scheme == null)
				throw new ArgumentNullException(nameof(scheme));

			m_GetActive = getActive;
			m_Creation = Creation.Scheme;
			Scheme = scheme;
		}
		public ComponentCache(Func<T, bool> getActive, bool unique)
		{
			if (getActive == null)
				throw new ArgumentNullException(nameof(getActive));

			if (unique)
			{
				m_Creation = Creation.Unique;
			}
			else
			{
				m_Creation = Creation.Stack;
				m_GameObject = new(typeof(T).GetName());
			}
		}

		public new T this[int index]
		{
			get => base[index].value;
		}

		public bool Temporary { get; init; }

		public T Scheme { get; }

		public new T Provide()
		{
			return base.Provide().value;
		}

		protected override Element Create()
		{
			var value = m_Creation switch
			{
				Creation.Scheme => Hierarchy.CreateComponent(Scheme, !Temporary),
				Creation.Unique => Hierarchy.CreateComponent<T>(typeof(T).GetName(), !Temporary),
				Creation.Stack  => m_GameObject.AddComponent<T>(),
				_ => throw new ArgumentOutOfRangeException()
			};

			return new(m_GetActive, value);
		}

		public new IEnumerator<T> GetEnumerator()
		{
			for (var i = 0; i < Count; i++)
			{
				yield return this[i];
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		private enum Creation
		{
			Scheme,
			Unique,
			Stack
		}

		public sealed class Element : IStatusProvider
		{
			private readonly Func<T, bool> m_GetActive;
			internal readonly T value;
			
			internal Element(Func<T, bool> getActive, T value)
			{
				m_GetActive = getActive;
				this.value = value;
			}

			public bool Active
			{
				get => m_GetActive(value);
			}

		}

	}
}