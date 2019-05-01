using System;
using System.IO;
using System.Xml.Serialization;

namespace CorebuildAssignment
{
    class Arena : IArena
    {
        static readonly string workingDirectory = Environment.CurrentDirectory;
        static readonly string projectDirectory = Directory.GetParent(workingDirectory).Parent.FullName;

        public void PlanetSelector()
        {
            Planets planets;
            XmlSerializer serializer2 = new XmlSerializer(typeof(Planets));
            using (FileStream fileStream = new FileStream(projectDirectory + @"\InputFiles\planets.xml", FileMode.Open))
            {
                planets = (Planets)serializer2.Deserialize(fileStream);
            }
            foreach (Planet planet in planets.Planet)
            {
                Console.WriteLine(planet.Id + " " + planet.Name + " " + planet.Description + " ");

                foreach (Modifiers modifiers in planet.Modifiers)
                {
                    Console.WriteLine(modifiers.HeroAttackModifier + " " + modifiers.HeroHealthModifier + " " + modifiers.VillainAttackModifier + " " + modifiers.VillainHealthModifier);
                }
            }
            Console.ReadKey();
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
