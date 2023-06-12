using Label = TMPro.TextMeshProUGUI;

namespace Bertis.UI
{
	using UnityEngine;
	using Bertis.Game;

	public sealed class MainMenu : MonoBehaviour
	{
		[SerializeField]
		private Button m_ContinueButton;
		[SerializeField]
		private Label m_CurrentLevelLabel;
		[SerializeField]
		private Label m_HighestLevelLabel;

		private void OnEnable()
		{
			StageHandler.UnloadStage();
			GameUtility.Pause(true);

			m_ContinueButton.interactable = StageHandler.CurrentLevel > 0;
			m_CurrentLevelLabel.text  = $"Current Level: {StageHandler.CurrentLevel}";

			var highestLevel = StageHandler.HighestLevel;
			m_HighestLevelLabel.text = highestLevel > 0
				? $"Highest Level: {StageHandler.HighestLevel}"
				: System.String.Empty;
		}

		private void OnDisable()
		{
			GameUtility.Pause(false);
		}

	}
}