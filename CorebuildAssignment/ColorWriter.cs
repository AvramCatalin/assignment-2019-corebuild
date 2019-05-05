using System;

namespace CorebuildAssignment
{
    class ColorWriter
    {
        private static void ColorSetter(string background, string foreground)
        {
            ConsoleColor consoleColor;
            consoleColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), background, true);
            Console.BackgroundColor = consoleColor;
            consoleColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), foreground, true);
            Console.ForegroundColor = consoleColor;
        }
        public static void Write(string background, string foreground, string text)
        {
            ColorSetter(background, foreground);
            Console.Write(text);
            Console.ResetColor();
        }
        public static void Write(string foreground, string text)
        {
            Write("Black", foreground, text);
        }
        public static void WriteLine(string background, string foreground, string text)
        {
            ColorSetter(background, foreground);
            Console.WriteLine(text);
            Console.ResetColor();
        }
        public static void WriteLine(string foreground, string text)
        {
            WriteLine("Black", foreground, text);
        }
        public static void SpaceWrite(string background, string foreground, string text)
        {
            Console.Write(" ");
            Write(background, foreground, text);
        }
        public static void SpaceWrite(string foreground, string text)
        {
            SpaceWrite("Black", foreground, text);
        }
        public static void SpaceWriteLine(string background, string foreground, string text)
        {
            Console.Write(" ");
            WriteLine(background, foreground, text);
        }
        public static void SpaceWriteLine(string foreground, string text)
        {
            SpaceWriteLine("Black", foreground, text);
        }
    }
}
