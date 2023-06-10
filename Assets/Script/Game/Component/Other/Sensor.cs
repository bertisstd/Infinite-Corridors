
namespace Bertis.Game
{
	using System;
	using UnityEngine;

	[RequireComponent(typeof(CircleCollider2D))]
	public class Sensor : MonoBehaviour
	{
		public event Action<UnitInfo, bool> OnDetect;

		private CircleCollider2D m_Collider;

		public bool Active
		{
			get => m_Collider.enabled;
			set => m_Collider.enabled = value;
		}
		public float Radius
		{
			get => m_Collider.radius;
			set => m_Collider.radius = value;
		}

		private void Awake()
		{
			m_Collider = GetComponent<CircleCollider2D>();
		}

		private void OnTriggerEnter2D(Collider2D collider)
		{
			if (collider.TryGetComponent<UnitInfo>(out var unit))
			{
				OnDetect?.Invoke(unit, true);
			}
		}

		private void OnTriggerExit2D(Collider2D collider)
		{
			if (collider.TryGetComponent<UnitInfo>(out var unit))
			{
				OnDetect?.Invoke(unit, false);
			}
		}

	}
}