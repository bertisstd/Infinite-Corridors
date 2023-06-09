
namespace Bertis.Editor
{
	using System.Linq;
	using UnityEditor;

	[CustomEditor(typeof(ObjectLib<>), editorForChildClasses: true)]
	public class ObjectLibEditor : Editor
	{
		private SerializedProperty m_CollectionProp;
		private SerializedProperty m_NextIdProp;

		private void OnEnable()
		{
			m_CollectionProp = serializedObject.GetProperty("m_Collection");
			m_NextIdProp = serializedObject.GetProperty("m_NextId");
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			var prevSize = m_CollectionProp.arraySize;

			base.OnInspectorGUI();

			if (prevSize != m_CollectionProp.arraySize)
			{
				Update();
			}

			serializedObject.ApplyModifiedProperties();
		}

		private void Update()
		{
			var idProps = m_CollectionProp.Iterate()
				.Select(prop => prop.GetSubPropertySafe("m_Id"))
				.Reverse();

			foreach (var idProp in idProps)
			{
				if (!IsUnique(idProp.intValue))
				{
					idProp.intValue = m_NextIdProp.intValue++;
				}
			}

			bool IsUnique(int id)
			{
				var flag = false;
				foreach (var idProp in idProps)
				{
					if (id == idProp.intValue)
					{
						if (!flag)
						{
							flag = true;
						}
						else return false;
					}
				}

				return true;
			}

		}

	}
}