
namespace Bertis.Editor
{
	using UnityEngine;
	using UnityEditor;

	[CustomPropertyDrawer(typeof(Set<>))]
	public sealed class SetPropertyDrawer : PropertyDrawer
	{
		private const float c_NonRepRandomSkipPercentage = 0.5f;

		protected override void Update(Rect rect, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginChangeCheck();
			base.Update(rect, property, label);
			if (EditorGUI.EndChangeCheck())
			{
				var collectionProp = property.GetSubPropertySafe("m_Collection");
				var lastIndexProp  = property.GetSubPropertySafe("m_LastIndex");

				lastIndexProp.intValue = (int)(collectionProp.arraySize * c_NonRepRandomSkipPercentage);
			}
		}

	}
}