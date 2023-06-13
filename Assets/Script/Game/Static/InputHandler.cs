
namespace Bertis.Game
{
	using UnityEngine;

	static public class InputHandler
	{
		static public IInputHandler Handler { get; set; }

		static public bool GetMovementDirection(out Vector3 direction)
		{
			return Handler.GetMovementDirection(out direction);
		}
		static public bool GetRotationDirection(out Vector3 direction)
		{
			return Handler.GetRotationDirection(out direction);
		}
		static public bool GetFiring()
		{
			return Handler.GetFiring();
		}

	}
}