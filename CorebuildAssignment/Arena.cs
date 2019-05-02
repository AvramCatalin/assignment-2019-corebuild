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
        private byte idSelectedPlanet;
        private byte idSelectedHero;
        private byte idSelectedVillain;
        private static Planets planets;
        private static Characters characters;
        private static List<Character> avengersList = new List<Character>();

        private void OptionSelector(string dataName)
        {
            bool whileRunner = true;
            while (whileRunner)
            {
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine(dataName + " Selector\n");

                bool errorGiven = false;
                idSelectedPlanet = 0;
                if (dataName == "Planet")
                {
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
                Console.Write("\nSelect a " + dataName.ToLower() + " : ");
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
                                ModifierWriter();
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
                            if (dataName == "Hero")
                            {
                                Console.WriteLine(" Start the fight with the selected " + dataName.ToLower());
                            }
                            else
                            {
                                Console.WriteLine(" Confirm selected " + dataName.ToLower());
                            }
                            Console.BackgroundColor = ConsoleColor.Yellow;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write("2");
                            Console.ResetColor();
                            Console.WriteLine(" Go back to the selection menu");

                            if (dataName == "Hero")
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

                                Console.BackgroundColor = ConsoleColor.Yellow;
                                Console.ForegroundColor = ConsoleColor.Black;
                                Console.Write("6");
                                Console.ResetColor();
                                Console.WriteLine(" Start the fight with the Avengers team");
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
                                        if (dataName == "Hero")
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
                                        if (dataName == "Hero")
                                        {
                                            AddHeroToAvengers(idSelectedHero,false);
                                        }
                                        else
                                        {
                                            goto default;
                                        }
                                        break;
                                    case 4:
                                        if (dataName == "Hero")
                                        {
                                            ShowAvengersTeam();
                                        }
                                        else
                                        {
                                            goto default;
                                        }
                                        break;
                                    case 5:
                                        if (dataName == "Hero")
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
                                    case 6:
                                        if (dataName == "Hero")
                                        {
                                            if (!avengersList.Any())
                                            {
                                                goto case 4;
                                            }
                                            else
                                            {
                                                whileRunner2 = false;
                                                whileRunner = false;
                                                Console.ForegroundColor = ConsoleColor.Green;
                                                Console.Write("\nAvengers assemble!");
                                                Console.ReadLine();
                                            }
                                            
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
            XmlSerializer serializer = new XmlSerializer(typeof(Planets));
            using (FileStream fileStream = new FileStream(projectDirectory + @"\InputFiles\planets.xml", FileMode.Open))
            {
                planets = (Planets)serializer.Deserialize(fileStream);
            }
            OptionSelector("Planet");
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
            OptionSelector("Hero");
        }
        public void Fight()
        {

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
    }
}
