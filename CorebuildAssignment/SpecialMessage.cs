using System;
using System.Media;

namespace CorebuildAssignment
{
    class SpecialMessage
    {
        public static void ErrorMessage(string text)
        {
            ColorWriter.Write("Red", "\n " + text);
            SystemSounds.Exclamation.Play();
            Console.ReadLine();
            Console.Clear();
        }
        public static void SuccessMessage(string text)
        {
            ColorWriter.Write("Green", "\n " + text);
            //SystemSounds.Exclamation.Play();
            Console.ReadLine();
            Console.Clear();
        }
        public static void WinMessage(string text)
        {
            ColorWriter.Write("Green", "\n " + text);
            //add sound
            Console.ReadLine();
            Console.Clear();
        }
        public static void LoseMessage(string text)
        {
            ColorWriter.Write("Red", "\n " + text);
            //add sound
            Console.ReadLine();
            Console.Clear();
        }
        public static void DefeatMessage(string text)
        {
            ColorWriter.SpaceWriteLine("Magenta", text);
            //add sound
        }
        public static void LoadingMessage(string text)
        {
            ColorWriter.Write("Yellow", "\n " + text + " ");
            for (byte i = 0; i <= 4; i++)
            {
                System.Threading.Thread.Sleep(750);
                ColorWriter.Write("White", ". ");
            }
            Console.Clear();
        }
        public static void CountdownMessage(string text, byte i)
        {
            ColorWriter.Write("Yellow", "\n " + text + " ");
            while (i > 0)
            {
                ColorWriter.Write("White", i + " ");
                System.Threading.Thread.Sleep(750);
                i--;
            }
        }
    }
}
