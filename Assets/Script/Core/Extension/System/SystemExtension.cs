
namespace Bertis
{
	using System;
	using System.Collections.Generic;

	static public class SystemExtension
	{
		static public IEnumerable<Type> IterateHierarchy(this Type type, bool includeThis = true)
		{
			if (type == null)
				throw new ArgumentNullException(nameof(type));

			var iterator = includeThis ? type : type.BaseType;
			while (iterator != null)
			{
				yield return iterator;
				iterator = iterator.BaseType;
			}
		}

	}
}