
namespace Bertis.Game
{
	using UnityEngine;
	using Bertis.Runtime;

	public sealed class BlackoutHandler : MonoBehaviour
	{
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		static private void Initialize()
		{
			var scheme = ConfigProvider.GetScheme<BlackoutHandler>(-1621176718); /*BlackoutHandler*/
			var instance = Hierarchy.CreateComponent(scheme);
			instance.gameObject.SetActive(false);
			StageHandler.OnStageChanged += () =>
			{
				instance.gameObject.SetActive(true);
			};
		}

		[System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051", Justification = "<Pending>")]
		private void Deactivate()
		{
			gameObject.SetActive(false);
		}

	}
}