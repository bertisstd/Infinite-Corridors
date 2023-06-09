
namespace Bertis.Game
{
	using UnityEngine;

	public sealed class PlayerInfo : UnitInfo
	{
		[SerializeField]
		private Gun m_TestGun;
		[SerializeField]
		private Transform m_Hand;

		private Gun m_Gun;

		public Gun Gun
		{
			get => m_Gun;
			set
			{
				if (m_Gun != null)
				{
					m_Gun.Source = null;
					m_Gun.transform.SetParent(null);
				}

				if (value != null)
				{
					value.Source = this;
					value.transform.SetParent(m_Hand, worldPositionStays: false);
					value.transform.localPosition = Vector3.zero;
					value.transform.localRotation = Quaternion.identity;
				}

				m_Gun = value;
			}
		}

		private void Awake()
		{
			if (m_TestGun != null)
			{
				Gun = m_TestGun;
			}
		}

	}
}