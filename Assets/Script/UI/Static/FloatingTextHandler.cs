
namespace Bertis.UI
{
	using UnityEngine;
	using Bertis.Runtime;
	using Bertis.Game;

	static public class FloatingTextHandler
	{
		static private GComponentCache<FloatingText> s_Cache;

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
		static private void Initialize()
		{
			var scheme = ConfigProvider.GetScheme<FloatingText>(+9739860); /*FloatingText*/
			s_Cache = new GComponentCache<FloatingText>(scheme);

			UnitInfo.OnReactionStatic += OnReaction;
			UnitInfo.OnHealStatic += OnHeal;
		}

		static private void OnReaction(ref ReactionInfo reaction)
		{
			s_Cache.Provide().OnReaction(ref reaction);
		}

		static private void OnHeal(UnitInfo unit, float amount)
		{
			s_Cache.Provide().OnHeal(unit, amount);
		}

	}
}