
namespace Bertis.Game
{
	using UnityEngine;
	using Bertis.Runtime;

	public class Rifle : Gun
	{
		[Header("Technical")]
		[SerializeField]
		private float m_FireInterval;
		[SerializeField]
		private float m_RoundLifetime;
		[SerializeField]
		private float m_RoundSpeed;
		[SerializeField]
		private bool m_Pierce;
		[SerializeField]
		private Round m_RoundScheme;

		[Header("Effect")]
		[SerializeField]
		private AudioClip m_FireSfx;
		[SerializeField]
		private SpriteAnimRef m_FireSpriteAnim;
		[SerializeField]
		private TransformNoiseRef m_FireNoise;

		private float m_LastFireTime;

		public float FireInterval
		{
			get => m_FireInterval;
		}
		public float RoundLifetime
		{
			get => m_RoundLifetime;
		}
		public float RoundSpeed
		{
			get => m_RoundSpeed;
		}
		public bool Pierce
		{
			get => m_Pierce;
		}

		private void Start()
		{
			enabled = false;
		}

		private void Update()
		{
			var time = Time.time;

			if (time - m_LastFireTime < m_FireInterval)
				return;

			Fire();

			m_LastFireTime = time;
		}

		private void Fire()
		{
			var round = RoundProvider.Provide(m_RoundScheme);
			round.Fire(this);

			PlayEffects();
		}

		private void PlayEffects()
		{
			AudioHandler.Play(m_FireSfx);
			m_FireSpriteAnim.Animate(Muzzle.position, Muzzle.rotation);

			if (Source is PlayerInfo)
				View.PlayNoise(m_FireNoise);
		}

		protected override void OnTriggerPull()
		{
			enabled = true;
		}

		protected override void OnTriggerRelease()
		{
			enabled = false;
		}

	}
}