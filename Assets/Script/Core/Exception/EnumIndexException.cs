
namespace Bertis
{
	using System;

	public class EnumIndexException : Exception
	{
		private const string c_MessageUndefined = "Enum index is undefined. Index: {0}, Type: {1}}";
		private const string c_MessageSystemOutdated = "Enum index is defined but system is outdated. Index: {0}, Type: {1}}";

		public EnumIndexException(Enum index)
		: base(GetMessage(index)) { }

		static private string GetMessage(Enum index)
		{
			if (index == null)
				return null;

			var type = index.GetType();
			var message = Enum.IsDefined(type, index)
				? c_MessageSystemOutdated
				: c_MessageUndefined;

			return String.Format(message, index, type);
		}

	}
}