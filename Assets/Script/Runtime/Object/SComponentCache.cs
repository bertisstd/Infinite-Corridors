
namespace Bertis.Runtime
{
	using System;
	using UnityEngine;

	public class SComponentCache<T> : ComponentCache<T> where T : Component, IStatusProvider
	{
		static private readonly Func<T, bool> s_GetActive;

		static SComponentCache()
		{
			s_GetActive = comp => comp.Active;
		}
		
		public SComponentCache(T scheme)
		: base(s_GetActive, scheme) { }
		public SComponentCache(bool unique)
		: base(s_GetActive, unique) { }

	}
}