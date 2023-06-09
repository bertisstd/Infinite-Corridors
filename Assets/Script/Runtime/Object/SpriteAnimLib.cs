
namespace Bertis.Runtime
{
	using UnityEngine;

	[CreateAssetMenu(fileName = c_FileName, menuName = c_MenuName)]
	public sealed class SpriteAnimLib : ObjectLib<SpriteAnimInfo>
	{
		private const string c_FileName = nameof(SpriteAnimLib);
		private const string c_MenuName = "Runtime/" + c_FileName;

		static private readonly SJitter<SpriteAnimLib> s_Reference = new(c_MenuName);

		static public SpriteAnimLib Reference
		{
			get => s_Reference.Get();
		}

	}
}