using FnId = Bertis.Runtime.FunctionUtility.Id;

namespace Bertis.Test
{
	using Bertis.Runtime;
	using UnityEngine;

	public class Tester : MonoBehaviour
	{
		[SerializeField]
		private SpriteAnimRef m_Anim;

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Return))
			{
				m_Anim.Animate(transform.position, transform.rotation);
			}
		}

	}
}