using System;
using UnityEngine;

namespace Bertis.Runtime
{
	static public class FunctionUtility
	{
		static private Messenger s_Messenger;
		
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		static private void Initialize()
		{
			s_Messenger = Hierarchy.CreateComponent<Messenger>(nameof(FunctionUtility), permanent: true);
		}

		static public Id InvokeDelayed(
			Action       action,
			float        delay,
			bool         scaled = true,
			Action<bool> onEnd  = null)
		{
			if (action == null)
				throw new ArgumentNullException(nameof(action));

			if (delay <= 0f)
			{
				action.Invoke();
				return default;
			}

			var fn = Cache.Provide();
			fn.action = action;
			fn.time = new(delay);
			fn.timer = delay;
			fn.scaled = scaled;
			fn.onEnd = onEnd;

			fn.Restart(Function.Invoking.Delayed);

			return fn.id;
		}

		static public bool IsActive(this Id id)
		{
			return id.GetReference(out _);
		}

		static public bool Abort(this Id id)
		{
			if (id.GetReference(out var reference))
			{
				Cache.SuspendAt(reference.offset, abort: true);
				return true;
			}
			else return false;
		}

		private class Messenger : MonoBehaviour
		{
			private void Awake()
			{
				enabled = false;
			}

			private void LateUpdate()
			{
				for (var i = Cache.count; --i >= 0;)
				{
					if (Cache.collection[i].update())
					{
						Cache.SuspendAt(i, abort: false);
					}
				}
			}

		}

		public struct Id
		{
			internal Function reference;
			internal int version;

			internal Id(Function reference)
			{
				this.reference = reference;
				version = 1;
			}

			internal bool GetReference(out Function reference)
			{
				reference = this.reference;

				return reference != null
					&& reference.offset < Cache.count
					&& version == reference.id.version;
			}

		}

		internal class Function
		{
			public Id id;
			public int offset;

			public Func<bool> update;
			public Action<bool> onEnd;

			public Action action;
			public Range time;
			public float timer;
			public bool scaled;

			private Func<bool> m__InvokeDelayed;

			public Function()
			{
				id = new(this);
			}

			private Func<bool> InvokeDelayed
			{
				get
				{
					return m__InvokeDelayed ??= JIT;

					bool JIT()
					{
						timer -= ext.Time.GetDeltaTime(scaled);

						if (timer <= 0f)
						{
							action.Invoke();
							return true;
						}
						else return false;
					}

				}
			}

			public void Restart(Invoking invoking)
			{
				update = invoking switch
				{
					Invoking.Delayed => InvokeDelayed,
					_ => throw new EnumIndexException(invoking)
				};

				id.version++;
			}

			public enum Invoking
			{
				Delayed
			}

		}

		static private class Cache
		{
			static public Function[] collection;
			static public int count;

			static Cache()
			{
				var initSize = 4;
				collection = new Function[initSize];
				InitializeCollection(0);
			}

			static private void InitializeCollection(int lowIndex)
			{
				for (var i = lowIndex; i < collection.Length; i++)
				{
					collection[i] = new Function { offset = i };
				}
			}

			static public Function Provide()
			{
				EnsureEnoughSpace();

				var ret = collection[count++];

				if (count == 1)
					s_Messenger.enabled = true;

				return ret;
			}

			static private void EnsureEnoughSpace()
			{
				if (count < collection.Length)
					return;

				var newSize = count + 4;
				var newColl = new Function[newSize];
				Array.Copy(collection, newColl, count);
				collection = newColl;
				InitializeCollection(count);
			}

			static public void SuspendAt(int index, bool abort)
			{
				var suspending = collection[index];

				if (--count > 0)
				{
					var swapping = collection[count];

					collection[index] = swapping;
					collection[count] = suspending;

					suspending.offset = count;
					swapping.offset   = index;
				}

				suspending.onEnd?.Invoke(abort);

				if (count == 0)
					s_Messenger.enabled = false;
			}

		}

	}
}