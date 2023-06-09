
namespace Bertis.Test
{
	using UnityEngine;

	[CreateAssetMenu(fileName = c_FileName, menuName = c_MenuName)]
	public class TestLib : ObjectLib<TestInfo>
	{
		private const string c_FileName = nameof(TestLib);
		private const string c_MenuName = "__test/" + c_FileName;

		static private readonly SJitter<TestLib> s_Reference = new(c_MenuName);

		static public TestLib Reference
		{
			get => s_Reference.Get();
		}

	}
}