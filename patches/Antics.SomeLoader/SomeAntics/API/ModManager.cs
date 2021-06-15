using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SomeAntics.API
{
	public class ModManager
	{
		public static ReadOnlyCollection<Mod> ModsCollection => Mods.AsReadOnly();

		internal static List<Mod> Mods { get; } = new();
	}
}