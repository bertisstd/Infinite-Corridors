
namespace Bertis.Runtime
{
	[System.Serializable]
	public sealed class SpriteAnimRef : ObjectRef<SpriteAnimInfo>
	{
		public bool GetInfo(out SpriteAnimInfo info)
		{
			return (info = Info).Valid;
		}

		protected override ObjectLib<SpriteAnimInfo> Lib
		{
			get => SpriteAnimLib.Reference;
		}

	}
}