
namespace Bertis.Game
{
	using UnityEngine;

	[CreateAssetMenu(fileName = c_FileName, menuName = c_MenuName)]
	public class ConfigProvider : ConfigProviderBase
	{
		private const string c_FileName = nameof(ConfigProvider);
		private const string c_MenuName = "Game/" + c_FileName;

		static private readonly SJitter<ConfigProvider> s_This = new(c_MenuName);

		static private ConfigProvider This
		{
			get => s_This.Get();
		}

		static public GameObject GetScheme(int id)
		{
			return This.m_SchemeLibrary.GetValue(id);
		}
		static public T GetScheme<T>(int id) where T : Component
		{
			return GetScheme(id).GetComponent<T>();
		}
		static public float GetSingle(int id)
		{
			return This.m_SingleLibrary.GetValue(id);
		}
		static public int GetInt32(int id)
		{
			return This.m_Int32Library.GetValue(id);
		}
		static public bool GetBoolean(int id)
		{
			return This.m_BooleanLibrary.GetValue(id);
		}
		static public Range GetRange(int id)
		{
			return This.m_RangeLibrary.GetValue(id);
		}

	}
}