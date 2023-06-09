
namespace Bertis.Runtime
{
	using System;
	using UnityEngine;

	static public class SpriteAnimHandler
	{
		static private GComponentCache<Processor> s_Cache;

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
		static private void Initialize()
		{
			s_Cache = new(unique: true);
		}
		
		static public void Animate(this SpriteAnimRef spriteAnimRef, Vector3 position, Quaternion rotation, Vector3 scale)
		{
			if (spriteAnimRef == null)
				throw new ArgumentNullException(nameof(spriteAnimRef));

			if (spriteAnimRef.GetInfo(out var info))
			{
				var processor = s_Cache.Provide();
				processor.Restart(info);

				var transform = processor.transform;
				transform.SetPositionAndRotation(position, rotation);
				transform.localScale = scale;
			}
		}

		static public void Animate(this SpriteAnimRef spriteAnimRef, Vector3 position, Quaternion rotation)
		{
			Animate(spriteAnimRef, position, rotation, Vector3.one);
		}

		static public void Animate(this SpriteAnimRef spriteAnimRef, Vector3 position)
		{
			Animate(spriteAnimRef, position, Quaternion.identity, Vector3.one);
		}

		private class Processor : MonoBehaviour
		{
			private Sprite[] m_Sprites;
			private float    m_UnitDuration;
			private float    m_Timer;
			private int      m_CurrIndex;

			private SpriteRenderer m_SpriteRenderer;

			private void Awake()
			{
				m_SpriteRenderer = gameObject.AddComponent<SpriteRenderer>();
			}

			private void Update()
			{
				m_Timer -= Time.deltaTime;

				if (m_Timer <= 0f)
				{
					if (++m_CurrIndex < m_Sprites.Length)
					{
						m_SpriteRenderer.sprite = m_Sprites[m_CurrIndex];
						m_Timer = m_UnitDuration;
					}
					else
					{
						gameObject.SetActive(false);
					}
				}
			}

			public void Restart(SpriteAnimInfo info)
			{
				m_Sprites      = info.Sprites;
				m_UnitDuration = info.Duration / m_Sprites.Length;
				m_Timer        = m_UnitDuration;
				m_CurrIndex    = 0;

				m_SpriteRenderer.sortingOrder = info.RenderOrder;
				m_SpriteRenderer.sprite = m_Sprites[0];

				gameObject.SetActive(true);
			}

		}

	}
}