
namespace Bertis.Editor
{
	using UnityEngine;
	using UnityEditor;
	using Bertis.Runtime;

	[CustomPropertyDrawer(typeof(TransformNoiseInfo.Function))]
	public sealed class TransformNoiseInfoFunctionPropertyDrawer : PropertyDrawer
	{
		protected override void Update(Rect rect, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginChangeCheck();
			base.Update(rect, property, label);
			if (EditorGUI.EndChangeCheck())
			{
				var curveProp = property.GetSubPropertySafe("m_Curve");
				var validProp = property.GetSubPropertySafe("m_Valid");

				validProp.boolValue = curveProp.animationCurveValue.length > 0;
			}
		}

	}
}