
namespace Bertis
{
	using UnityEngine;

	[System.Serializable]
	public struct Range
	{
		public float start;
		public float end;

		public Range(float start, float end)
		{
			this.start = start;
			this.end = end;
		}
		public Range(float end)
		: this(0f, end) { }

		public float Delta
		{
			get => end - start;
		}
		public float Length
		{
			get => Mathf.Abs(Delta);
		}
		public float Random
		{
			get => RNG.GenSingle(start, end);
		}

		public override string ToString()
		{
			return $"[{start}..{end}]";
		}

	}
}