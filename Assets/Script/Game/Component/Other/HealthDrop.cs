
namespace Bertis.Game
{
	using UnityEngine;
	using Bertis.Runtime;

	[RequireComponent(typeof(Collider2D))]
	public sealed class HealthDrop : MonoBehaviour
	{
		static private GComponentCache<HealthDrop> s_Cache;
		static private float s_DropChance;
		static private float s_DropAmount;

		private float m_Amount;

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
		static private void Initialize()
		{
			var scheme = ConfigProvider.GetScheme<HealthDrop>(-522818778); /*HealthDrop*/
			s_Cache = new GComponentCache<HealthDrop>(scheme);

			s_DropChance = ConfigProvider.GetSingle(-1235376000); /*HealthDropChance*/
			s_DropAmount = ConfigProvider.GetSingle(+482862054);  /*HealthDropAmount*/

			PlayerInfo.Reference.OnKill += OnKill;
			StageHandler.OnStageChanged += OnStageChanged;
		}

		static public void Drop(Vector3 position, float amount)
		{
			if (amount > 0f)
			{
				var instance = s_Cache.Provide();
				instance.m_Amount = amount;
				instance.transform.position = position;
				instance.gameObject.SetActive(true);
			}
		}

		static private void OnKill(UnitInfo unit)
		{
			if (unit is SatelliteInfo && RNG.GenBool(s_DropChance))
			{
				Drop(unit.transform.position, s_DropAmount);
			}
		}

		private static void OnStageChanged()
		{
			foreach (var elem in s_Cache)
			{
				elem.gameObject.SetActive(false);
			}
		}

		private void Awake()
		{
			gameObject.layer = Layer.Sensor;
		}

		private void OnTriggerEnter2D(Collider2D collider)
		{
			if (collider.TryGetComponent<PlayerInfo>(out var player))
			{
				player.Heal(m_Amount);
				gameObject.SetActive(false);
			}
		}

	}
}