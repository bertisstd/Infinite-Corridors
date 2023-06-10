
namespace Bertis.Game
{
	using UnityEngine;
	using Bertis.Runtime;

	[RequireComponent(typeof(Camera))]
	public sealed class View : MonoBehaviour
	{
		static private float s_FollowSpeed;
		static private float s_Depth;

		static private View s_This;
		static private Camera s_Camera;

		[SerializeField]
		private TransformNoiseHandler m_NoiseHandler;

		static public Camera Camera
		{
			get => s_Camera;
		}
		static public float Depth
		{
			get => s_Depth;
		}

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
		static private void Initialize()
		{
			s_FollowSpeed = ConfigProvider.GetSingle(+1512779511); /*ViewFollowSpeed*/
		}

		static public Vector3 GetPointerWorldPosition()
		{
			var screenPos = Input.mousePosition;
			screenPos.z = -s_Depth;
			return s_Camera.ScreenToWorldPoint(screenPos);
		}

		static public void PlayNoise(TransformNoiseRef transformNoiseRef)
		{
			s_This.m_NoiseHandler.Play(transformNoiseRef);
		}

		private void Awake()
		{
			s_This = this;
			s_Camera = GetComponent<Camera>();
			s_Depth = transform.position.z;
		}

		private void FixedUpdate()
		{
			transform.localPosition = Vector2.Lerp(
				transform.localPosition,
				PlayerInfo.Reference.Front.position,
				s_FollowSpeed * Time.deltaTime);
		}

	}
}