
namespace Bertis.Game
{
	using UnityEngine;
	using Bertis.Runtime;

	static public class RoundProvider
	{
		static private Provider s_Provider;

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
		static private void Initialize()
		{
			s_Provider = new();
		}

		static public Round Provide(Round scheme)
		{
			return s_Provider.Provide(scheme);
		}

		private sealed class Provider : ComponentProvider<Round>
		{
			protected override ComponentCache<Round> Create(Round scheme)
			{
				return new GComponentCache<Round>(scheme);
			}

		}

	}
}