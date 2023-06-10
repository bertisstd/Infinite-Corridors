
namespace Bertis.Game
{
	using System;
	using UnityEngine;

	[RequireComponent(typeof(PlayerInfo), typeof(Animator), typeof(Rigidbody2D))]
	public sealed class PlayerBehavior : MonoBehaviour
	{
		private Action m_Update;

		private int m_IdleHash;
		private int m_WalkHash;

		private PlayerInfo m_PlayerInfo;
		private Rigidbody2D m_Rigidbody2D;
		private Animator m_Animator;

		private void Awake()
		{
			m_IdleHash = Animator.StringToHash("Idle");
			m_WalkHash = Animator.StringToHash("Walk");

			m_PlayerInfo  = GetComponent<PlayerInfo>();
			m_Rigidbody2D = GetComponent<Rigidbody2D>();
			m_Animator    = GetComponent<Animator>();
		}

		private void Update()
		{
			m_Update();
		}

		private void OnEnable()
		{
			StartIdle();
		}

		private void StartIdle()
		{
			m_Rigidbody2D.velocity = Vector2.zero;
			m_Animator.Play(m_IdleHash);
			m_Update = UpdateIdle;
		}

		private void UpdateIdle()
		{
			if (GetDirection(out _))
			{
				StartWalk();
			}
			else
			{
				UpdateRotation();
				UpdateGunTrigger();
			}
		}

		private void StartWalk()
		{
			m_Animator.Play(m_WalkHash);
			m_Update = UpdateWalk;
		}

		private void UpdateWalk()
		{
			if (GetDirection(out var direction))
			{
				m_Rigidbody2D.velocity = direction * m_PlayerInfo.MoveSpeed;

				UpdateRotation();
				UpdateGunTrigger();
			}
			else
			{
				StartIdle();
			}
		}

		private bool GetDirection(out Vector3 direction)
		{
			var ret = false;
			direction = Vector3.zero;

			if (Input.GetKey(KeyCode.A)) { direction.x = -1.0f; ret = true; } else 
			if (Input.GetKey(KeyCode.D)) { direction.x = +1.0f; ret = true; } 

			if (Input.GetKey(KeyCode.S)) { direction.y = -1.0f; ret = true; } else 
			if (Input.GetKey(KeyCode.W)) { direction.y = +1.0f; ret = true; }

			return ret;
		}

		private void UpdateRotation()
		{
			var angle = ext.Math.ToDegree(View.GetPointerWorldPosition() - transform.position);
			transform.rotation = Quaternion.Euler(0f, 0f, angle);
		}

		private void UpdateGunTrigger()
		{
			var gun = m_PlayerInfo.Gun;
			if (gun != null)
			{
				gun.PullTrigger(Input.GetMouseButton(0));
			}
		}

	}
}