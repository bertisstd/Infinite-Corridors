
namespace Bertis.Test
{
	using UnityEngine;
	using Bertis.Game;

	public sealed class InputHandler : IInputHandler
	{
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		static private void Initialize()
		{
			if (GameUtility.GetInputSource() == GameUtility.InputSource.MouseKeyboard)
			{
				Game.InputHandler.Handler = new InputHandler();
			}
		}

		public bool GetMovementDirection(out Vector3 direction)
		{
			var ret = false;
			direction = Vector3.zero;

			if (Input.GetKey(KeyCode.A)) { direction.x = -1.0f; ret = true; } else 
			if (Input.GetKey(KeyCode.D)) { direction.x = +1.0f; ret = true; }

			if (Input.GetKey(KeyCode.S)) { direction.y = -1.0f; ret = true; } else 
			if (Input.GetKey(KeyCode.W)) { direction.y = +1.0f; ret = true; }

			return ret;
		}
		public bool GetRotationDirection(out Vector3 direction)
		{
			direction = View.GetPointerWorldPosition() - PlayerInfo.Reference.transform.position;
			return true;
		}
		public bool GetFiring()
		{
			return Input.GetMouseButton(0);
		}

	}
}