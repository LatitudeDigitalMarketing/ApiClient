using System;
using System.Diagnostics;

namespace WebApiTestClient.Utils
{
    public static class ConsoleExtensions
    {
        [DebuggerStepThrough]
        public static void ConsoleGreen(this string text)
        {
            text.ColoredWriteLine(ConsoleColor.Green);
        }

        [DebuggerStepThrough]
        public static void ConsoleRed(this string text)
        {
            text.ColoredWriteLine(ConsoleColor.Red);
        }

        [DebuggerStepThrough]
        public static void ConsoleYellow(this string text)
        {
            text.ColoredWriteLine(ConsoleColor.Yellow);
        }

        [DebuggerStepThrough]
        public static void ConsoleWhite(this string text)
        {
            text.ColoredWriteLine(ConsoleColor.White);
        }

        [DebuggerStepThrough]
        public static void ConsoleBlue(this string text)
        {
            text.ColoredWriteLine(ConsoleColor.Blue);
        }

        [DebuggerStepThrough]
        public static void ColoredWriteLine(this string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}
