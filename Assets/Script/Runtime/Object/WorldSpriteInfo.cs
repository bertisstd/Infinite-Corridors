
namespace Bertis.Runtime
{
	using UnityEngine;

	[System.Serializable]
	public sealed class WorldSpriteInfo : ObjectInfo
	{
		[SerializeField]
		private Set<Sprite> m_SpriteSet;
		[SerializeField]
		private int m_RenderOrder;

		public bool Valid
		{
			get => m_SpriteSet.Count > 0;
		}
		public Set<Sprite> SpriteSet
		{
			get => m_SpriteSet;
		}
		public int RenderOrder
		{
			get => m_RenderOrder;
		}

	}
}