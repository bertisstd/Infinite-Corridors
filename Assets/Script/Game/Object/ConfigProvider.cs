
namespace Bertis.Game
{
	using UnityEngine;

	[CreateAssetMenu(fileName = c_FileName, menuName = c_MenuName)]
	public class ConfigProvider : ScriptableObject
	{
		private const string c_FileName = nameof(ConfigProvider);
		private const string c_MenuName = "Game/" + c_FileName;

		static private readonly SJitter<ConfigProvider> s_This = new(c_MenuName);

		static private ConfigProvider This
		{
			get => s_This.Get();
		}

	}
}