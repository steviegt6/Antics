using SomeAntics.API.Interfaces;

namespace SomeAntics.API.ModTypes
{
	public abstract class ModLoadable : IModLoadable
	{
		public Mod Mod { get; internal set; }

		public virtual void Load() {
		}
	}
}