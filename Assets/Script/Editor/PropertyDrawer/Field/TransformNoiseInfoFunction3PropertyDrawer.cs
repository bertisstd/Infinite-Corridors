
namespace Bertis.Editor
{
	using UnityEngine;
	using UnityEditor;
	using Bertis.Runtime;

	[CustomPropertyDrawer(typeof(TransformNoiseInfo.Function3))]
	public sealed class TransformNoiseInfoFunction3PropertyDrawer : PropertyDrawer
	{
		protected override void Update(Rect rect, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginChangeCheck();
			base.Update(rect, property, label);
			if (EditorGUI.EndChangeCheck())
			{
				var amplitudeProp = property.GetSubPropertySafe("m_Amplitude");
				var xProp         = property.GetSubPropertySafe("m_X");
				var yProp         = property.GetSubPropertySafe("m_Y");
				var zProp         = property.GetSubPropertySafe("m_Z");
				var validProp     = property.GetSubPropertySafe("m_Valid");

				validProp.boolValue = (amplitudeProp.floatValue > 0f)
					&& (IsValid(xProp) || IsValid(yProp) || IsValid(zProp));

				static bool IsValid(SerializedProperty property)
				{
					return property.GetSubPropertySafe("m_Valid").boolValue;
				}

			}
		}

	}
}