
namespace Bertis.UI
{
	using System;
	using UnityEngine;
	using UnityEngine.UI;
	using Bertis.Game;

	public class UnitBar : MonoBehaviour
	{
		[SerializeField]
		private Image m_RatioImage;

		private UnitInfo m_UnitInfo;

		public UnitInfo UnitInfo
		{
			get => m_UnitInfo;
			set
			{
				if (value == null)
					throw new ArgumentNullException(nameof(value));

				m_UnitInfo = value;
				m_UnitInfo.OnHealthChanged += OnHealthChanged;
				OnHealthChanged();

				gameObject.SetActive(true);
			}
		}

		protected void OnDisable()
		{
			if (m_UnitInfo != null)
			{
				m_UnitInfo.OnHealthChanged -= OnHealthChanged;
				m_UnitInfo = null;
			}
		}

		private void OnHealthChanged()
		{
			m_RatioImage.fillAmount = m_UnitInfo.Health.Ratio;
		}

	}
}