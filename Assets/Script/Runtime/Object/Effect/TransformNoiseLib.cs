
namespace Bertis.Runtime
{
	using UnityEngine;

	[CreateAssetMenu(fileName = c_FileName, menuName = c_MenuName)]
	public sealed class TransformNoiseLib : ObjectLib<TransformNoiseInfo>
	{
		private const string c_FileName = nameof(TransformNoiseLib);
		private const string c_MenuName = "Runtime/" + c_FileName;

		static private readonly SJitter<TransformNoiseLib> s_Reference = new(c_MenuName);

		static public TransformNoiseLib Reference
		{
			get => s_Reference.Get();
		}

	}
}