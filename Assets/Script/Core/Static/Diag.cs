using System.Diagnostics;

namespace Bertis
{
	using System.Collections;
	using UnityEngine;

	static public class Diag
	{
		public const string UnityEditor = "UnityEditor";

		[Conditional(UnityEditor)]
		static public void Log(string message)
		{
			Debug.Log(message);
		}

		[Conditional(UnityEditor)]
		static public void LogColl(IEnumerable coll)
		{
			if (coll != null)
			{
				foreach (var elem in coll)
				{
					Debug.Log(elem);
				}
			}
		}

		[Conditional(UnityEditor)]
		static public void LogWarning(string message)
		{
			Debug.LogWarning(message);
		}

		[Conditional(UnityEditor)]
		static public void LogError(string message)
		{
			Debug.LogError(message);
		}

	}
}