
namespace Bertis.Game
{
	using UnityEngine;
	using Bertis.Runtime;

	[RequireComponent(typeof(Collider2D))]
	public sealed class HealthDrop : MonoBehaviour
	{
		static private GComponentCache<HealthDrop> s_Cache;

		private float m_Amount;

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
		static private void Initialize()
		{
			var scheme = ConfigProvider.GetScheme<HealthDrop>(-522818778); /*HealthDrop*/
			s_Cache = new GComponentCache<HealthDrop>(scheme);
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