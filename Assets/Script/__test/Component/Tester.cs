using FnId = Bertis.Runtime.FunctionUtility.Id;

namespace Bertis.Test
{
	using Bertis.Runtime;
	using UnityEngine;

	public class Tester : MonoBehaviour
	{
		[SerializeField]
		private TransformNoiseHandler m_Handler;
		[SerializeField]
		private TransformNoiseRef m_NoiseRef;

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Return))
			{
				m_Handler.Apply(m_NoiseRef);
			}
		}

	}
}