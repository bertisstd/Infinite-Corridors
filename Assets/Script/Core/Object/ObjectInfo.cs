
namespace Bertis
{
	using UnityEngine;

	public class ObjectInfo
	{
		[SerializeField]
		private string m_Name;
		[SerializeField, ReadOnly]
		private int m_Id;

		public string Name
		{
			get => m_Name;
		}
		public int Id
		{
			get => m_Id;
		}

	}
}