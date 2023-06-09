
namespace Bertis.ext
{
	using UnityEngine;

	static public class Math
	{
		public const float Epsilon = 1e-6f;

		static public float ToDegree(Vector2 vector)
		{
			return Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
		}

		static public Vector2 ToVector(float degree)
		{
			var rad = degree * Mathf.Deg2Rad;
			return new Vector2(
				Mathf.Cos(rad),
				Mathf.Sin(rad));
		}

	}
}