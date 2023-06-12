
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

		static private AnimationCurve s_LevelSatelliteCountCurve;
		static private AnimationCurve s_LevelMineCountCurve;

		static private readonly SJitter<StageHandler> s_This = new(c_MenuName);
		static private GComponentProvider<SatelliteInfo> s_EnemyProvider;
		static private GComponentProvider<Gun> s_GunProvider;
		static private GComponentProvider<Mine> s_MineProvider;

		static private Stage s_CurrentStage;

		[SerializeField]
		private int m_CurrentLevel;
		[SerializeField]
		private int m_HighestLevel;
		[SerializeField]
		private Set<Stage> m_Stages;
		[SerializeField]
		private WeightedSet<SatelliteInfo> m_Spawns;
		[SerializeField]
		private Set<Mine> m_Mines;
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
		static public int HighestLevel
		{
			get => This.m_HighestLevel;
			private set => This.m_HighestLevel = value;
		}

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		static private void Initialize()
		{
			s_LevelSatelliteCountCurve = ConfigProvider.GetCurve(+23588706);   /*LevelSatelliteCount*/
			s_LevelMineCountCurve      = ConfigProvider.GetCurve(+1777853996); /*LevelMineCount*/

			s_EnemyProvider = new();
			s_GunProvider   = new();
			s_MineProvider  = new();
		}

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
		static private void Register()
		{
			PlayerInfo.Reference.OnDie += () =>
			{
				CurrentLevel = 0;
				PlayerInfo.Reference.ResetProperties();
			};
		}

		static public void UnloadStage()
		{
			if (s_CurrentStage != null)
			{
				DisposeEnemies();
				DisposeMines();
				s_CurrentStage.Dispose();
				s_CurrentStage = null;
			}
		}
		
		static public void StartNewStage()
		{
			PlayerInfo.Reference.gameObject.SetActive(true);
			CurrentLevel = 1;
			LoadStage();
		}

		static public void ContinueStage()
		{
			LoadStage();
		}

		static public void GotoNextStage()
		{
			if (++CurrentLevel > HighestLevel)
				HighestLevel = CurrentLevel;

			LoadStage();
		}

		static public void LoadStage()
		{
			UnloadStage();

			var nextStage = Instantiate(This.m_Stages.Gen());
			TeleportPlayer(nextStage);
			SwapGuns();
			SpawnMines(nextStage);
			var count = SpawnEnemies(nextStage);

			OnStageChanged?.Invoke();
			nextStage.Activate(count);
			s_CurrentStage = nextStage;
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

		static private void DisposeMines()
		{
			foreach (var cache in s_MineProvider)
			{
				foreach (var elem in cache)
				{
					elem.gameObject.SetActive(false);
				}
			}
		}

		static private void TeleportPlayer(Stage nextStage)
		{
			var doors = nextStage.Doors;
			var nextDoor = doors[RNG.GenInt32(doors.Length)];
			PlayerInfo.Reference.transform.position = nextDoor.transform.position;
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

		static private int SpawnEnemies(Stage nextStage)
		{
			var count = (int)s_LevelSatelliteCountCurve.Evaluate(CurrentLevel);

			var points = nextStage.SpawnPoints
				.OrderBy(_ => RNG.GenInt32(count))
				.Take(count);

			var ret = 0;
			foreach (var point in points)
			{
				var scheme = This.m_Spawns.GenValue();
				var instance = s_EnemyProvider.Provide(scheme);
				instance.ResetProperties();
				instance.transform.position = point.position;
				instance.gameObject.SetActive(true);

				ret++;
			}

			return ret;
		}

		static private void SpawnMines(Stage nextStage)
		{
			var count = (int)s_LevelMineCountCurve.Evaluate(CurrentLevel);

			var points = nextStage.MinePoints
				.OrderBy(_ => RNG.GenInt32(count))
				.Take(count);

			foreach (var point in points)
			{
				var scheme = This.m_Mines.Gen();
				var instance = s_MineProvider.Provide(scheme);
				instance.ResetProperties();
				instance.transform.position = point.position;
				instance.gameObject.SetActive(true);
			}
		}

	}
}