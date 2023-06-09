
namespace Bertis.Runtime
{
	using System;
	using UnityEngine;

	static public class PfxHandler
	{
		static private Provider s_Provider;

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
		static private void Initialize()
		{
			s_Provider = new();
		}

		static public void Play(ParticleSystem scheme, Vector3 position)
		{
			if (scheme != null)
			{
				var pfx = s_Provider.Provide(scheme);
				pfx.transform.position = position;
				pfx.Play();
			}
		}

		private sealed class Provider : ComponentProvider<ParticleSystem>
		{
			static private readonly Func<ParticleSystem, bool> s_GetActive;

			static Provider()
			{
				s_GetActive = pfx => pfx.isEmitting;
			}

			protected override ComponentCache<ParticleSystem> Create(ParticleSystem scheme)
			{
				return new ComponentCache<ParticleSystem>(s_GetActive, scheme);
			}

		}

	}
}