using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace CorebuildAssignment
{
    class Arena : IArena
    {
        private static readonly string workingDirectory = Environment.CurrentDirectory;
        private static readonly string projectDirectory = Directory.GetParent(workingDirectory).Parent.FullName;
        private byte idSelectedPlanet=0;
        private byte idSelectedHero=0;
        private byte idSelectedVillain=0;
        private Planets planets;
        private Characters characters;
        private List<Character> avengersList = new List<Character>();

        private void OptionSelector(string dataName)
        {
            bool whileRunner = true;
            while (whileRunner)
            {
                Console.Write(" ");
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine(dataName + " Selector\n");
                Console.ResetColor();

                bool errorGiven = false;
                if (dataName == "Planet")
                {
                    foreach (Planet planet in planets.Planet)
                    {
                        Console.Write(" ");
                        Console.BackgroundColor = ConsoleColor.Yellow;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write(planet.Id);
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine(" " + planet.Name);
                        Console.ResetColor();
                        Console.WriteLine(" \u2022 " + planet.Description);
                    }
                }
                else
                {
                    if (dataName == "Villain")
                    {
                        foreach (Character character in characters.Character)
                        {
                            if (character.IsVillain)
                            {
                                Console.BackgroundColor = ConsoleColor.Yellow;
                                Console.ForegroundColor = ConsoleColor.Black;
                                Console.Write(character.Id);
                                Console.BackgroundColor = ConsoleColor.Black;
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.WriteLine(" " + character.Name);
                                Console.ResetColor();
                                Console.WriteLine(" \u2022 " + character.Description);
                            }
                        }
                    }
                    if (dataName == "Hero")
                    {
                        foreach (Character character in characters.Character)
                        {
                            if (!character.IsVillain)
                            {
                                Console.BackgroundColor = ConsoleColor.Yellow;
                                Console.ForegroundColor = ConsoleColor.Black;
                                Console.Write(character.Id);
                                Console.BackgroundColor = ConsoleColor.Black;
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.WriteLine(" " + character.Name);
                                Console.ResetColor();
                                Console.WriteLine(" \u2022 " + character.Description);
                            }
                        }
                    }

                }
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("\n Select a " + dataName.ToLower() + " : ");
                Console.ResetColor();
                byte idSelectedEntity = 0;
                try
                {
                    idSelectedEntity = byte.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Wrong type of value given!\nExpected Byte");
                    errorGiven = true;
                    Console.ReadLine();
                    Console.Clear();
                }
                finally
                {
                    bool entityFound = false;
                    if (dataName == "Planet")
                    {
                        foreach (Planet planet in planets.Planet)
                        {
                            if (planet.Id == idSelectedEntity)
                            {
                                entityFound = true;
                                idSelectedPlanet = idSelectedEntity;
                            }
                        }
                    }
                    else
                    {
                        if (dataName == "Villain")
                        {
                            foreach (Character character in characters.Character)
                            {
                                if (character.Id == idSelectedEntity && character.IsVillain)
                                {
                                    entityFound = true;
                                    idSelectedVillain = idSelectedEntity;
                                }
                            }
                        }
                        else
                        {
                            if (dataName == "Hero")
                            {
                                foreach (Character character in characters.Character)
                                {
                                    if (character.Id == idSelectedEntity && !character.IsVillain)
                                    {
                                        entityFound = true;
                                        idSelectedHero = idSelectedEntity;
                                    }
                                }
                            }
                        }
                    }
                    if (entityFound)
                    {
                        bool whileRunner2 = true;
                        while (whileRunner2)
                        {
                            bool errorGiven2 = false;
                            byte option = 0;
                            if (dataName == "Planet")
                            {
                                PlanetDetails();
                            }
                            else
                            {
                                if (dataName == "Villain")
                                {
                                    CharacterDetails(idSelectedVillain);
                                }
                                else
                                {
                                    if (dataName == "Hero")
                                    {
                                        CharacterDetails(idSelectedHero);
                                    }
                                }
                            }

                            Console.WriteLine("\nOptions: ");
                            Console.BackgroundColor = ConsoleColor.Yellow;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write("1");
                            Console.ResetColor();
                            Console.WriteLine(" Confirm selected " + dataName.ToLower());
                            Console.BackgroundColor = ConsoleColor.Yellow;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write("2");
                            Console.ResetColor();
                            Console.WriteLine(" Go back to the selection menu");

                            if (dataName == "Avangers")
                            {
                                Console.BackgroundColor = ConsoleColor.Yellow;
                                Console.ForegroundColor = ConsoleColor.Black;
                                Console.Write("3");
                                Console.ResetColor();
                                Console.WriteLine(" Add to Avengers team");

                                Console.BackgroundColor = ConsoleColor.Yellow;
                                Console.ForegroundColor = ConsoleColor.Black;
                                Console.Write("4");
                                Console.ResetColor();
                                Console.WriteLine(" Show Avengers team");

                                Console.BackgroundColor = ConsoleColor.Yellow;
                                Console.ForegroundColor = ConsoleColor.Black;
                                Console.Write("5");
                                Console.ResetColor();
                                Console.WriteLine(" Clear the Avengers team");
                            }

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("\nSelect an option: ");
                            Console.ResetColor();
                            try
                            {
                                option = byte.Parse(Console.ReadLine());
                            }
                            catch (Exception)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Wrong type of value given!\nExpected Byte!");
                                errorGiven2 = true;
                                Console.ReadLine();
                                Console.Clear();
                            }
                            finally
                            {
                                switch (option)
                                {
                                    case 1:
                                        whileRunner2 = false;
                                        whileRunner = false;
                                        if (dataName == "Avengers")
                                        {
                                            avengersList.Clear();
                                            AddHeroToAvengers(idSelectedHero,true);
                                            Console.ForegroundColor = ConsoleColor.Green;
                                            Console.WriteLine("\nStarting the fight with the selected hero!");
                                            Console.ReadLine();
                                        }
                                        break;
                                    case 2:
                                        whileRunner2 = false;
                                        Console.Clear();
                                        break;
                                    case 3:
                                        if (dataName == "Avengers")
                                        {
                                            AddHeroToAvengers(idSelectedHero,false);
                                        }
                                        else
                                        {
                                            goto default;
                                        }
                                        break;
                                    case 4:
                                        if (dataName == "Avengers")
                                        {
                                            ShowAvengersTeam();
                                        }
                                        else
                                        {
                                            goto default;
                                        }
                                        break;
                                    case 5:
                                        if (dataName == "Avengers")
                                        {
                                            avengersList.Clear();
                                            Console.ForegroundColor = ConsoleColor.Green;
                                            Console.Write("\nAvengers team was cleared!");
                                            Console.ReadLine();
                                        }
                                        else
                                        {
                                            goto default;
                                        }
                                        break;
                                    default:
                                        if (!errorGiven2)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine("No option of value = " + option + " was found");
                                            Console.ReadLine();
                                            Console.Clear();
                                        }
                                        break;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (!errorGiven)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("No " + dataName.ToLower() + " of id = " + idSelectedEntity + " was found");
                            Console.ReadLine();
                            Console.Clear();
                        }
                    }
                }
            }
            Console.Clear();
        }
        public void PlanetSelector()
        {
            Console.Clear();
            idSelectedPlanet = 0;
            XmlSerializer serializer = new XmlSerializer(typeof(Planets));
            using (FileStream fileStream = new FileStream(projectDirectory + @"\InputFiles\planets.xml", FileMode.Open))
            {
                planets = (Planets)serializer.Deserialize(fileStream);
            }
            OptionSelector("Planet");
        }
        private void PlanetDetails()
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
            Console.Clear();
            idSelectedVillain = 0;
            XmlSerializer serializer = new XmlSerializer(typeof(Characters));
            using (FileStream fileStream = new FileStream(projectDirectory + @"\InputFiles\characters.xml", FileMode.Open))
            {
                characters = (Characters)serializer.Deserialize(fileStream);
            }
            OptionSelector("Villain");
        }
        private void CharacterDetails(byte idSelectedCharacter)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(characters.Character[idSelectedCharacter - 1].Name);
            Console.ResetColor();
            Console.WriteLine(" \u2022 " + characters.Character[idSelectedCharacter - 1].Description);

            Console.Write(" \u00BB " + "Attack points: ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(characters.Character[idSelectedCharacter - 1].Attack);
            Console.ResetColor();

            Console.Write(" \u00BB " + "Health points: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(characters.Character[idSelectedCharacter - 1].Health);
            Console.ResetColor();
        }
        public void HeroSelector()
        {
            Console.Clear();
            idSelectedHero = 0;
            XmlSerializer serializer = new XmlSerializer(typeof(Characters));
            using (FileStream fileStream = new FileStream(projectDirectory + @"\InputFiles\characters.xml", FileMode.Open))
            {
                characters = (Characters)serializer.Deserialize(fileStream);
            }
            OptionSelector("Hero");
        }
        public void AvangersTeam()
        {
            Console.Clear();
        }
        public void Fight()
        {
            Console.Clear();
        }
        private void AddHeroToAvengers(byte idHeroToAdd,bool silentMode)
        {
            bool heroExists = false;
            foreach (Character hero in avengersList)
            {
                if (hero.Id == idHeroToAdd)
                {
                    heroExists = true;
                }
            }
            if(!heroExists)
            {
                foreach (Character character in characters.Character)
                {
                    if (idHeroToAdd == character.Id)
                    {
                        avengersList.Add(character);
                    }
                }
                if(!silentMode)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("\nHero added succesfully!");
                    Console.ReadLine();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("\nSelected Hero is already part of the Avengers Team!");
                Console.ReadLine();
            }
        }
        private void ShowAvengersTeam()
        {  
            Console.WriteLine();
            if (!avengersList.Any())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("There are no Heroes in the Avengers Team!");
            }
            else
            {
                byte i = 1;
                foreach (Character character in avengersList)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write(i+") ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(character.Name);
                    i++;
                }
            }
            Console.ReadLine();
        }
        public string PlanetChecker()
        {
            if(idSelectedPlanet!= 0)
            {
                return planets.Planet[idSelectedPlanet - 1].Name;
            }
            else
            {
                return null;
            }
        }
        public string VillainChecker()
        {
            if (idSelectedVillain != 0)
            {
                return characters.Character[idSelectedVillain - 1].Name;
            }
            else
            {
                return null;
            }
        }
        public string HeroChecker()
        {
            if (idSelectedHero != 0)
            {
                return characters.Character[idSelectedHero - 1].Name;
            }
            else
            {
                return null;
            }
        }
        public bool AvengersChecker()
        {
            if (avengersList.Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
