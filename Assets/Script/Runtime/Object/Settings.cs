
namespace Bertis.Runtime
{
	using UnityEngine;

	[CreateAssetMenu(fileName = c_FileName, menuName = c_MenuName)]
	public sealed class Settings : ScriptableObject
	{
		private const string c_FileName = nameof(Settings);
		private const string c_MenuName = "Runtime/" + c_FileName;

		static private readonly SJitter<Settings> s_This = new(c_MenuName);

		[SerializeField]
		private float m_AudioVolume;

		static private Settings This
		{
			get => s_This.Get();
		}

		static public float AudioVolume
		{
			get => This.m_AudioVolume;
			set => This.m_AudioVolume = value;
		}

	}
}