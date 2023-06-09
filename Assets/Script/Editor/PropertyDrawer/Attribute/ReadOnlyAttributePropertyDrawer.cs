
namespace Bertis.Editor
{
	using UnityEngine;
	using UnityEditor;

	[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
	public class ReadOnlyAttributePropertyDrawer : PropertyDrawer
	{
		protected override void Update(Rect position, SerializedProperty property, GUIContent label)
		{
			var enabled = GUI.enabled;
			GUI.enabled = false;
			base.Update(position, property, label);
			GUI.enabled = enabled;
		}

	}
}