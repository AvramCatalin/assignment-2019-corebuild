using System;

namespace CorebuildAssignment
{
    class Program
    {
        private static Arena arena = new Arena();
        private static bool errorGiven = false;
        private static void StartUpLogo()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(@"  __  __                      _                             _                                                       ");
            Console.WriteLine(@" |  \/  |                    | |                           | |                                                      ");
            Console.WriteLine(@" | \  / | __ _ _ ____   _____| |  ___ _   _ _ __   ___ _ __| |__   ___ _ __ ___   ___  ___  __      ____ _ _ __ ___ ");
            Console.WriteLine(@" | |\/| |/ _` | '__\ \ / / _ \ | / __| | | | '_ \ / _ \ '__| '_ \ / _ \ '__/ _ \ / _ \/ __| \ \ /\ / / _` | '__/ __|");
            Console.WriteLine(@" | |  | | (_| | |   \ V /  __/ | \__ \ |_| | |_) |  __/ |  | | | |  __/ | | (_) |  __/\__ \  \ V  V / (_| | |  \__ \");
            Console.WriteLine(@" |_|  |_|\__,_|_|    \_/ \___|_| |___/\__,_| .__/ \___|_|  |_| |_|\___|_|  \___/ \___||___/   \_/\_/ \__,_|_|  |___/");
            Console.WriteLine(@"                                           | | ");
            Console.WriteLine(@"                                           |_| ");
        }

        private static byte MainSelectionMenu()
        {
            string temporaryName = null;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(" Main menu options: \n");
            Console.Write(" ");
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("1");
            Console.ResetColor();
            for (byte i=1;i<=5;i++)
            {
                if(i>=1 && i<=3)
                {
                    Console.Write(" Select a");
                }
                if(i==4)
                {
                    Console.Write(" Build an");
                }
                if (i==5)
                {
                    Console.Write(" Start the");
                }
                Console.ForegroundColor = ConsoleColor.Cyan;
                switch (i)
                {
                    case 1:
                        Console.WriteLine(" Planet");
                        temporaryName = arena.PlanetChecker();
                        if (temporaryName != null)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("     \u00BB "+ temporaryName + " selected!");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                        }
                        else
                        {
                            Console.WriteLine();
                        }
                        goto default;
                    case 2:
                        temporaryName = arena.VillainChecker();
                        Console.WriteLine(" Villain");
                        if (temporaryName != null)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("     \u00BB "+ temporaryName + " selected!");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                        }
                        else
                        {
                            Console.WriteLine();
                        }
                        goto default;
                    case 3:
                        temporaryName = arena.HeroChecker();
                        Console.WriteLine(" Hero");
                        if (temporaryName != null)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("     \u00BB " + temporaryName + " selected!");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                        }
                        else
                        {
                            Console.WriteLine();
                        }
                        goto default;
                    case 4:
                        Console.WriteLine(" Avengers Team");
                        if (arena.AvengersChecker())
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("     \u00BB Avengers Team assembled!");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                        }
                        else
                        {
                            Console.WriteLine();
                        }
                        goto default;
                    case 5:
                        Console.WriteLine(" Fight");
                        goto default;
                    default:
                        Console.Write(" ");
                        Console.BackgroundColor = ConsoleColor.Yellow;
                        Console.ForegroundColor = ConsoleColor.Black;
                        if (i <= 4)
                        {
                            Console.Write(i + 1);
                        }
                        Console.ResetColor();
                        break;
                }
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\n Select an option (number) : ");
            Console.ResetColor();
            byte option=0;
            try
            {
                option = byte.Parse(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Wrong type of value given!\nExpected Byte");
                errorGiven = true;
                Console.ReadLine();
                Console.Clear();
            }
            return option;
        }
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WindowWidth = 118; //max is 240
            Console.WindowHeight = 30; //max is 63

            while(true)
            {
                StartUpLogo();
                byte option = MainSelectionMenu();
                if (!errorGiven)
                {
                    switch (option)
                    {
                        case 1:
                            arena.PlanetSelector();
                            break;
                        case 2:
                            arena.VillainSelector();
                            break;
                        case 3:
                            arena.HeroSelector();
                            break;
                        case 4:
                            arena.AvangersTeam();
                            break;
                        case 5:
                            arena.Fight();
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
