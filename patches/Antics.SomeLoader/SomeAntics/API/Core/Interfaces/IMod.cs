using SomeAntics.API.Core.Delegates;

namespace SomeAntics.API.Core.Interfaces
{
	public interface IMod
	{
		WorldDelegates.OnWorldChanged OnWorldChanged { get; }

		WorldDelegates.OnWorldRequested OnWorldRequested { get; }

		string Name { get; }

		void PreGameLaunch();
	}
}