using Label = TMPro.TextMeshProUGUI;

namespace Bertis.UI
{
	using UnityEngine;
	using Bertis.Game;

	[RequireComponent(typeof(Canvas), typeof(Animator))]
	public class FloatingText : MonoBehaviour
	{
		static private readonly int s_MessageHash;
		static private readonly int s_DamageHash;
		static private readonly int s_CriticalHash;
		static private readonly int s_HealHash;

		[SerializeField]
		private Label m_Label;

		private Animator m_Animator;

		static FloatingText()
		{
			s_MessageHash  = Animator.StringToHash("Message");
			s_DamageHash   = Animator.StringToHash("Damage");
			s_CriticalHash = Animator.StringToHash("Critical");
			s_HealHash     = Animator.StringToHash("Heal");
		}

		private void Awake()
		{
			var canvas = GetComponent<Canvas>();
			canvas.worldCamera = View.Camera;

			m_Animator = GetComponent<Animator>();
		}

		public void OnMessage(Vector3 position, string message)
		{
			gameObject.SetActive(true);
			transform.position = position;
			m_Label.text = message;
			m_Animator.Play(s_MessageHash);
		}

		public void OnReaction(ref ReactionInfo reaction)
		{
			gameObject.SetActive(true);
			transform.position = reaction.target.transform.position;

			int hash;
			string text;

			var damage = reaction.damage.ToString("F0");

			if (reaction.critical)
			{
				hash = s_CriticalHash;
				text = $"!{damage}";
			}
			else
			{
				hash = s_DamageHash;
				text = damage;
			}

			m_Label.text = text;
			m_Animator.Play(hash);
		}

		public void OnHeal(UnitInfo unit, float amount)
		{
			gameObject.SetActive(true);
			transform.position = unit.transform.position;
			m_Label.text = $"+{amount:F0}";
			m_Animator.Play(s_HealHash);
		}

		[System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051", Justification = "<Pending>")]
		private void Deactivate()
		{
			gameObject.SetActive(false);
		}

	}
}