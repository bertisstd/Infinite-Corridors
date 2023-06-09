
namespace Bertis.UI
{
	using System;
	using UnityEngine;
	using Bertis.Game;

	[RequireComponent(typeof(Canvas))]
	public sealed class SatelliteBar : UnitBar
	{
		private SatelliteInfo m_SatelliteInfo;

		public SatelliteInfo SatelliteInfo
		{
			get => m_SatelliteInfo;
			set
			{
				if (value == null)
					throw new ArgumentNullException(nameof(value));

				UnitInfo = value;

				m_SatelliteInfo = value;
				m_SatelliteInfo.OnVisibilityChanged += OnVisibilityChanged;
			}
		}

		private void Awake()
		{
			var canvas = GetComponent<Canvas>();
			canvas.worldCamera = View.Camera;
		}

		private void Update()
		{
			transform.position = m_SatelliteInfo.BarPosition;
		}

		private new void OnDisable()
		{
			if (m_SatelliteInfo != null)
			{
				m_SatelliteInfo.OnVisibilityChanged -= OnVisibilityChanged;
				m_SatelliteInfo = null;
			}
		}

		private void OnVisibilityChanged()
		{
			if (!m_SatelliteInfo.Visible)
			{
				gameObject.SetActive(false);
			}
		}

	}
}