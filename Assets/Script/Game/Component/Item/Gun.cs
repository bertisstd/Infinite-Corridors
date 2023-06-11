
namespace Bertis.Game
{
	using UnityEngine;

	public abstract class Gun : MonoBehaviour
	{
		[SerializeField]
		private float m_Damage;
		[SerializeField]
		private Transform m_Muzzle;

		private UnitInfo m_Source;
		private bool m_TriggerPulled;

		public UnitInfo Source
		{
			get => m_Source;
			set
			{
				if (m_Source != null)
					m_Source.Damage -= m_Damage;

				if (value != null)
				{
					value.Damage += m_Damage;
					m_Source = value;
				}
			}
		}
		public Transform Muzzle
		{
			get => m_Muzzle;
		}

		public void PullTrigger(bool forward)
		{
			if (m_TriggerPulled == forward)
				return;

			switch (m_TriggerPulled = forward)
			{
				case true:
					OnTriggerPull();
					return;

				case false:
					OnTriggerRelease();
					return;
			}
		}

		protected abstract void OnTriggerPull();
		protected abstract void OnTriggerRelease();

	}
}