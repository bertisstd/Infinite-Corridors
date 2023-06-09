using FnId = Bertis.Runtime.FunctionUtility.Id;

namespace Bertis.Test
{
	using Bertis.Runtime;
	using UnityEngine;

	public class Tester : MonoBehaviour
	{
		[SerializeField]
		private WorldSpriteRef m_Ref;

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Return))
			{
				m_Ref.Place(transform.position);
			}
		}

	}
}