using System;
using System.IO;
using Antics.SomeLoader.CLI;
using Antics.SomeLoader.CLI.Personal;
using SomeAntics.API;
using SomeAntics.API.Interfaces;
using SomeAntics.API.Internal;
using SomeAntics.API.ModTypes;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Antics.SomeLoader
{
	/// <summary>
	///     Entry-point.
	/// </summary>
	public static class Launcher
	{
		/// <summary>
		///     Raw version.
		/// </summary>
		public const string VersionString = "0.1.0.0";

		/// <summary>
		///     Version parsed to a <see cref="Version"/> instance.
		/// </summary>
		public static readonly Version Version = Version.Parse(VersionString);

		/// <summary>
		///     Universal instance of <see cref="SomeAntics.SomeAntics"/>.
		/// </summary>
		public static SomeAntics.SomeAntics AnticsInstance { get; private set; } = null!;

		public static string ExecutableDirectory => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;

		public static string LibraryDirectory => ExecutableDirectory; // change eventually

		static Launcher() {
			AppDomain.CurrentDomain.AssemblyResolve += CurrentDomainOnAssemblyResolve;
		}

		/// <summary>
		/// </summary>
		public static void Main() {
			Console.Title = "Antics.SomeLoader - by convicted tomatophile";
			PersonalCLI.WriteStaticText();

			CommandLineUtilities.WriteLineWithColor("Asserting the presence of a modded copy of SomeAntics...",
				ConsoleColor.DarkGray);

			AssertModdedPresence();

			CommandLineUtilities.WriteLineWithColor("Loading mods...", ConsoleColor.DarkGray);

			InternalMod mod = new() {Version = typeof(Launcher).Assembly.GetName().Version};
			ModManager.Mods.Add(mod);

			Directory.CreateDirectory(Path.Combine(ExecutableDirectory, "Mods"));

			foreach (DirectoryInfo directory in new DirectoryInfo(Path.Combine(ExecutableDirectory, "Mods"))
				.GetDirectories()) {
				foreach (FileInfo file in directory.GetFiles().Where(x => x.Name.Equals($"{directory.Name}.dll"))) {
					Assembly assembly = Assembly.LoadFrom(file.ToString());

					List<Type> types = assembly.GetTypes().ToList();
					Type[] loadableTypes = types.Where(x =>
						x.IsAssignableFrom(typeof(ILoadable)) && !x.IsSubclassOf(typeof(ModLoadable)) &&
						!x.IsSubclassOf(typeof(Mod)) && !x.IsAbstract &&
						x.GetConstructor(Array.Empty<Type>()) != null).ToArray();
					Type[] modIdentityTypes = types.Where(x =>
						x.IsSubclassOf(typeof(ModLoadable)) && !x.IsAbstract &&
						x.GetConstructor(Array.Empty<Type>()) != null).ToArray();
					Type[] modTypes = types.Where(x =>
							x.IsSubclassOf(typeof(Mod)) && !x.IsAbstract &&
							x.GetConstructor(Array.Empty<Type>()) != null)
						.ToArray();

					if (modTypes.Length != 1) {
						CommandLineUtilities.WriteLineWithColor(
							$"Unexpected mod count in {file.Name}, skipping load process.", ConsoleColor.Red);
						goto Break;
					}

					Mod loadingMod = (Activator.CreateInstance(modTypes.First()) as Mod)!;
					loadingMod.Version = loadingMod.GetType().Assembly.GetName().Version;
					loadingMod.Load();

					foreach (Type loadableType in loadableTypes) {
						(Activator.CreateInstance(loadableType) as ILoadable)!.Load();
					}

					foreach (Type modIdentityType in modIdentityTypes) {
						object instance = Activator.CreateInstance(modIdentityType)!;
						(instance as ILoadable)!.Load();

						switch (instance) {
							case GlobalDrawnWord drawn:
								mod.globalDrawnWords.Add(drawn);
								break;

							case GlobalFunctionalWord functional:
								mod.globalFunctionalWords.Add(functional);
								break;
						}
					}

					ModManager.Mods.Add(loadingMod);
					CommandLineUtilities.WriteLineWithColor($"Loaded mod: {file.Name}", ConsoleColor.Red);
				}

				Break: ;
			}

			CommandLineUtilities.WriteLineWithColor("Launching SomeAntics...", ConsoleColor.DarkGray);

			RunGame();
		}

		/// <summary>
		/// </summary>
		public static void RunGame() {
			try {
				InternalRun(); // Separate method to prevent an immediate load attempt
			}
			catch (Exception e) {
				CommandLineUtilities.WriteLineWithColor("Caught error, closing safely:", ConsoleColor.Yellow);
				CommandLineUtilities.WriteLineWithColor(e.ToString(), ConsoleColor.Red);
				CommandLineUtilities.WriteLineWithColor("Press <enter> to exit.", ConsoleColor.DarkGray);
			} // Exit once the process has exited, maybe don't, we'll determine later
		}

		private static void InternalRun() {
			using SomeAntics.SomeAntics antics = new();
			AnticsInstance = antics;
			AnticsInstance.Run();
		}

#nullable enable
		private static Assembly? CurrentDomainOnAssemblyResolve(object? sender, ResolveEventArgs args) {
			try {
				AssemblyName name = new(args.Name);

				foreach (FileInfo file in new DirectoryInfo(LibraryDirectory).EnumerateFiles("*.dll"))
					if (name.Name != null && name.Name.Equals(AssemblyName.GetAssemblyName(file.FullName).Name,
						StringComparison.OrdinalIgnoreCase)) {
						CommandLineUtilities.WriteLineWithColor($"Loaded assembly: {name.Name}", ConsoleColor.DarkGray);
						return Assembly.LoadFrom(file.FullName);
					}

				CommandLineUtilities.WriteLineWithColor($"Failed to load assembly: {name.Name}", ConsoleColor.Yellow);
				return null;
			}
			catch (Exception e) {
				CommandLineUtilities.WriteLineWithColor(
					$"Error while attempting to resolve assembly \"{args.Name}\": {e}", ConsoleColor.DarkRed);
				return null;
			}
		}
#nullable restore

		private static void AssertModdedPresence() {
			try {
				// SomeAntics always present
				typeof(SomeAntics.SomeAntics).Assembly.GetType("SomeAntics.API.Mod", true);
			}
			catch (Exception e) {
				if (File.Exists(Path.Combine(ExecutableDirectory, "SomeAntics.dll"))) {
					CommandLineUtilities.WriteLineWithColor("Failed to load a modded version of SomeAntics.dll." +
					                                        "\nMake sure you're executing Antics.SomeLoader alongside your modded copy!",
						ConsoleColor.DarkRed);
					CommandLineUtilities.WriteLineWithColor($"Full error: {e}", ConsoleColor.Red);
					CommandLineUtilities.WriteLineWithColor("Press <enter> to exit!", ConsoleColor.DarkGray);
					Console.ReadLine();
					Environment.Exit(0);
				}

				CommandLineUtilities.WriteLineWithColor(
					"Failed to locate a SomeAntics assembly (SomeAntics.dll file)." +
					"\nMake sure you're executing Antics.SomeLoader alongside your modded copy!", ConsoleColor.DarkRed);
				CommandLineUtilities.WriteLineWithColor($"Full error: {e}", ConsoleColor.Red);
				CommandLineUtilities.WriteLineWithColor("Press <enter> to exit!", ConsoleColor.DarkGray);
				Console.ReadLine();
				Environment.Exit(0);
			}
		}
	}
}