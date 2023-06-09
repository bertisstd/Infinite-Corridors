
namespace Bertis
{
	using UnityEngine;

	public abstract class ObjectRef<T> where T : ObjectInfo
	{
		[SerializeField, HideInInspector]
		private int m_Id;

		public T Info
		{
			get
			{
				return Lib.GetInfo(m_Id);
			}
		}

		protected abstract ObjectLib<T> Lib { get; }

	}
}