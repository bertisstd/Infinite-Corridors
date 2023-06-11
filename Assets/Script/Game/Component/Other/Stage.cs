
namespace Bertis.Game
{
	using System;
	using UnityEngine;

	public class Stage : MonoBehaviour
	{
		static public event Action<int> OnProgress;

		[SerializeField]
		private Door[] m_Doors;
		[SerializeField]
		private Transform[] m_SpawnPoints;
		[SerializeField]
		private int m_MaxSpawnCount;

		private int m_SatelliteCount;

		public Door[] Doors
		{
			get => m_Doors;
		}
		public Transform[] SpawnPoints
		{
			get => m_SpawnPoints;
		}
		public int MaxSpawnCount
		{
			get => m_MaxSpawnCount;
		}

		public void Activate(int satelliteCount)
		{
			gameObject.SetActive(true);
			m_SatelliteCount = satelliteCount;
			PlayerInfo.Reference.OnKill += OnKill;
			RaiseOnProgress();
			OpenDoors(false);
		}

		public void Dispose()
		{
			PlayerInfo.Reference.OnKill -= OnKill;
			gameObject.SetActive(false);
			Destroy(gameObject);
		}

		private void OnKill(UnitInfo unit)
		{
			if (--m_SatelliteCount <= 0)
			{
				OpenDoors(true);
			}

			RaiseOnProgress();
		}

		private void OpenDoors(bool forward)
		{
			foreach (var door in Doors)
			{
				door.Active = forward;
			}
		}

		private void RaiseOnProgress()
		{
			OnProgress?.Invoke(m_SatelliteCount);
		}

	}
}