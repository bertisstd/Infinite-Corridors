
namespace Bertis.Editor
{
	using System.Linq;
	using UnityEngine;
	using UnityEditor;

	[CustomPropertyDrawer(typeof(WeightedSet<>))]
	public sealed class WeightedSetPropertyDrawer : PropertyDrawer
	{
		protected override void Update(Rect rect, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginChangeCheck();
			base.Update(rect, property, label);
			if (EditorGUI.EndChangeCheck())
			{
				var collectionProp = property.GetSubPropertySafe("m_Collection");
				var totalWeightProp = property.GetSubPropertySafe("m_TotalWeight");

				var coll = collectionProp.Iterate()
					.ToArray();

				var totalWeight = coll.Sum(elemProp => elemProp.GetSubPropertySafe("m_Weight").floatValue);
				totalWeightProp.floatValue = totalWeight;

				foreach (var elem in coll)
				{
					var chanceProp = elem.GetSubPropertySafe("m_Chance");
					var weightProp = elem.GetSubPropertySafe("m_Weight");
					chanceProp.floatValue = weightProp.floatValue / totalWeight * 100f;
				}
			}
		}

	}
}