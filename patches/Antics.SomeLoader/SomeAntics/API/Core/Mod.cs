using SomeAntics.API.Core.Delegates;
using SomeAntics.API.Core.Interfaces;

namespace SomeAntics.API.Core
{
	public abstract class Mod : IMod
	{
		public abstract string Name { get; }

		public virtual WorldDelegates.OnWorldChanged OnWorldChanged { get; }

		public virtual WorldDelegates.OnWorldRequested OnWorldRequested { get; }

		public virtual void PreGameLaunch()
		{
		}
	}
}