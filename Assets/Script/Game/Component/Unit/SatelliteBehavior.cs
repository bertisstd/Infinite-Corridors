
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

		private SatelliteInfo m_SatelliteInfo;
		private NavMeshAgent  m_NavMeshAgent;
		private Animator      m_Animator;

		private State m_State;
		private bool  m_Attack;
		private bool  m_Attacking;
		private float m_AttackTimer;

		static SatelliteBehavior()
		{
			s_IdleHash   = Animator.StringToHash("Idle");
			s_ChaseHash  = Animator.StringToHash("Chase");
			s_AttackHash = Animator.StringToHash("Attack");
		}

		private void Awake()
		{
			m_SatelliteInfo = GetComponent<SatelliteInfo>();
			m_NavMeshAgent  = GetComponent<NavMeshAgent>();
			m_Animator      = GetComponent<Animator>();

			m_NavMeshAgent.updateRotation = false;
			m_NavMeshAgent.updateUpAxis   = false;

			m_SatelliteInfo.OnReaction += OnReaction;
			m_SatelliteInfo.OnReset    += Idle;
			m_DetectSensor.OnDetect    += OnDetectTrigger;
			m_AttackSensor.OnDetect    += OnAttackTrigger;

			Idle();
		}

		private void Update()
		{
			if (m_Attack && !m_Attacking)
			{
				var time = Time.time;
				if ((time - m_AttackTimer) >= m_AttackInterval)
				{
					Attack();
					m_AttackTimer = time;
				}
			}
		}

		private void FixedUpdate()
		{
			var playerRef = PlayerInfo.Reference;
			var playerPos = playerRef.transform.position;

			m_NavMeshAgent.SetDestination(playerPos);
			transform.rotation = Quaternion.Euler(0f, 0f, ext.Math.ToDegree(playerPos - transform.position));

			if (playerRef.Dead)
				Idle();
		}

		private void Idle()
		{
			if (gameObject.activeSelf)
				m_Animator.Play(s_IdleHash);

			m_Attacking = false;
			enabled = false;
			m_State = State.Idle;
		}

		private void Chase()
		{
			m_Attacking = false;
			m_Animator.Play(s_ChaseHash);
			enabled = true;
			m_State = State.Chase;
		}

		private void OnReaction(ref ReactionInfo reaction)
		{
			if (m_State == State.Idle && reaction.source is PlayerInfo)
				Chase();
		}

		private void OnDetectTrigger(UnitInfo unit, bool forward)
		{
			if (forward)
				Chase();
		}

		private void OnAttackTrigger(UnitInfo unit, bool forward)
		{
			m_Attack = forward;
		}

		private void Attack()
		{
			m_Animator.Play(s_AttackHash);
			m_State = State.Attack;
		}

#pragma warning disable IDE0051

		private void StartSlash()
		{
			m_Attacking = true;

			var playerRef = PlayerInfo.Reference;
			var playerPos = playerRef.transform.position;

			var distance = Vector2.Distance(
				transform.position,
				playerPos);

			if (distance <= m_AttackSensor.Radius)
				m_SatelliteInfo.DealDamage(playerRef, playerPos);
		}

		private void EndSlash()
		{
			m_Attacking = false;
			Chase();
		}

#pragma warning restore IDE0051

		private enum State
		{
			Idle,
			Chase,
			Attack
		}

	}
}