
namespace Bertis.Runtime
{
	using UnityEngine;

	public class GComponentProvider<T> : ComponentProvider<T> where T : Component
	{
		protected override ComponentCache<T> Create(T scheme)
		{
			return new GComponentCache<T>(scheme);
		}

	}
}