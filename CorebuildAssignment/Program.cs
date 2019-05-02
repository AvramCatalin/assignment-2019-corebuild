using System;

namespace CorebuildAssignment
{
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Arena arena = new Arena();
            arena.PlanetSelector();
            arena.VillainSelector();
            arena.HeroSelector();
        }
    }
}
