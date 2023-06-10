
namespace Bertis.Game
{
	using UnityEngine;
	using Bertis.Runtime;

	[RequireComponent(typeof(Collider2D))]
	public sealed class Ground : MonoBehaviour
	{
		[SerializeField]
		private GroundInfo m_Info;

		public void Step()
		{
			var sfx = m_Info.FootstepSfx.Gen();
			AudioHandler.Play(sfx);
		}

	}
}