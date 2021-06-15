using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;

namespace SomeAntics.API
{
	public static class ModManager
	{
		public static ReadOnlyCollection<Mod> ModsCollection => Mods.AsReadOnly();

		internal static List<Mod> Mods { get; } = new();

		internal static List<Assembly> ModAssemblies { get; } = new();
	}
}