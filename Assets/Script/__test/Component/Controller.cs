
namespace Bertis.Test
{
	using UnityEngine;

	public class Controller : MonoBehaviour
	{
		[SerializeField]
		private float m_MoveSpeed;
		[SerializeField]
		private float m_RotateDir;

		private void Update()
		{
			var moveDir = Vector3.zero;

			if (Input.GetKey(KeyCode.W)) { moveDir.y = +m_MoveSpeed; } else
			if (Input.GetKey(KeyCode.S)) { moveDir.y = -m_MoveSpeed; }

			if (Input.GetKey(KeyCode.A)) { moveDir.x = -m_MoveSpeed; } else
			if (Input.GetKey(KeyCode.D)) { moveDir.x = +m_MoveSpeed; }

			transform.position += moveDir * Time.deltaTime;

			var rotDeg = 0f;

			if (Input.GetKey(KeyCode.LeftArrow)) { rotDeg = -m_RotateDir; } else
			if (Input.GetKey(KeyCode.RightArrow)) { rotDeg = +m_RotateDir; }

			transform.eulerAngles += rotDeg * Time.deltaTime * Vector3.forward;
		}

	}
}