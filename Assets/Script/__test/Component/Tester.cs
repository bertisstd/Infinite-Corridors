using FnId = Bertis.Runtime.FunctionUtility.Id;

namespace Bertis.Test
{
	using UnityEngine;
	using Bertis.Runtime;
	using Bertis.Game;

	public class Tester : MonoBehaviour
	{
		[SerializeField]
		private Gun m_Gun;

		private void Update()
		{
			m_Gun.PullTrigger(Input.GetKey(KeyCode.Space));
		}

	}
}