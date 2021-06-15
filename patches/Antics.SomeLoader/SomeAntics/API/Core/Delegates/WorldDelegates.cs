using SomeAntics.Map;

namespace SomeAntics.API.Core.Delegates
{
	public class WorldDelegates
	{
		public delegate void OnWorldChanged(World world);

		public delegate void OnWorldRequested(ref World world);
	}
}