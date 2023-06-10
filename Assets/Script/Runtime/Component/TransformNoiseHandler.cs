using static Bertis.Runtime.TransformNoiseInfo;

namespace Bertis.Runtime
{
	using System;
	using UnityEngine;

	public class TransformNoiseHandler : MonoBehaviour
	{
		private Cache m_Cache;

		private void Awake()
		{
			m_Cache = new(transform);
			enabled = false;
		}

		private void Update()
		{
			for (var i = m_Cache.Count; --i >= 0;)
			{
				var elem = m_Cache[i];
				if (elem.Active && elem.Update() && --m_Cache.ActiveCount == 0)
				{
					transform.localPosition = Vector3.zero;
					transform.localRotation = Quaternion.identity;

					enabled = false;
					return;
				}
			}
		}

		public void Play(TransformNoiseRef transformNoiseRef)
		{
			if (transformNoiseRef == null)
				throw new ArgumentNullException(nameof(transformNoiseRef));

			if (transformNoiseRef.GetInfo(out var info))
			{
				m_Cache.Provide().Restart(info, transformNoiseRef.Params);

				if (++m_Cache.ActiveCount == 1)
					enabled = true;
			}
		}

		private class Processor : IStatusProvider
		{
			private readonly Transform m_Transform;

			private Function3 m_Position;
			private Function3 m_Rotation;

			private Vector3   m_PosMul;
			private Vector3   m_RotMul;
			private float     m_Timer;
			private float     m_TimerMul;

			private Vector3   m_PrevPos;
			private Vector3   m_PrevRot;

			public Processor(Transform transform)
			{
				m_Transform = transform;
			}

			public bool Active { get; private set; }

			public void Restart(TransformNoiseInfo info, EffectParams @params)
			{
				m_Position = info.Position;
				m_Rotation = info.Rotation;

				m_PosMul   = RNG.GenVector3Sign() * @params.Amplitude;
				m_RotMul   = RNG.GenVector3Sign() * @params.Amplitude;
				m_Timer    = 0f;
				m_TimerMul = 1f / @params.Duration;

				m_PrevPos = Vector3.zero;
				m_PrevRot = Vector3.zero;

				Active = true;
			}

			public bool Update()
			{
				bool ret;

				m_Timer += Time.deltaTime * m_TimerMul;

				if (m_Timer >= 1f)
				{
					m_Timer = 1f;
					ret = true;
					Active = false;
				}
				else
				{
					ret = false;
				}

				if (m_Position.Valid)
				{
					var curr = m_Position[m_Timer, m_PosMul];
					m_Transform.localPosition += curr - m_PrevPos;
					m_PrevPos = curr;
				}
				if (m_Rotation.Valid)
				{
					var curr = m_Rotation[m_Timer, m_RotMul];
					m_Transform.localEulerAngles += curr - m_PrevRot;
					m_PrevRot = curr;
				}

				return ret;
			}

		}
		
		private sealed class Cache : Cache<Processor>
		{
			private readonly Transform m_Transform;

			public Cache(Transform transform)
			{
				m_Transform = transform;
			}

			public new int ActiveCount { get; set; }

			protected override Processor Create()
			{
				return new(m_Transform);
			}

		}

	}
}