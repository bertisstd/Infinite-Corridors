
namespace Bertis.Editor
{
	using UnityEngine;
	using UnityEditor;

	public class PropertyDrawer : UnityEditor.PropertyDrawer
	{
		private bool m_Initialized;

		public sealed override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
		{
			InitialzieInternal(property, label);
			Update(rect, property, label);
		}

		public sealed override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			InitialzieInternal(property, label);
			return GetHeight(property, label);
		}

		protected virtual void Update(Rect rect, SerializedProperty property, GUIContent label)
		{
			EditorGUI.PropertyField(rect, property, label, includeChildren: true);
		}

		protected virtual float GetHeight(SerializedProperty property, GUIContent label)
		{
			return EditorGUI.GetPropertyHeight(property, label, includeChildren: true);
		}

		private void InitialzieInternal(SerializedProperty property, GUIContent label)
		{
			if (!m_Initialized)
			{
				Initialize(property, label);
				m_Initialized = true;
			}
		}

		protected virtual void Initialize(SerializedProperty property, GUIContent label) { }

	}
}