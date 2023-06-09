
namespace Bertis.UI
{
	using UnityEngine;
	using Bertis.Runtime;
	using Bertis.Game;

	static public class UnitBarHandler
	{
		static private GComponentCache<SatelliteBar> s_Cache;

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
		static private void Initialize()
		{
			var playerBarScheme = ConfigProvider.GetScheme<UnitBar>(+870610950); /*PlayerBar*/
			var playerBar = Hierarchy.CreateComponent(playerBarScheme, permanent: true);
			playerBar.UnitInfo = PlayerInfo.Reference;

			var satelliteBarScheme = ConfigProvider.GetScheme<SatelliteBar>(+499939827); /*SatelliteBar*/
			s_Cache = new GComponentCache<SatelliteBar>(satelliteBarScheme);
			SatelliteInfo.OnVisibilityChangedStatic += OnVisibilityChanged;
		}

		static private void OnVisibilityChanged(SatelliteInfo satelliteInfo)
		{
			if (satelliteInfo.Visible)
			{
				s_Cache.Provide().SatelliteInfo = satelliteInfo;
			}
		}

	}
}