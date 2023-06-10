
namespace Bertis.Game
{
	using UnityEngine;

	public sealed class PlayerInfo : UnitInfo
	{
		static private PlayerInfo s_Reference;

		[SerializeField]
		private Gun m_TestGun;
		[SerializeField]
		private Transform m_Hand;
		[SerializeField]
		private Transform m_Front;

		private Gun m_Gun;

		static public PlayerInfo Reference
		{
			get => s_Reference;
		}

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
		public Transform Hand
		{
			get => m_Hand;
		}
		public Transform Front
		{
			get => m_Front;
		}

		private void Awake()
		{
			s_Reference = this;

			if (m_TestGun != null)
			{
				Gun = m_TestGun;
			}
		}

	}
}