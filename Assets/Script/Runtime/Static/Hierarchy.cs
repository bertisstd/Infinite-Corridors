
namespace Bertis.Runtime
{
	using System;
	using System.Runtime.InteropServices;
	using UnityEngine;

	static public class Hierarchy
	{
		static public GameObject CreateGameObject([Optional] string name, [Optional] bool permanent)
		{
			var ret = new GameObject(name);

			if (permanent)
				UnityEngine.Object.DontDestroyOnLoad(ret);

			return ret;
		}

		static public T CreateComponent<T>([Optional] string name, [Optional] bool permanent) where T : Component
		{
			return CreateGameObject(name ?? typeof(T).GetName(), permanent)
				.AddComponent<T>();
		}

		static public T CreateComponent<T>(T scheme, [Optional] bool permanent) where T : Component
		{
			if (scheme == null)
				throw new ArgumentNullException(nameof(scheme));

			var ret = UnityEngine.Object.Instantiate(scheme);

			if (permanent)
				UnityEngine.Object.DontDestroyOnLoad(ret.gameObject);

			ret.name = scheme.name;

			return ret;
		}

	}
}