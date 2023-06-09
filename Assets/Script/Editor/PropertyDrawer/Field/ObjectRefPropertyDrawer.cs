
namespace Bertis.Editor
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;
	using UnityEngine;
	using UnityEditor;

	[CustomPropertyDrawer(typeof(ObjectRef<>), useForChildren: true)]
	public class ObjectRefPropertyDrawer : PropertyDrawer
	{
		private ObjectInfo[] m_Infos;
		private GUIContent[] m_Options;

		protected override void Initialize(SerializedProperty property, GUIContent label)
		{
			var thisType = this.GetFieldType();

			var infoType = thisType.IterateHierarchy()
				.Reverse()
				.ToArray()[^2]
				.GenericTypeArguments[0];

			var libBaseType = typeof(ObjectLib<>).MakeGenericType(infoType);

			var libType = thisType.Assembly
				.GetTypes()
				.FirstOrDefault(thisType => thisType.BaseType == libBaseType);

			if (libType == null)
				throw new Exception("ObjectLib type not found");

			var attr = libType.GetCustomAttribute<CreateAssetMenuAttribute>();

			if (attr == null)
				throw new Exception("CreateAssetMenu attribute not found");

			var asset = ext.ScriptableObject.Load(
				attr.menuName,
				throwIfMissing: false);

			if (asset == null)
				throw new Exception("ObjectLib asset not found");

			m_Infos = ((IEnumerable<ObjectInfo>)asset).ToArray();

			m_Options = m_Infos.Select(info => new GUIContent(info.Name))
				.ToArray();
		}

		protected override void Update(Rect rect, SerializedProperty property, GUIContent label)
		{
			var idProp = property.GetSubPropertySafe("m_Id");

			rect.UnitizeHeight();
			idProp.isExpanded = EditorGUI.Foldout(
				rect,
				idProp.isExpanded,
				idProp.displayName,
				true);

			if (!idProp.isExpanded)
				return;

			rect.NewLine();

			EditorGUI.indentLevel++;

			var index = EditorGUI.Popup(
				rect,
				new GUIContent("Info"),
				GetIndex(idProp),
				m_Options);

			SetId(idProp, index);

			if (property.hasVisibleChildren)
			{
				rect.NewLine();

				EditorGUI.indentLevel++;

				rect.AdaptHeight(property);
				EditorGUI.PropertyField(
					rect,
					property,
					new GUIContent("Params"),
					includeChildren: true);

				EditorGUI.indentLevel--;
			}

			EditorGUI.indentLevel--;
		}

		private int GetIndex(SerializedProperty idProp)
		{
			var i = 0;
			foreach (var info in m_Infos)
			{
				if (idProp.intValue == info.Id)
					return i;

				i++;
			}

			if (m_Infos.Length == 0)
				throw new Exception("Empty ObjectLib");

			return 0;
		}

		private void SetId(SerializedProperty idProp, int index)
		{
			idProp.intValue = m_Infos[index].Id;
		}

		protected override float GetHeight(SerializedProperty property, GUIContent label)
		{
			const float unit = ext.EditorGUI.UnitHeight;

			var ret = unit;

			var idProp = property.GetSubPropertySafe("m_Id");
			if (idProp.isExpanded)
			{
				ret += unit;

				if (property.hasVisibleChildren)
					ret += EditorGUI.GetPropertyHeight(property, includeChildren: true);
			}

			return ret;
		}

	}
}