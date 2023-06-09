
namespace Bertis.Editor
{
	using System;
	using UnityEngine;
	using UnityEditor;
	using System.Collections.Generic;

	static public class UnityExtension
	{
		static public void NewLine(this ref Rect rect)
		{
			rect.y += ext.EditorGUI.UnitHeight;
		}

		static public void NewLine(this ref Rect rect, SerializedProperty property)
		{
			if (property == null)
			{
				rect.NewLine();
			}
			else
			{
				var value = EditorGUI.GetPropertyHeight(property, includeChildren: false);
				rect.y += value;
			}
		}

		static public void UnitizeHeight(this ref Rect rect)
		{
			rect.height = ext.EditorGUI.UnitHeight;
		}

		static public void AdaptHeight(this ref Rect rect, SerializedProperty property)
		{
			if (property == null)
			{
				rect.UnitizeHeight();
			}
			else
			{
				var value = EditorGUI.GetPropertyHeight(property, includeChildren: false);
				rect.height = value;
			}
		}

		static public Type GetFieldType(this PropertyDrawer propertyDrawer)
		{
			if (propertyDrawer == null)
				throw new ArgumentNullException(nameof(propertyDrawer));

			var raw = propertyDrawer.fieldInfo.FieldType;
			return raw.IsArray ? raw.GetElementType() : raw;
		}

		static public SerializedProperty GetSubPropertySafe(this SerializedProperty property, string name)
		{
			if (property == null)
				throw new ArgumentNullException(nameof(property));

			var ret = property.FindPropertyRelative(name);

			if (ret == null)
				throw new ArgumentException($"Property not found. Name: {name}");

			return ret;
		}

		static public SerializedProperty GetProperty(this SerializedObject serializedObject, string name)
		{
			if (serializedObject == null)
				throw new ArgumentNullException(nameof(serializedObject));

			var ret = serializedObject.FindProperty(name);

			if (ret == null)
				throw new ArgumentException($"Property not found. Name: {name}");

			return ret;
		}

		static public IEnumerable<SerializedProperty> Iterate(this SerializedProperty property)
		{
			if (property == null)
				throw new ArgumentNullException(nameof(property));

			if (!property.isArray)
				throw new ArgumentException("Property is not an array");

			for (var i = 0; i < property.arraySize; i++)
			{
				yield return property.GetArrayElementAtIndex(i);
			}
		}

	}
}