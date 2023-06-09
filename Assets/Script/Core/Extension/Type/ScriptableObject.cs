using __buff = UnityEngine.ScriptableObject;

namespace Bertis.ext
{
	using System;
	using UnityEngine;

	static public class ScriptableObject
	{
		public const string c_Directory = "ScriptableObject/";

		static public UnityEngine.Object Load(string menuName, bool throwIfMissing = true)
		{
			var path = c_Directory + menuName;
			var ret = Resources.Load(path);

			if (ret == null && throwIfMissing)
			{
				throw new Exception($"Missing ScriptableObject. Path: {path}");
			}

			return ret;
		}

		static public T Load<T>(string menuName, bool throwIfMissing = true) where T : __buff
		{
			return (T)Load(menuName, throwIfMissing);
		}

	}
}