
namespace Bertis.Game
{
	[System.Serializable]
	public sealed class UnitTypeRef : ObjectRef<UnitTypeInfo>
	{
		protected override ObjectLib<UnitTypeInfo> Lib
		{
			get => UnitTypeLib.Reference;
		}

	}
}