
namespace Bertis.Runtime
{
	using System;
	using UnityEngine;

	public class GComponentCache<T> : ComponentCache<T> where T : Component
	{
		static private readonly Func<T, bool> s_GetActive;

		static GComponentCache()
		{
			s_GetActive = comp => comp.gameObject.activeSelf;
		}

		public GComponentCache(T scheme)
		: base(s_GetActive, scheme) { }
		public GComponentCache(bool unique)
		: base(s_GetActive, unique) { }

		public override void Initialize()
		{
			base.Initialize();

			for (var i = Count; --i >= 0;)
			{
				this[i].gameObject.SetActive(false);
			}
		}

	}
}