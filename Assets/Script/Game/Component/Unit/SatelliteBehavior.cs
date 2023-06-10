
namespace Bertis.Game
{
	using UnityEngine;
	using UnityEngine.AI;

	[RequireComponent(typeof(SatelliteInfo), typeof(NavMeshAgent), typeof(Animator))]
	public class SatelliteBehavior : MonoBehaviour
	{
		static private readonly int s_IdleHash;
		static private readonly int s_ChaseHash;
		static private readonly int s_AttackHash;

		[SerializeField]
		private float m_AttackInterval;
		[SerializeField]
		private Sensor m_DetectSensor;
		[SerializeField]
		private Sensor m_AttackSensor;

		private bool m_Attacking;
		private float m_AttackTimer;

		private SatelliteInfo m_SatelliteInfo;
		private NavMeshAgent m_NavMeshAgent;
		private Animator m_Animator;

		static SatelliteBehavior()
		{
			s_IdleHash   = Animator.StringToHash("Idle");
			s_ChaseHash  = Animator.StringToHash("Chase");
			s_AttackHash = Animator.StringToHash("Attack");
		}

		private void Awake()
		{
			m_SatelliteInfo = GetComponent<SatelliteInfo>();
			m_SatelliteInfo.OnReaction += OnReaction;
			m_SatelliteInfo.OnVisibilityChanged += OnVisibilityChanged;

			m_Animator = GetComponent<Animator>();

			m_NavMeshAgent = GetComponent<NavMeshAgent>();
			m_NavMeshAgent.updateRotation = false;
			m_NavMeshAgent.updateUpAxis = false;

			m_DetectSensor.OnDetect += OnDetectTrigger;
			m_AttackSensor.OnDetect += OnAttackTrigger;

			Idle();
		}

		private void Update()
		{
			if (m_Attacking)
			{
				if ((m_AttackTimer -= Time.deltaTime) <= 0f)
				{
					Attack();
					m_AttackTimer = m_AttackInterval;
				}
			}
		}

		private void FixedUpdate()
		{
			var targetPos = PlayerInfo.Reference.transform.position;
			m_NavMeshAgent.SetDestination(targetPos);

			var deltaAngle = ext.Math.ToDegree(targetPos - transform.position);
			transform.rotation = Quaternion.Euler(0f, 0f, deltaAngle);
		}
		
		private void OnReaction(ref ReactionInfo reaction)
		{
			if (reaction.source is PlayerInfo)
			{
				Chase();
			}
		}

		private void OnDetectTrigger(UnitInfo target, bool forward)
		{
			if (target is PlayerInfo)
			{
				if (forward)
				{
					Chase();
				}
				else
				{
					Idle();
				}
			}
		}

		private void OnAttackTrigger(UnitInfo target, bool forward)
		{
			m_Attacking = forward;
		}

		private void OnVisibilityChanged()
		{
			if (!m_SatelliteInfo.Visible)
			{
				enabled = false;
			}
		}

		private void Idle()
		{
			if (gameObject.activeSelf)
			{
				m_Animator.Play(s_IdleHash);
				enabled = false;
			}
		}

		private void Chase()
		{
			m_Animator.Play(s_ChaseHash);
			enabled = true;
		}

		private void Attack()
		{
			m_Animator.Play(s_AttackHash);
		}

		[System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051", Justification = "<Pending>")]
		private void Slash()
		{
			var player = PlayerInfo.Reference;
			var playerPos = player.transform.position;
			var distance = Vector2.Distance(transform.position, playerPos);
			if (distance <= m_AttackSensor.Radius)
			{
				m_SatelliteInfo.DealDamage(player, playerPos);
			}
		}

	}
}