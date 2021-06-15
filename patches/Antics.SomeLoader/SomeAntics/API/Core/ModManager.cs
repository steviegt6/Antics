using System.Collections.Generic;
using System.Collections.ObjectModel;
using SomeAntics.API.Core.Interfaces;

namespace SomeAntics.API.Core
{
	public static class ModManager
	{
		public static ReadOnlyCollection<IMod> AvailableMods => Mods.AsReadOnly();

		internal static List<IMod> Mods { get; } = new();
	}
}