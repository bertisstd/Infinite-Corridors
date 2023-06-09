using FnId = Bertis.Runtime.FunctionUtility.Id;

namespace Bertis.Game
{
	using UnityEngine;
	using Bertis.Runtime;

	[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
	public class Round : MonoBehaviour
	{
		private Rifle m_Rifle;
		private FnId m_StopId;

		private Rigidbody2D m_Rigidbody;

		private void Awake()
		{
			m_Rigidbody = GetComponent<Rigidbody2D>();
		}

		private void OnTriggerEnter2D(Collider2D collider)
		{
			if (!m_Rifle.Pierce)
			{
				m_StopId.Abort();
				gameObject.SetActive(false);
			}
		}

		public void Fire(Rifle rifle)
		{
			m_Rifle = rifle;
			gameObject.SetActive(true);

			var muzzle = rifle.Muzzle;
			transform.SetPositionAndRotation(
				muzzle.position,
				muzzle.rotation);

			var direction = ext.Math.ToVector(muzzle.eulerAngles.z);
			m_Rigidbody.velocity = direction * rifle.RoundSpeed;

			m_StopId = FunctionUtility.InvokeDelayed(Stop, m_Rifle.RoundLifetime);
		}

		private void Stop()
		{
			gameObject.SetActive(false);
		}

	}
}