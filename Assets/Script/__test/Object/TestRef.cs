
namespace Bertis.Test
{
	using UnityEngine;

	[System.Serializable]
	public class TestRef : ObjectRef<TestInfo>
	{
		[SerializeField]
		private float m_Amplitude;
		[SerializeField]
		private int m_Count;

		protected override ObjectLib<TestInfo> Lib
		{
			get => TestLib.Reference;
		}

	}
}