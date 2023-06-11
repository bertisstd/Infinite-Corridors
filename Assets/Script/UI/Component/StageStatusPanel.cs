using Label = TMPro.TextMeshProUGUI;

namespace Bertis.UI
{
	using UnityEngine;
	using Bertis.Runtime;
	using Bertis.Game;

	public sealed class StageStatusPanel : MonoBehaviour
	{
		[SerializeField]
		private Label m_LeftSatelliteCountLabel;
		[SerializeField]
		private Label m_CurrentLevelLabel;

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		static private void Initialize()
		{
			var scheme = ConfigProvider.GetScheme<StageStatusPanel>(-1191362830); /*StageStatusPanel*/
			Hierarchy.CreateComponent(scheme, permanent: true);
		}

		private void Awake()
		{
			StageHandler.OnStageChanged += OnStageChanged;
			Stage.OnProgress += OnStageProgress;
		}

		private void OnStageChanged()
		{
			m_CurrentLevelLabel.text = $"Level {StageHandler.CurrentLevel}";
		}

		private void OnStageProgress(int left)
		{
			m_LeftSatelliteCountLabel.text = left <= 0f
				? "Cleared"
				: $"{left} Left";
		}

	}
}