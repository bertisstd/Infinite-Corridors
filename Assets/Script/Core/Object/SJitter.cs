
namespace Bertis
{
	public sealed class SJitter<T> where T : UnityEngine.ScriptableObject
	{
		private T m_Value;
		private bool m_Initialized;
		private readonly string m_MenuName;

		public SJitter(string menuName)
		{
			m_MenuName = menuName;
		}

		public T Get()
		{
			if (!m_Initialized)
			{
				m_Value = ext.ScriptableObject.Load<T>(m_MenuName);
				m_Initialized = true;
			}

			return m_Value;
		}

	}
}