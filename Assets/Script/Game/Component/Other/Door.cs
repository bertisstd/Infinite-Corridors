
namespace Bertis.Game
{
	using UnityEngine;

	[RequireComponent(typeof(Collider2D))]
	public class Door : MonoBehaviour
	{
		private Collider2D m_Collider;

		public bool Active
		{
			get => m_Collider.enabled;
			set => m_Collider.enabled = value;
		}

		private void Awake()
		{
			m_Collider = GetComponent<Collider2D>();
			Active = false;
		}

		private void OnTriggerStay2D(Collider2D collider)
		{
			if (collider.TryGetComponent<PlayerInfo>(out _))
			{
				StageHandler.GotoNextStage();
			}
		}

	}
}