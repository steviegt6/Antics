using System;

namespace Antics.SomeLoader.CLI
{
    /// <summary>
    ///     Supplies useful command line-related utilities for a cleaner interface.
    /// </summary>
    public static class CommandLineUtilities
    {
        /// <summary>
        /// </summary>
        /// <param name="o"></param>
        /// <param name="color"></param>
        public static void WriteLineWithColor(object o, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(o);
            Console.ResetColor();
        }

        /// <summary>
        /// </summary>
        /// <param name="o"></param>
        /// <param name="color"></param>
        public static void WriteWithColor(object o, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(o);
            Console.ResetColor();
        }
    }
}