
namespace Bertis.Runtime
{
	using UnityEngine;

	[System.Serializable]
	public sealed class SpriteAnimInfo : ObjectInfo
	{
		[SerializeField]
		private Sprite[] m_Sprites;
		[SerializeField]
		private float m_Duration;
		[SerializeField]
		private int m_RenderOrder;
		[SerializeField, ReadOnly]
		private bool m_Valid;

		public bool Valid
		{
			get => m_Valid;
		}
		public Sprite[] Sprites
		{
			get => m_Sprites;
		}
		public float Duration
		{
			get => m_Duration;
		}
		public int RenderOrder
		{
			get => m_RenderOrder;
		}

	}
}