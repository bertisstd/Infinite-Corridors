using FnId = Bertis.Runtime.FunctionUtility.Id;

namespace Bertis.Test
{
	using Bertis.Runtime;
	using UnityEngine;

	public class Tester : MonoBehaviour
	{
		[SerializeField]
		private float m_Delay;

		private FnId m_FnId;

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Return))
			{
				fn();
				m_FnId = FunctionUtility.InvokeDelayed(fn, m_Delay);
			}
			else if (Input.GetKeyDown(KeyCode.Space))
			{
				m_FnId.Abort();
			}
		}

		void fn()
		{
			Diag.Log(Time.time);
		}

	}
}