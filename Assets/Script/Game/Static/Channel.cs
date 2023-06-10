
namespace Bertis.Game
{
	using System.Collections.Generic;
	using UnityEngine;

	static public class Channel
	{
		static private readonly ContactFilter2D s_Filter;

		static Channel()
		{
			s_Filter = new ContactFilter2D
			{
				useTriggers  = true,
				useLayerMask = true,
				layerMask    = Layer.UnitFlag
			};
		}

		static public IEnumerable<UnitInfo> GetUnitsInArea(Vector3 position, float radius)
		{
			const int maxCount = 10;

			var buffer = new Collider2D[maxCount];
			var count = Physics2D.OverlapCircle(
				position,
				radius,
				s_Filter,
				buffer);

			for (var i = 0; i < count; i++)
			{
				if (buffer[i].TryGetComponent<UnitInfo>(out var ret))
				{
					yield return ret;
				}
			}
		}

	}
}