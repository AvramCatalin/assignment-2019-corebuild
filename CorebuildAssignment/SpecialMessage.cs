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
            //add sound
            Console.ReadLine();
            Console.Clear();
        }
        public static void WinMessage(string text)
        {
            ColorWriter.SpaceWrite("Green", text);
            //add sound
            Console.ReadLine();
            Console.Clear();
        }
        public static void LoseMessage(string text)
        {
            ColorWriter.SpaceWrite("Red", text);
            //add sound
            Console.ReadLine();
            Console.Clear();
        }
        public static void DefeatMessage(string text)
        {
            ColorWriter.SpaceWriteLine("Magenta", text + "\n");
            //add sound
        }
        public static void WaitingOnEnterMessage(string text)
        {
            ColorWriter.SpaceWrite("Yellow",text);
            Console.ReadLine();
        }
        public static void LoadingMessage(string text)
        {
            ColorWriter.SpaceWrite("Yellow",text);
            for (byte i = 0; i <= 3; i++)
            {
                System.Threading.Thread.Sleep(750);
                ColorWriter.SpaceWrite("White", ".");
            }
            System.Threading.Thread.Sleep(750);
            ColorWriter.SpaceWriteLine("White", ".");
            System.Threading.Thread.Sleep(750);
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
            ColorWriter.WriteLine("White", i.ToString());
            Console.Clear();
        }
    }
}
