
namespace Bertis.Runtime
{
	using System;
	using UnityEngine;

	[Serializable]
	public struct CollectionParams
	{
		[SerializeField]
		private int m_InitSize;
		[SerializeField]
		private int m_Accretion;

		public int InitSize
		{
			get => m_InitSize;
		}
		public int Accretion
		{
			get => Math.Max(m_Accretion, 1);
		}

	}
}