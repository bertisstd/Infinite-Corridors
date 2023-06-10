
namespace Bertis.Game
{
	using UnityEngine;

	[RequireComponent(typeof(Collider2D))]
	public class Door : MonoBehaviour
	{
		private void Start()
		{
			enabled = false;
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.E))
			{
				StageHandler.GotoNextStage();
			}
		}

		private void OnTriggerStay2D(Collider2D collider)
		{
			if (collider.TryGetComponent<PlayerInfo>(out _))
			{
				enabled = true;
			}
		}

		private void OnTriggerExit2D(Collider2D collider)
		{
			if (collider.TryGetComponent<PlayerInfo>(out _))
			{
				enabled = false;
			}
		}

	}
}