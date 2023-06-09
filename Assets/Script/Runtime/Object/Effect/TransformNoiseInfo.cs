
namespace Bertis.Runtime
{
	using UnityEngine;

	[System.Serializable]
	public class TransformNoiseInfo : ObjectInfo
	{
		[SerializeField]
		private Function3 m_Position;
		[SerializeField]
		private Function3 m_Rotation;

		public bool Valid
		{
			get => m_Position.Valid || m_Rotation.Valid;
		}
		public Function3 Position
		{
			get => m_Position;
		}
		public Function3 Rotation
		{
			get => m_Rotation;
		}

		[System.Serializable]
		public sealed class Function
		{
			[SerializeField]
			private AnimationCurve m_Curve;
			[SerializeField, ReadOnly]
			private bool m_Valid;

			public float this[float x, float mul]
			{
				get => m_Curve.Evaluate(x) * mul;
			}

			public bool Valid
			{
				get => m_Valid;
			}

		}

		[System.Serializable]
		public sealed class Function3
		{
			[SerializeField]
			private float m_Amplitude;
			[SerializeField]
			private Function m_X;
			[SerializeField]
			private Function m_Y;
			[SerializeField]
			private Function m_Z;
			[SerializeField, ReadOnly]
			private bool m_Valid;

			public Vector3 this[float x, Vector3 mul]
			{
				get
				{
					var ret = Vector3.zero;

					if (m_X.Valid) { ret.x = m_X[x, mul.x * m_Amplitude]; }
					if (m_Y.Valid) { ret.y = m_Y[x, mul.y * m_Amplitude]; }
					if (m_Z.Valid) { ret.z = m_Z[x, mul.z * m_Amplitude]; }

					return ret;
				}
			}

			public bool Valid
			{
				get => m_Valid;
			}

		}

	}
}