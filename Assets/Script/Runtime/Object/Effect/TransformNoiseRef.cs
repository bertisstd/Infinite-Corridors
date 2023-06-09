
namespace Bertis.Runtime
{
	using UnityEngine;

	[System.Serializable]
	public class TransformNoiseRef : ObjectRef<TransformNoiseInfo>
	{
		[SerializeField]
		private EffectParams m_Params;

		public EffectParams Params
		{
			get => m_Params;
		}

		public bool GetInfo(out TransformNoiseInfo info)
		{
			return (info = Info).Valid && m_Params.Valid;
		}

		protected override ObjectLib<TransformNoiseInfo> Lib
		{
			get => TransformNoiseLib.Reference;
		}

	}
}