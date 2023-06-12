
namespace Bertis.UI
{
	using UnityEngine;
	using Bertis.Game;

	public sealed class DeathMenu : MonoBehaviour
	{
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
		static private void Initialize()
		{
			var instance = FindObjectOfType<DeathMenu>(includeInactive: true);
			PlayerInfo.Reference.OnDie += () =>
			{
				instance.gameObject.SetActive(true);
			};
		}

		private void OnEnable()
		{
			StageHandler.UnloadStage();
			GameUtility.Pause(true);
		}

		private void OnDisable()
		{
			GameUtility.Pause(false);
		}

	}
}