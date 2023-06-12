
namespace Bertis.Game
{
	using UnityEngine;

	[CreateAssetMenu(fileName = c_FileName, menuName = c_MenuName)]
	public sealed class GameUtility : ScriptableObject
	{
		private const string c_FileName = nameof(GameUtility);
		private const string c_MenuName = "Game/" + c_FileName;

		static private readonly SJitter<GameUtility> s_This = new(c_MenuName);

		static private GameUtility This
		{
			get => s_This.Get();
		}

		static public void Pause(bool forward)
		{
			Time.timeScale = forward ? 0 : 1;
		}

		static public void Quit()
		{
			Application.Quit();
		}

	}
}