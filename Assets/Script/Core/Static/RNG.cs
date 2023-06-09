
namespace Bertis
{
	using UnityEngine;

	static public class RNG
	{
		static public float GenSingle(float min, float max)
		{
			return Random.Range(min, max);
		}

		static public float GenSingle(float max)
		{
			return GenSingle(0f, max);
		}

		static public float GenSingleSign(bool includeZero = false)
		{
			if (includeZero)
			{
				return GenInt32(-1, 2);
			}
			else
			{
				return GenBool() ? -1f : +1f;
			}
		}

		static public int GenInt32(int low, int high)
		{
			return Random.Range(low, high);
		}

		static public int GenInt32(int high)
		{
			return GenInt32(0, high);
		}

		static public bool GenBool()
		{
			return GenInt32(0, 2) == 0;
		}

		static public bool GenBool(float trueChance)
		{
			return GenSingle(ext.Math.Epsilon, 1.0f) <= trueChance;
		}

		static public Vector3 GenVector3Sign(bool includeZero = false)
		{
			return new Vector3(
				GenSingleSign(includeZero),
				GenSingleSign(includeZero),
				GenSingleSign(includeZero));
		}

	}
}