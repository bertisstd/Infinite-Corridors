
namespace Bertis
{
	using System;
	using UnityEngine;

	[Serializable]
	public sealed class Library<T>
	{
		[SerializeField]
		private Element[] m_Collection;

		public T GetValue(int id)
		{
			foreach (var elem in m_Collection)
			{
				if (elem.Id == id)
					return elem.Value;
			}

			throw new ArgumentException($"Element with the id not found. Id: {id}");
		}

		[Serializable]
		public sealed class Element
		{
			[SerializeField]
			private string m_Name;
			[SerializeField, ReadOnly]
			private int m_Id;
			[SerializeField]
			private T m_Value;

			public string Name
			{
				get => m_Name;
			}
			public int Id
			{
				get => m_Id;
			}
			public T Value
			{
				get => m_Value;
			}

		}

	}
}