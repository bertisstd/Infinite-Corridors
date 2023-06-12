
namespace Bertis.UI
{
	using UnityEngine;
	using UnityEngine.UI;
	using Bertis.Game;

	public sealed class SettingsMenu : MonoBehaviour
	{
		[SerializeField]
		private Slider m_VolumeSlider;

		private void Awake()
		{
			m_VolumeSlider.onValueChanged.AddListener(OnVolumeSliderValueChanged);
		}

		private void OnEnable()
		{
			GameUtility.Pause(true);

			m_VolumeSlider.value = Runtime.Settings.AudioVolume;
		}

		private void OnDisable()
		{
			GameUtility.Pause(false);
		}

		private void OnVolumeSliderValueChanged(float value)
		{
			Runtime.Settings.AudioVolume = value;
		}

	}
}