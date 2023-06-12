
namespace Bertis.Runtime
{
	using UnityEngine;

	static public class AudioHandler
	{
		static private SComponentCache<Processor> s_Cache;

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		static private void Initialize()
		{
			s_Cache = new(unique: false);
		}

		static public void Play(AudioClip clip, float volumeScale = 1.0f)
		{
			if (clip == null)
				return;

			var audioSource = s_Cache.Provide().audioSource;
			audioSource.clip = clip;
			audioSource.volume = Settings.AudioVolume * volumeScale;
			audioSource.Play();
		}

		private class Processor : MonoBehaviour, IStatusProvider
		{
			public AudioSource audioSource;

			private void Awake()
			{
				audioSource = gameObject.AddComponent<AudioSource>();
			}

			public bool Active
			{
				get => audioSource.isPlaying;
			}

		}

	}
}