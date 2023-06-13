
namespace Bertis.UI
{
	using UnityEngine;
	using UnityEngine.EventSystems;
	using UnityEngine.UI;

	[RequireComponent(typeof(Image))]
	public class VirtualJoystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
	{
		[SerializeField]
		private Image m_Handle;

		private bool m_Hold;

		public bool Hold
		{
			get => m_Hold;
		}

		public Vector3 Direction
		{
			get
			{
				return (m_Handle.transform.position - transform.position).normalized;
			}
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			m_Hold = true;
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			m_Handle.transform.localPosition = Vector3.zero;
			m_Hold = false;
		}

		public void OnDrag(PointerEventData eventData)
		{
			m_Handle.transform.position = eventData.position;
		}

	}
}