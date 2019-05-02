using System;
using System.IO;
using System.Xml.Serialization;

namespace CorebuildAssignment
{
    class Arena : IArena
    {
        static readonly string workingDirectory = Environment.CurrentDirectory;
        static readonly string projectDirectory = Directory.GetParent(workingDirectory).Parent.FullName;
        private int idSelectedPlanet;
        private static Planets planets;

        public void PlanetSelector()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            XmlSerializer serializer = new XmlSerializer(typeof(Planets));
            using (FileStream fileStream = new FileStream(projectDirectory + @"\InputFiles\planets.xml", FileMode.Open))
            {
                planets = (Planets)serializer.Deserialize(fileStream);
            }
            bool whileRunner = true;
            while (whileRunner)
            {
                bool errorGiven = false;
                idSelectedPlanet = 0;
                foreach (Planet planet in planets.Planet)
                {
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write(planet.Id);
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(" " + planet.Name);
                    Console.ResetColor();
                    Console.WriteLine(" \u2022 " + planet.Description);
                }
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("\nSelect a planet: ");
                Console.ResetColor();
                try
                {
                    idSelectedPlanet = int.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Wrong type of value given!\nExpected Integer!");
                    errorGiven = true;
                    Console.ReadLine();
                    Console.Clear();
                }
                finally
                {
                    if (idSelectedPlanet >= 1 && idSelectedPlanet <= 5)
                    {
                        bool whileRunner2 = true;
                        while (whileRunner2)
                        {
                            bool errorGiven2 = false;
                            int option = 0;
                            ModifierWriter();

                            Console.WriteLine("\nOptions: ");
                            Console.BackgroundColor = ConsoleColor.Yellow;
                            Console.ForegroundColor = ConsoleColor.Black;

                            Console.Write("1");
                            Console.ResetColor();
                            Console.WriteLine(" Confirm selected planet");
                            Console.BackgroundColor = ConsoleColor.Yellow;
                            Console.ForegroundColor = ConsoleColor.Black;

                            Console.Write("2");
                            Console.ResetColor();
                            Console.WriteLine(" Go back to the selection menu");

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("\nSelect an option: ");
                            Console.ResetColor();
                            try
                            {
                                option = int.Parse(Console.ReadLine());
                            }
                            catch (Exception)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Wrong type of value given!\nExpected Integer!");
                                errorGiven2 = true;
                                Console.ReadLine();
                                Console.Clear();
                            }
                            finally
                            {
                                if (option == 1)
                                {
                                    whileRunner2 = false;
                                    whileRunner = false;
                                }
                                else
                                {
                                    if (option == 2)
                                    {
                                        whileRunner2 = false;
                                        Console.Clear();
                                    }
                                    else
                                    {
                                        if (!errorGiven2)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine("No option of value = " + option + " was found");
                                            Console.ReadLine();
                                            Console.Clear();
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (!errorGiven)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("No planet of id = " + idSelectedPlanet + " was found");
                            Console.ReadLine();
                            Console.Clear();
                        }
                    }
                }
            }
        }
        private void ModifierWriter()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(planets.Planet[idSelectedPlanet - 1].Name);
            Console.ResetColor();
            Console.WriteLine(" \u2022 " + planets.Planet[idSelectedPlanet - 1].Description);

            Console.Write(" \u00BB " + "Hero Attack Modifier: ");
            Console.ForegroundColor = ConsoleColor.Red;

            char plus = '\0';
            if (planets.Planet[idSelectedPlanet - 1].Modifiers.HeroAttackModifier > 0)
            {
                plus = '+';
            }
            Console.WriteLine(plus + "" + planets.Planet[idSelectedPlanet - 1].Modifiers.HeroAttackModifier);

            Console.ResetColor();

            Console.Write(" \u00BB " + "Hero Health Modifier: ");
            Console.ForegroundColor = ConsoleColor.Green;
            plus = '\0';
            if (planets.Planet[idSelectedPlanet - 1].Modifiers.HeroHealthModifier > 0)
            {
                plus = '+';
            }
            Console.WriteLine(plus + "" + planets.Planet[idSelectedPlanet - 1].Modifiers.HeroHealthModifier);
            Console.ResetColor();

            Console.Write(" \u00BB " + "Villain Attack Modifier: ");
            Console.ForegroundColor = ConsoleColor.Red;
            plus = '\0';
            if (planets.Planet[idSelectedPlanet - 1].Modifiers.VillainAttackModifier > 0)
            {
                plus = '+';
            }
            Console.WriteLine(plus + "" + planets.Planet[idSelectedPlanet - 1].Modifiers.VillainAttackModifier);
            Console.ResetColor();

            Console.Write(" \u00BB " + "Villain Health Modifier: ");
            Console.ForegroundColor = ConsoleColor.Green;
            plus = '\0';
            if (planets.Planet[idSelectedPlanet - 1].Modifiers.VillainHealthModifier > 0)
            {
                plus = '+';
            }
            Console.WriteLine(plus + "" + planets.Planet[idSelectedPlanet - 1].Modifiers.VillainHealthModifier);
            Console.ResetColor();
        }
        public void VillainSelector()
        {
            Characters characters;
            XmlSerializer serializer = new XmlSerializer(typeof(Characters));
            using (FileStream fileStream = new FileStream(projectDirectory + @"\InputFiles\characters.xml", FileMode.Open))
            {
                characters = (Characters)serializer.Deserialize(fileStream);
            }
            foreach (Character character in characters.Character)
            {
                Console.WriteLine(character.Id + " " + character.Name + " " + character.Description + " " + character.Health + " " + character.Attack + " " + character.IsVillain);
            }
        }
        public void HeroSelector()
        {
            Characters characters;
            XmlSerializer serializer = new XmlSerializer(typeof(Characters));
            using (FileStream fileStream = new FileStream(projectDirectory + @"\InputFiles\characters.xml", FileMode.Open))
            {
                characters = (Characters)serializer.Deserialize(fileStream);
            }
            foreach (Character character in characters.Character)
            {
                Console.WriteLine(character.Id + " " + character.Name + " " + character.Description + " " + character.Health + " " + character.Attack + " " + character.IsVillain);
            }
        }
        public void AvengersSelector()
        {

        }
        public void Fight()
        {

        }
    }
}
