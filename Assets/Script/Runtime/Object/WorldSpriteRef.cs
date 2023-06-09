
namespace Bertis.Runtime
{
	[System.Serializable]
	public sealed class WorldSpriteRef : ObjectRef<WorldSpriteInfo>
	{
		public bool GetInfo(out WorldSpriteInfo info)
		{
			return (info = Info).Valid;
		}

		protected override ObjectLib<WorldSpriteInfo> Lib
		{
			get => WorldSpriteLib.Reference;
		}

	}
}