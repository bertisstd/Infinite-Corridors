
namespace Bertis.Test
{
	using Bertis.Game;
	using UnityEngine;

	public class Controller : MonoBehaviour
	{
		[SerializeField]
		private float m_MoveSpeed;

		private void Update()
		{
			var moveDir = Vector3.zero;

			if (Input.GetKey(KeyCode.W)) { moveDir.y = +m_MoveSpeed; } else
			if (Input.GetKey(KeyCode.S)) { moveDir.y = -m_MoveSpeed; }

			if (Input.GetKey(KeyCode.A)) { moveDir.x = -m_MoveSpeed; } else
			if (Input.GetKey(KeyCode.D)) { moveDir.x = +m_MoveSpeed; }

			transform.position += moveDir * Time.deltaTime;

			var delta = View.GetPointerWorldPosition() - transform.position;
			transform.rotation = Quaternion.Euler(0f, 0f, ext.Math.ToDegree(delta));
		}

	}
}