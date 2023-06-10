
namespace Bertis.Game
{
	using System;
	using UnityEngine;
	using Bertis.Runtime;

	[Serializable]
	public class Explosion
	{
		[SerializeField]
		private bool m_Enabled;
		[SerializeField]
		private float m_Radius;
		[SerializeField]
		private SpriteAnimRef m_Anim;
		[SerializeField]
		private AudioClip m_Sfx;
		[SerializeField]
		private TransformNoiseRef m_Noise;

		public bool Valid
		{
			get => m_Enabled;
		}
		public float Radius
		{
			get => m_Radius;
		}

		public void Explode(UnitInfo source, Vector3 position)
		{
			if (source == null)
				throw new ArgumentNullException(nameof(source));

			var units = Channel.GetUnitsInArea(position, m_Radius);
			foreach (var unit in units)
			{
				source.DealDamage(unit, position);
			}

			m_Anim.Animate(position, Quaternion.identity, Vector3.one * m_Radius);
			AudioHandler.Play(m_Sfx);
			View.PlayNoise(m_Noise);
		}

	}
}