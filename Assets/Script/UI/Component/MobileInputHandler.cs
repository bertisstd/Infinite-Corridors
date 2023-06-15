
namespace Bertis.UI
{
	using UnityEngine;
	using Bertis.Runtime;
	using Bertis.Game;

	public sealed class MobileInputHandler : MonoBehaviour, IInputHandler
	{
		[SerializeField]
		private VirtualJoystick m_MovementJoystick;
		[SerializeField]
		private VirtualJoystick m_RotationJoystick;

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		static private void Initialize()
		{
			//if (GameUtility.GetInputSource() == GameUtility.InputSource.ScreenTouch)
			{
				var scheme = ConfigProvider.GetScheme<MobileInputHandler>(-1370831324); /*MobileInputHandler*/
				var instance = Hierarchy.CreateComponent(scheme);
				InputHandler.Handler = instance;
			}
		}

		public bool GetMovementDirection(out Vector3 direction)
		{
			direction = m_MovementJoystick.Direction;
			return m_MovementJoystick.Hold;
		}

		public bool GetRotationDirection(out Vector3 direction)
		{
			var ret = false;
			direction = Vector2.zero;

			if (m_RotationJoystick.Hold)
			{
				direction = m_RotationJoystick.Direction;
				ret = true;
			}
			else if (m_MovementJoystick.Hold)
			{
				direction = m_MovementJoystick.Direction;
				ret = true;
			}

			return ret;
		}

		public bool GetFiring()
		{
			return m_RotationJoystick.Hold;
		}

	}
}