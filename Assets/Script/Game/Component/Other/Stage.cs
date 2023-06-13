
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
		private Transform[] m_MinePoints;

		private int m_SatelliteCount;

		public Door[] Doors
		{
			get => m_Doors;
		}
		public Transform[] SpawnPoints
		{
			get => m_SpawnPoints;
		}
		public Transform[] MinePoints
		{
			get => m_MinePoints;
		}

		public void Activate(int satelliteCount)
		{
			gameObject.SetActive(true);
			m_SatelliteCount = satelliteCount;
			UnitInfo.OnDieStatic += OnUnitDie;
			RaiseOnProgress();
			OpenDoors(false);
		}

		public void Dispose()
		{
			UnitInfo.OnDieStatic -= OnUnitDie;
			gameObject.SetActive(false);
			Destroy(gameObject);
		}

		private void OnUnitDie(UnitInfo unit)
		{
			if (unit is not SatelliteInfo)
				return;

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