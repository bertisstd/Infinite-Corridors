
namespace Bertis.Runtime
{
	using UnityEngine;

	[CreateAssetMenu(fileName = c_FileName, menuName = c_MenuName)]
	public sealed class WorldSpriteLib : ObjectLib<WorldSpriteInfo>
	{
		private const string c_FileName = nameof(WorldSpriteLib);
		private const string c_MenuName = "Runtime/" + c_FileName;

		static private readonly SJitter<WorldSpriteLib> s_Reference = new(c_MenuName);

		static public WorldSpriteLib Reference
		{
			get => s_Reference.Get();
		}

	}
}