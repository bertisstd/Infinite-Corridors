
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

	}
}