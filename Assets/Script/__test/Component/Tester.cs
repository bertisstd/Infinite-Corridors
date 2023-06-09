using FnId = Bertis.Runtime.FunctionUtility.Id;

namespace Bertis.Test
{
	using Bertis.Runtime;
	using Bertis.Runtime.Audio;
	using UnityEngine;

	public class Tester : MonoBehaviour
	{
		[SerializeField]
		private AudioClip m_Clip;

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Return))
			{
				AudioHandler.Play(m_Clip);
			}
		}

	}
}