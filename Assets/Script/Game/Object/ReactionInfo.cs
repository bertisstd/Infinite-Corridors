
namespace Bertis.Game
{
	public readonly ref struct ReactionInfo
	{
		public readonly UnitInfo source;
		public readonly UnitInfo target;

		public readonly float damage;
		public readonly bool critical;

		public ReactionInfo(UnitInfo source, UnitInfo target)
		{
			this.source = source;
			this.target = target;

			damage = source.Damage;

			if (critical = RNG.GenBool(source.CriticalChance))
				damage *= source.CriticalDamage;
		}

	}
}