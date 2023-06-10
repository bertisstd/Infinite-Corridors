
namespace Bertis
{
	using UnityEngine;

	[System.Serializable]
	public sealed class WeightedSet<T>
	{
		[SerializeField]
		private Element[] m_Collection;
		[SerializeField, ReadOnly]
		private float m_TotalWeight;

		public T GetValue()
		{
			switch (m_Collection.Length)
			{
				case 0:
					return default(T);

				case 1:
					return m_Collection[0].Value;

				default:
					break;
			}

			var random = RNG.GenSingle(m_TotalWeight);
			var acc = 0f;
			foreach (var elem in m_Collection)
			{
				acc += elem.Weight;

				if (random < acc)
				{
					return elem.Value;
				}
			}

			return m_Collection[^1].Value;
		}

		[System.Serializable]
		public sealed class Element
		{
			[SerializeField]
			private string m_Name;
			[SerializeField]
			private T m_Value;
			[SerializeField]
			private float m_Weight;
			[SerializeField, ReadOnly]
			private float m_Chance;

			public string Name
			{
				get => m_Name;
			}
			public T Value
			{
				get => m_Value;
			}
			public float Weight
			{
				get => m_Weight;
			}
			public float Chance
			{
				get => m_Chance;
			}

		}

	}
}