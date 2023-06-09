
namespace Bertis.Game
{
	using UnityEngine;

	public abstract class Gun : MonoBehaviour
	{
		[SerializeField]
		private Transform m_Muzzle;

		private bool m_TriggerPulled;

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