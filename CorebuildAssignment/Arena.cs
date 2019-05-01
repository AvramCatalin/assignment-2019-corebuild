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
        public void PlanetSelector()
        {
            Planets planets;
            XmlSerializer serializer = new XmlSerializer(typeof(Planets));
            using (FileStream fileStream = new FileStream(projectDirectory + @"\InputFiles\planets.xml", FileMode.Open))
            {
                planets = (Planets)serializer.Deserialize(fileStream);
            }
            bool whileRunner = true;
            while (whileRunner)
            {
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
                    Console.WriteLine(planet.Description);
                }
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("\nSelect a Planet: ");
                Console.ResetColor();
                try
                {
                    idSelectedPlanet = int.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Wrong type of value given!\nExpected Integer!");
                    Console.ReadLine();
                    Console.Clear();
                }
                finally
                {
                    if (idSelectedPlanet >= 1 && idSelectedPlanet <= 5)
                    {
                        whileRunner = false;
                        Console.ReadLine();
                    }
                    else
                    {
                        if (idSelectedPlanet == 0)
                        {
                            Console.Clear();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("No planet of id = " + idSelectedPlanet + " was found");
                            Console.ReadLine();
                            Console.Clear();
                        }
                    }
                }
            }
            /*
              foreach (Modifiers modifiers in planet.Modifiers)
              {
                 Console.WriteLine(modifiers.HeroAttackModifier + " " + modifiers.HeroHealthModifier + " " + modifiers.VillainAttackModifier + " " + modifiers.VillainHealthModifier);
              }
           */


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
