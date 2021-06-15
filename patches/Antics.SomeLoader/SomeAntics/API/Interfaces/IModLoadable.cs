namespace SomeAntics.API.Interfaces
{
	public interface IModLoadable : ILoadable
	{
		Mod Mod { get; }
	}
}