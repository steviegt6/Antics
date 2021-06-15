using System;
using System.IO;
using Antics.SomeLoader.CLI;
using Antics.SomeLoader.CLI.Personal;
using SomeAntics.API;
using System.Collections.Generic;
using System.Diagnostics;
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
		public static SomeAntics.SomeAntics AnticsInstance { get; private set; }

		public static string ExecutableDirectory => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

		/// <summary>
		/// </summary>
		public static void Main() {
			Console.Title = "Antics.SomeLoader - by convicted tomatophile";

			PersonalCLI.WriteStaticText();
			CommandLineUtilities.WriteLineWithColor("Launching SomeAntics...", ConsoleColor.DarkGray);

			/*if (!File.Exists("SomeAntics.exe") && OperatingSystem.IsWindows())
			{
				CommandLineUtilities.WriteLineWithColor(
					"SomeAntics.exe could not be found. You are using a Windows OS, please supply the loader with a working executable!" +
					"\nPress <enter> to exit.", ConsoleColor.Red);
				Console.ReadLine();
				return;
			}*/

			if (!File.Exists("SomeAntics.dll")) {
				CommandLineUtilities.WriteLineWithColor(
					"SomeAntics.dll could not be found, please supply the loader with a working DLL." +
					"\nPress <enter> to exit.", ConsoleColor.Red);
				Console.ReadLine();
				return;
			}

			/*string processName;
			string arguments = "";

			if (OperatingSystem.IsWindows())
				processName = "SomeAntics.exe";
			else
			{
				processName = "bash"; // If bash doesn't work then I don't know what you tell 'ya :^)
				arguments = "dotnet SomeAntics.dll"; // *should* launch the DLL
			}

			ProcessStartInfo startInfo = new(processName)
			{
				RedirectStandardError = true,
				RedirectStandardInput = true,
				RedirectStandardOutput = true,
				WindowStyle = ProcessWindowStyle.Maximized,
				CreateNoWindow = false,
				Arguments = arguments
			};

			Process process = Process.Start(startInfo);

			if (process is null)
			{
				CommandLineUtilities.WriteLineWithColor("Process failed to start. Press <enter> to exit.",
					ConsoleColor.Red);
				Console.ReadLine();
				return;
			}

			while (!process.HasExited)
			{

			}*/

			Directory.CreateDirectory(Path.Combine(ExecutableDirectory, "Mods"));
			CommandLineUtilities.WriteLineWithColor("Loading mods in /Mods/...", ConsoleColor.DarkGray);
			List<Assembly> assemblies =
				(from modDirectory in Directory.GetDirectories(Path.Combine(ExecutableDirectory, "Mods"), "*",
						SearchOption.AllDirectories)
					select new DirectoryInfo(modDirectory)
					into dirInfo
					from file in dirInfo.GetFiles()
					where file.Name.Equals($"{dirInfo.Name}.dll")
					select Assembly.LoadFile(file.ToString())).ToList();

			foreach (Assembly assembly in assemblies) {
				AppDomain.CurrentDomain.Load(assembly.GetName());

				foreach (Type type in assembly.GetTypes())
					if (type == typeof(Mod) && !type.IsAbstract && type.GetConstructor(Array.Empty<Type>()) != null)
						ModManager.ModAssemblies.Add(assembly); // add if this is a mod assembly
			}

			foreach (Assembly modAssembly in ModManager.ModAssemblies) {
				Mod mod = (Mod) Activator.CreateInstance(modAssembly.GetTypes().First(x => x == typeof(Mod) && !x.IsAbstract && x.GetConstructor(Array.Empty<Type>()) != null));
				Debug.Assert(mod != null, nameof(mod) + " != null");
				mod.Code = modAssembly;
			}

			try {
				/*Type programType = typeof(SomeAntics.Program);
				MethodInfo mainMethod = programType.GetMethod("Main", BindingFlags.Static | BindingFlags.NonPublic);

				if (mainMethod is null)
				{
					CommandLineUtilities.WriteLineWithColor("Main method was null! Press <enter> to exit.", ConsoleColor.Red);
					Console.ReadLine();
					return;
				}

				mainMethod.Invoke(null, null);*/

				using SomeAntics.SomeAntics antics = new();
				AnticsInstance = antics;
				AnticsInstance.Run();
			}
			catch (Exception e) {
				CommandLineUtilities.WriteLineWithColor("Caught error, closing safely:", ConsoleColor.Yellow);
				CommandLineUtilities.WriteLineWithColor(e.ToString(), ConsoleColor.Red);
				CommandLineUtilities.WriteLineWithColor("Press <enter> to exit.", ConsoleColor.DarkGray);
			}

			// Exit once the process has exited, maybe don't, we'll determine later
		}
	}
}