
namespace Bertis.Game
{
	using UnityEngine;

	[RequireComponent(typeof(Camera))]
	public sealed class View : MonoBehaviour
	{
		static private float s_Depth;

		static private View s_This;
		static private Camera s_Camera;

		static public Camera Camera
		{
			get => s_Camera;
		}
		static public float Depth
		{
			get => s_Depth;
		}

		static public Vector3 GetPointerWorldPosition()
		{
			var screenPos = Input.mousePosition;
			screenPos.z = -s_Depth;
			return s_Camera.ScreenToWorldPoint(screenPos);
		}

		private void Awake()
		{
			s_This = this;
			s_Camera = GetComponent<Camera>();
			s_Depth = transform.position.z;
		}

	}
}