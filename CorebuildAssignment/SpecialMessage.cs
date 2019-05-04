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
    }
}
