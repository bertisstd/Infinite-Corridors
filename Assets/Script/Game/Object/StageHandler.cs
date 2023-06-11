
namespace Bertis.Game
{
	using System;
	using System.Linq;
	using UnityEngine;
	using Bertis.Runtime;

	[CreateAssetMenu(fileName = c_FileName, menuName = c_MenuName)]
	public sealed class StageHandler : ScriptableObject
	{
		private const string c_FileName = nameof(StageHandler);
		private const string c_MenuName = "Game/" + c_FileName;

		static public event Action OnStageChanged;

		static private readonly SJitter<StageHandler> s_This = new(c_MenuName);
		static private GComponentProvider<SatelliteInfo> s_EnemyProvider;
		static private GComponentProvider<Gun> s_GunProvider;

		static private Stage s_CurrentStage;

		[SerializeField]
		private int m_CurrentLevel;
		[SerializeField]
		private Set<Stage> m_Stages;
		[SerializeField]
		private WeightedSet<SatelliteInfo> m_Spawns;
		[SerializeField]
		private WeightedSet<Gun> m_Guns;

		static private StageHandler This
		{
			get => s_This.Get();
		}

		static public int CurrentLevel
		{
			get => This.m_CurrentLevel;
			private set => This.m_CurrentLevel = value;
		}

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		static private void Initialize()
		{
			s_EnemyProvider = new();
			s_GunProvider   = new();
		}

		static public void GotoNextStage()
		{
			CurrentLevel++;

			var nextStage = Instantiate(This.m_Stages.Gen());

			DisposeEnemies();

			if (s_CurrentStage != null)
				s_CurrentStage.Dispose();

			TeleportPlayer(nextStage);
			SwapGuns();
			var count = SpawnEnemies(nextStage);

			OnStageChanged?.Invoke();
			nextStage.Activate(count);
			s_CurrentStage = nextStage;
		}

		static private void TeleportPlayer(Stage nextStage)
		{
			var doors = nextStage.Doors;
			var nextDoor = doors[RNG.GenInt32(doors.Length)];
			PlayerInfo.Reference.transform.position = nextDoor.transform.position;
		}

		static private void DisposeEnemies()
		{
			foreach (var cache in s_EnemyProvider)
			{
				foreach (var elem in cache)
				{
					elem.gameObject.SetActive(false);
				}
			}
		}

		static private int SpawnEnemies(Stage nextStage)
		{
			var points = nextStage.SpawnPoints
				.OrderBy(_ => RNG.GenInt32(nextStage.MaxSpawnCount))
				.Take(nextStage.MaxSpawnCount);

			var count = 0;
			foreach (var point in points)
			{
				var scheme = This.m_Spawns.GenValue();
				var instance = s_EnemyProvider.Provide(scheme);
				instance.ResetProperties();
				instance.transform.position = point.position;
				instance.gameObject.SetActive(true);

				count++;
			}

			return count;
		}

		static private void SwapGuns()
		{
			var player = PlayerInfo.Reference;
			var prevGun = player.Gun;

			if (prevGun != null)
				prevGun.gameObject.SetActive(false);

			var nextGun = s_GunProvider.Provide(This.m_Guns.GenValue());
			nextGun.gameObject.SetActive(true);
			player.Gun = nextGun;
		}

	}
}