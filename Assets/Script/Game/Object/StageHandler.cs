
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

		static private Stage s_CurrentStage;

		[SerializeField]
		private Set<Stage> m_Stages;
		[SerializeField]
		private WeightedSet<SatelliteInfo> m_Spawns;

		static private StageHandler This
		{
			get => s_This.Get();
		}

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		static private void Initialize()
		{
			s_EnemyProvider = new();
		}

		static public void GotoNextStage()
		{
			var nextStage = Instantiate(This.m_Stages.Gen());

			DisposeEnemies();

			if (s_CurrentStage != null)
			{
				var gameObject = s_CurrentStage.gameObject;
				gameObject.SetActive(false);
				Destroy(gameObject);
			}

			TeleportPlayer(nextStage);
			SpawnEnemies(nextStage);

			OnStageChanged?.Invoke();

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

		static private void SpawnEnemies(Stage nextStage)
		{
			var points = nextStage.SpawnPoints
				.OrderBy(_ => RNG.GenInt32(nextStage.MaxSpawnCount))
				.Take(nextStage.MaxSpawnCount);

			foreach (var point in points)
			{
				var scheme = This.m_Spawns.GenValue();
				var instance = s_EnemyProvider.Provide(scheme);
				instance.ResetProperties();
				instance.transform.position = point.position;
				instance.gameObject.SetActive(true);
			}
		}

	}
}