using System;

namespace CorebuildAssignment
{
    class Program
    {
        private static Arena arena;
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
            ColorWriter.WriteLine("Yellow", " Main menu options: \n");
            ColorWriter.SpaceWrite("Yellow", "Black", "1");
            ColorWriter.SpaceWrite("Gray", "Select a");
            ColorWriter.SpaceWriteLine("Cyan", "Planet");
            if (arena.PlanetChecker() != null)
            {
                ColorWriter.SpaceWriteLine("Green", "     \u00BB " + arena.PlanetChecker() + " selected!");
            }
            else
            {
                Console.WriteLine();
            }
            ColorWriter.SpaceWrite("Yellow", "Black", "2");
            ColorWriter.SpaceWrite("Gray", "Select a");
            ColorWriter.SpaceWriteLine("Cyan", "Villain");
            if (arena.VillainChecker() != null)
            {
                ColorWriter.SpaceWriteLine("Green", "     \u00BB " + arena.VillainChecker() + " selected!");
            }
            else
            {
                Console.WriteLine();
            }
            ColorWriter.SpaceWrite("Yellow", "Black", "3");
            ColorWriter.SpaceWrite("Gray", "Select a");
            ColorWriter.SpaceWriteLine("Cyan", "Hero");
            if (arena.HeroChecker() != null)
            {
                ColorWriter.SpaceWriteLine("Green", "     \u00BB " + arena.HeroChecker() + " selected!");
            }
            else
            {
                Console.WriteLine();
            }
            ColorWriter.SpaceWrite("Yellow", "Black", "4");
            ColorWriter.SpaceWrite("Gray", "Build the");
            ColorWriter.SpaceWriteLine("Cyan", "Avengers Team");
            if (arena.AvengersChecker())
            {
                ColorWriter.SpaceWriteLine("Green", "     \u00BB Avengers Team assembled!");
            }
            else
            {
                Console.WriteLine();
            }
            ColorWriter.SpaceWrite("Yellow", "Black", "5");
            ColorWriter.SpaceWrite("Gray", "Start the");
            ColorWriter.SpaceWriteLine("Cyan", "Fight\n");
            ColorWriter.SpaceWrite("Yellow", "Black", "6");
            ColorWriter.SpaceWriteLine("Gray", "Exit");
            ColorWriter.Write("Yellow", "\n Select an option (number) : ");
            byte option = 0;
            try
            {
                option = byte.Parse(Console.ReadLine());
            }
            catch (Exception)
            {
                SpecialMessage.ErrorMessage("Wrong type of value given!\n Expected Byte!");
                errorGiven = true;
            }
            return option;
        }
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WindowWidth = (int)(Console.LargestWindowWidth * 0.495d);
            Console.WindowHeight = (int)(Console.LargestWindowHeight * 0.667d);
            arena = new Arena();
            while (true)
            {
                StartUpLogo();
                errorGiven = false;
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
                            arena.FightMenu();
                            break;
                        case 6:
                            goto exit;
                        default:
                            SpecialMessage.ErrorMessage("No option of value: " + option + " found!");
                            break;
                    }
                }
            }
        exit:;
        }
    }
}