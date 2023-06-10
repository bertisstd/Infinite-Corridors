using FnId = Bertis.Runtime.FunctionUtility.Id;

namespace Bertis.Test
{
	using UnityEngine;
	using Bertis.Runtime;
	using Bertis.Game;

	public class Tester : MonoBehaviour
	{
		[SerializeField]
		private float m_Value;

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Return))
			{
				StageHandler.GotoNextStage();
			}
		}

	}
}