
namespace Bertis.Game
{
	using UnityEngine;

	[CreateAssetMenu(menuName = c_MenuName)]
	public sealed class GroundInfo : ScriptableObject
	{
		private const string c_MenuName = "Game/GroundInfo";

		[SerializeField]
		private Set<AudioClip> m_FootstepSfx;

		public Set<AudioClip> FootstepSfx
		{
			get => m_FootstepSfx;
		}

	}
}