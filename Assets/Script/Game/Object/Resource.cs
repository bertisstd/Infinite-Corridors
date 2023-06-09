
namespace Bertis.Game
{
	using UnityEngine;

	[System.Serializable]
	public struct Resource
	{
		[SerializeField]
		private float m_Current;
		[SerializeField]
		private float m_Max;

		public float Current
		{
			get => m_Current;
			set => m_Current = Mathf.Clamp(value, 0f, m_Max);
		}
		public float Max
		{
			get => m_Max;
			set => m_Max = Mathf.Max(0f, value);
		}
		public float Ratio
		{
			get => m_Current / m_Max;
		}
		public bool Depleted
		{
			get => m_Current <= 0f;
		}

		public override string ToString()
		{
			return $"{m_Current}:{m_Max}";
		}

	}
}