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
        public static void LoadingMessage(string text)
        {
            ColorWriter.Write("Yellow", "\n " + text + " ");
            for (byte i = 0; i <= 4; i++)
            {
                System.Threading.Thread.Sleep(750);
                ColorWriter.Write("Yellow", ". ");
            }
            Console.Clear();
        }
        public static void CountdownMessage(string text, byte i)
        {
            ColorWriter.Write("Yellow", "\n " + text + " ");
            while (i > 0)
            {
                ColorWriter.Write("Yellow", i + " ");
                System.Threading.Thread.Sleep(750);
                i--;
            }
        }
    }
}
