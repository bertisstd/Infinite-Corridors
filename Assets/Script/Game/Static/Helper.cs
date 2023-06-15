
namespace Bertis.Game
{
	using UnityEngine;
	using Bertis.Runtime;

	public class Helper
	{
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
		static private void Register()
		{
			StageHandler.OnStageChanged += WorldSpriteHandler.ClearAll;
		}

	}
}