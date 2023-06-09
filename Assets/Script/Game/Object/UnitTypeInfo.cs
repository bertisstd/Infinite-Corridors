
namespace Bertis.Game
{
	using UnityEngine;
	using Bertis.Runtime;

	[System.Serializable]
	public sealed class UnitTypeInfo : ObjectInfo
	{
		[SerializeField]
		private bool m_Static;
		[SerializeField]
		private AudioClip m_HitSfx;
		[SerializeField]
		private ParticleSystem m_HitPfx;
		[SerializeField]
		private AudioClip m_DeathSfx;
		[SerializeField]
		private ParticleSystem m_DeathPfx;
		[SerializeField]
		private WorldSpriteRef m_DeathSprite;

		public bool Static
		{
			get => m_Static;
		}
		public AudioClip HitSfx
		{
			get => m_HitSfx;
		}
		public ParticleSystem HitPfx
		{
			get => m_HitPfx;
		}
		public AudioClip DeathSfx
		{
			get => m_DeathSfx;
		}
		public ParticleSystem DeathPfx
		{
			get => m_DeathPfx;
		}
		public WorldSpriteRef DeathSprite
		{
			get => m_DeathSprite;
		}

	}
}