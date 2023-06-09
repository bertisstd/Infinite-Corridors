
namespace Bertis.Editor
{
	using UnityEngine;
	using UnityEditor;
	using Bertis.Runtime;

	[CustomPropertyDrawer(typeof(SpriteAnimInfo))]
	public sealed class SpriteAnimInfoPropertyDrawer : PropertyDrawer
	{
		protected override void Update(Rect rect, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginChangeCheck();
			base.Update(rect, property, label);
			if (EditorGUI.EndChangeCheck())
			{
				var spritesProp  = property.GetSubPropertySafe("m_Sprites");
				var durationProp = property.GetSubPropertySafe("m_Duration");
				var validProp    = property.GetSubPropertySafe("m_Valid");

				validProp.boolValue = spritesProp.arraySize > 0 && durationProp.floatValue > 0f;
			}
		}

	}
}