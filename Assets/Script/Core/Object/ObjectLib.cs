
namespace Bertis
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class ObjectLib<T> : ScriptableObject, IEnumerable<T> where T : ObjectInfo
	{
		[SerializeField]
		private T[] m_Collection;
		[SerializeField, ReadOnly]
		private int m_NextId;

		public T GetInfo(int id)
		{
			var length = m_Collection.Length;

			if (length == 0)
				throw new Exception("ObjectLib is empty");

			for (var i = 0; i < length; i++)
			{
				if (m_Collection[i].Id == id)
				{
					return m_Collection[i];
				}
			}

			Diag.LogWarning($"Obsolete object id. Id: {id}, Lib: {name}");
			return m_Collection[0];
		}

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