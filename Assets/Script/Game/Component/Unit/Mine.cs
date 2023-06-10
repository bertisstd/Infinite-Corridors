
namespace Bertis.Game
{
	using UnityEngine;

	public class Mine : UnitInfo
	{
		[SerializeField]
		private Explosion m_Explosion;

		public override void Die()
		{
			base.Die();
			m_Explosion.Explode(this, transform.position);
		}

	}
}