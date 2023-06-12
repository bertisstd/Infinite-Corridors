
namespace Bertis
{
	using UnityEngine;

	public class ConfigProviderBase : ScriptableObject
	{
		[SerializeField]
		protected Library<GameObject> m_SchemeLibrary;
		[SerializeField]
		protected Library<float> m_SingleLibrary;
		[SerializeField]
		protected Library<int> m_Int32Library;
		[SerializeField]
		protected Library<bool> m_BooleanLibrary;
		[SerializeField]
		protected Library<Range> m_RangeLibrary;
		[SerializeField]
		protected Library<AnimationCurve> m_CurveLibrary;

	}
}