
namespace Bertis.Game
{
	using System;
	using UnityEngine;
	using Bertis.Runtime;

	public class UnitInfo : MonoBehaviour
	{
		public event Action OnHealthChanged;

		[Header("Info")]
		[SerializeField]
		private UnitTypeRef m_UnitType;

		[Header("Offensive")]
		[SerializeField]
		private float m_Damage;
		[SerializeField]
		private float m_CriticalChance;
		[SerializeField]
		private float m_CriticalDamage;

		[Header("Defensive")]
		[SerializeField]
		private Resource m_Health;
		[SerializeField]
		private float m_Resistance;

		public UnitTypeInfo UnitType
		{
			get => m_UnitType.Info;
		}
		public float Damage
		{
			get => m_Damage;
			set => m_Damage = value;
		}
		public float CriticalChance
		{
			get => m_CriticalChance;
			set => m_CriticalChance = value;
		}
		public float CriticalDamage
		{
			get => m_CriticalDamage;
			set => m_CriticalDamage = value;
		}
		public Resource Health
		{
			get => m_Health;
			set
			{
				m_Health = value;
				OnHealthChanged?.Invoke();
			}
		}
		public float Resistance
		{
			get => m_Resistance;
			set => m_Resistance = value;
		}

		public void DealDamage(UnitInfo target, Vector3 reactionPosition)
		{
			float damage;

			if (target.UnitType.Static)
			{
				damage = 0f;
			}
			else
			{
				var reaction = new ReactionInfo(this, target);
				damage = reaction.damage;
			}

			if (damage > 0f)
			{
				var health = target.Health;
				health.Current -= damage;
				target.Health = health;
			}

			var type = UnitType;

			AudioClip sfx;
			ParticleSystem pfx;
			if (target.Health.Depleted)
			{
				sfx = type.DeathSfx;
				pfx = type.DeathPfx;
				type.DeathSprite.Place(reactionPosition);
				target.Die();
			}
			else
			{
				sfx = type.HitSfx;
				pfx = type.HitPfx;
			}

			AudioHandler.Play(sfx);
			PfxHandler.Play(pfx, reactionPosition);
		}

		public void Die()
		{
			gameObject.SetActive(false);
		}

	}
}