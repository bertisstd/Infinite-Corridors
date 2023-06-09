
namespace Bertis.Game
{
	using UnityEngine;

	[CreateAssetMenu(fileName = c_FileName, menuName = c_MenuName)]
	public sealed class UnitTypeLib : ObjectLib<UnitTypeInfo>
	{
		private const string c_FileName = nameof(UnitTypeLib);
		private const string c_MenuName = "Game/" + c_FileName;

		static private readonly SJitter<UnitTypeLib> s_Reference = new(c_MenuName);

		static public UnitTypeLib Reference
		{
			get => s_Reference.Get();
		}

	}
}