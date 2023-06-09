using __buff = UnityEngine.Time;

namespace Bertis.ext
{
	static public class Time
	{
		static public float GetDeltaTime(bool scaled)
		{
			return scaled ? __buff.deltaTime : __buff.unscaledDeltaTime;
		}

	}
}