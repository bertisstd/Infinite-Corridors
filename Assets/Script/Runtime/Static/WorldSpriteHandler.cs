
namespace Bertis.Runtime
{
	using System;
	using UnityEngine;

	static public class WorldSpriteHandler
	{
		static private GComponentCache<Processor> s_Cache;

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
		static private void Initialize()
		{
			s_Cache = new(unique: true);
		}

		static public void Place(this WorldSpriteRef worldSpriteRef, Vector3 position)
		{
			if (worldSpriteRef == null)
				throw new ArgumentNullException(nameof(worldSpriteRef));

			if (worldSpriteRef.GetInfo(out var info))
			{
				var processor = s_Cache.Provide();
				processor.transform.position = position;
				processor.Restart(info);
			}
		}

		private class Processor : MonoBehaviour
		{
			private SpriteRenderer m_SpriteRenderer;

			private void Awake()
			{
				m_SpriteRenderer = gameObject.AddComponent<SpriteRenderer>();
			}

			private void OnBecameInvisible()
			{
				gameObject.SetActive(false);
			}

			public void Restart(WorldSpriteInfo info)
			{
				m_SpriteRenderer.sortingOrder = info.RenderOrder;
				m_SpriteRenderer.sprite = info.SpriteSet.Gen();
				gameObject.SetActive(true);
			}

		}

	}
}