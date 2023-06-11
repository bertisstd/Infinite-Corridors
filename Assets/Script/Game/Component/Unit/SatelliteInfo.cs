
namespace Bertis.Game
{
	using System;
	using UnityEngine;

	public class SatelliteInfo : UnitInfo
	{
		static public event Action<SatelliteInfo> OnVisibilityChangedStatic;

		public event Action OnVisibilityChanged;
		public event Action OnReset;

		[SerializeField]
		private Transform m_Bar;

		private bool m_Visible;
		private Vector3 m_BarOffset;

		public bool Visible
		{
			get => m_Visible;
		}
		public Vector3 BarPosition
		{
			get => transform.position + m_BarOffset;
		}

		private void Awake()
		{
			m_BarOffset = m_Bar.localPosition;
		}

		private void OnBecameVisible()
		{
			SetVisible(true);
		}

		private void OnBecameInvisible()
		{
			SetVisible(false);
		}

		private void SetVisible(bool value)
		{
			m_Visible = value;
			OnVisibilityChangedStatic?.Invoke(this);
			OnVisibilityChanged?.Invoke();
		}

		public void ResetProperties()
		{
			var health = Health;
			health.Current = health.Max;
			Health = health;

			OnReset?.Invoke();
		}

	}
}