
namespace Bertis.UI
{
	using UnityEngine;

	[CreateAssetMenu(fileName = c_FileName, menuName = c_MenuName)]
	public class ConfigProvider : ScriptableObject
	{
		private const string c_FileName = nameof(ConfigProvider);
		private const string c_MenuName = "UI/" + c_FileName;

		static private readonly SJitter<ConfigProvider> s_This = new(c_MenuName);

		[SerializeField]
		private Library<GameObject> m_SchemeLibrary;

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

	}
}