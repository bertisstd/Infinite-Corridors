
namespace Bertis.Game
{
	using UnityEngine;

	public class Stage : MonoBehaviour
	{
		[SerializeField]
		private Door[] m_Doors;
		[SerializeField]
		private Transform[] m_SpawnPoints;
		[SerializeField]
		private int m_MaxSpawnCount;

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

	}
}