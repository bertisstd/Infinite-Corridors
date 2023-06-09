
namespace Bertis.Editor
{
	using UnityEngine;
	using UnityEditor;

	[CustomPropertyDrawer(typeof(Library<>.Element))]
	public sealed class LibraryElementPropertyDrawer : PropertyDrawer
	{
		protected override void Update(Rect rect, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginChangeCheck();
			base.Update(rect, property, label);
			if (EditorGUI.EndChangeCheck())
			{
				var nameProp = property.GetSubPropertySafe("m_Name");
				var idProp   = property.GetSubPropertySafe("m_Id");

				idProp.intValue = Animator.StringToHash(nameProp.stringValue);
			}
		}

	}
}