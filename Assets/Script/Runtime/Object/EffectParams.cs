
namespace Bertis.Runtime
{
	using UnityEngine;

	[System.Serializable]
	public struct EffectParams
	{
		[SerializeField]
		private bool m_Enabled;
		[SerializeField]
		private float m_Amplitude;
		[SerializeField]
		private float m_Duration;

		public bool Valid
		{
			get => m_Enabled;
		}
		public float Amplitude
		{
			get => m_Amplitude;
			set => m_Amplitude = value;
		}
		public float Duration
		{
			get => m_Duration;
			set => m_Duration = value;
		}

	}
}