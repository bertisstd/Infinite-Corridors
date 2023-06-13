
namespace Bertis.Game
{
	using UnityEngine;

	public interface IInputHandler
	{
		bool GetMovementDirection(out Vector3 direction);
		bool GetRotationDirection(out Vector3 direction);
		bool GetFiring();

	}
}