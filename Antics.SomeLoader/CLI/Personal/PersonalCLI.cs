#nullable enable
using System;

namespace Antics.SomeLoader.CLI.Personal
{
    /// <summary>
    ///     Antics-specific command line utilities.
    /// </summary>
    public static class PersonalCLI
    {
        /// <summary>
        /// </summary>
        public static readonly string TitleText = @$"
 Antics.SomeLoader v{Launcher.VersionString}
 SomeAntics by Laugic and Blockaroz
 Antics.SomeLoader by convicted tomatophile";

        /// <summary>
        /// </summary>
        /// <param name="message"></param>
        /// <param name="color"></param>
        public static void WriteStaticText(string? message = null, ConsoleColor? color = null)
        {
            CommandLineUtilities.WriteLineWithColor(TitleText, ConsoleColor.DarkGray);

            if (message is not null)
            {
                CommandLineUtilities.WriteLineWithColor(message, color ?? Console.ForegroundColor);
            }
            else
            {
                Console.WriteLine();
            }

            Console.WriteLine();
        }
    }
}