
namespace Bertis
{
	using System;
	using System.Collections.Generic;

	static public class SystemExtension
	{
		private const string c_NestedTypeSeparator = "+";

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

		static public string GetName(this Type type)
		{
			if (type == null)
				throw new ArgumentNullException(nameof(type));

			var declaringType = type.DeclaringType;
			return declaringType != null
				? $"{declaringType.Name}{c_NestedTypeSeparator}{type.Name}"
				: type.Name;
		}

	}
}