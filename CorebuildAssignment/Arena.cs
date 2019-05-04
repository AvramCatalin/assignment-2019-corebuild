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
        private byte idSelectedPlanet = 0;
        private byte idSelectedHero = 0;
        private byte idSelectedVillain = 0;
        private Planets planets;
        private Characters characters;
        private List<Character> avengersList = new List<Character>();
        private Planet fightingPlanet;
        private Character fightingHero;
        private Character fightingVillain;

        private void OptionSelector(string dataName, bool multiple)
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
                                Console.Write(" ");
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
                                Console.Write(" ");
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
                                    CharacterDetails(idSelectedEntity);
                                }
                                else
                                {
                                    if (dataName == "Hero")
                                    {
                                        CharacterDetails(idSelectedEntity);
                                    }
                                }
                            }

                            Console.WriteLine("\nOptions: ");
                            Console.BackgroundColor = ConsoleColor.Yellow;
                            Console.ForegroundColor = ConsoleColor.Black;
                            if (dataName == "Hero" && multiple)
                            {
                                Console.Write("1");
                                Console.ResetColor();
                                Console.WriteLine(" Add to the Avengers team");
                            }
                            else
                            {
                                Console.Write("1");
                                Console.ResetColor();
                                Console.WriteLine(" Confirm selected " + dataName.ToLower());
                            }
                            Console.BackgroundColor = ConsoleColor.Yellow;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write("2");
                            Console.ResetColor();
                            Console.WriteLine(" Go back to the selection menu");
                            if (dataName == "Hero" && multiple)
                            {
                                Console.BackgroundColor = ConsoleColor.Yellow;
                                Console.ForegroundColor = ConsoleColor.Black;
                                Console.Write("3");
                                Console.ResetColor();
                                Console.WriteLine(" Show the Avengers team");

                                Console.BackgroundColor = ConsoleColor.Yellow;
                                Console.ForegroundColor = ConsoleColor.Black;
                                Console.Write("4");
                                Console.ResetColor();
                                Console.WriteLine(" Clear the Avengers team");

                                Console.BackgroundColor = ConsoleColor.Yellow;
                                Console.ForegroundColor = ConsoleColor.Black;
                                Console.Write("5");
                                Console.ResetColor();
                                Console.WriteLine(" Confirm Avengers Team");
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
                                        if (dataName == "Hero" && multiple)
                                        {
                                            AddHeroToAvengers(idSelectedEntity);
                                        }
                                        else
                                        {
                                            if (dataName == "Hero" && !multiple)
                                            {
                                                idSelectedHero = idSelectedEntity;
                                            }
                                            whileRunner2 = false;
                                            whileRunner = false;
                                        }

                                        break;
                                    case 2:
                                        whileRunner2 = false;
                                        Console.Clear();
                                        break;
                                    case 3:
                                        if (dataName == "Hero" && multiple)
                                        {
                                            ShowAvengersTeam();
                                        }
                                        else
                                        {
                                            goto default;
                                        }
                                        break;
                                    case 4:
                                        if (dataName == "Hero" && multiple)
                                        {
                                            if (avengersList.Any())
                                            {
                                                avengersList.Clear();
                                                Console.ForegroundColor = ConsoleColor.Green;
                                                Console.Write("\nThe Avengers team was cleared!");
                                                Console.ReadLine();
                                            }
                                            else
                                            {
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.Write("\nThe Avengers team is already empty!");
                                                Console.ReadLine();
                                            }
                                        }
                                        else
                                        {
                                            goto default;
                                        }
                                        break;
                                    case 5:
                                        if (dataName == "Hero" && multiple)
                                        {
                                            whileRunner2 = false;
                                            whileRunner = false;
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
            OptionSelector("Planet", false);
        }
        private void PlanetDetails()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(planets.Planet[idSelectedPlanet - 1].Name);
            Console.ResetColor();
            Console.WriteLine(" \u2022 " + planets.Planet[idSelectedPlanet - 1].Description);

            Console.Write(" \u00BB " + "Villain Attack Modifier: ");
            Console.ForegroundColor = ConsoleColor.Red;
            char plus = '\0';
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

            Console.Write(" \u00BB " + "Hero Attack Modifier: ");
            Console.ForegroundColor = ConsoleColor.Red;

            plus = '\0';
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
            OptionSelector("Villain", false);
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
            OptionSelector("Hero", false);
        }
        public void AvangersTeam()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Characters));
            using (FileStream fileStream = new FileStream(projectDirectory + @"\InputFiles\characters.xml", FileMode.Open))
            {
                characters = (Characters)serializer.Deserialize(fileStream);
            }
            Console.Clear();
            OptionSelector("Hero", true);
        }
        private void IdToObject(string objectName)
        {
            if (objectName == "Planet")
            {
                foreach (Planet planet in planets.Planet)
                {
                    if (idSelectedPlanet == planet.Id)
                    {
                        fightingPlanet = planet;
                    }
                }
            }
            if (objectName == "Character")
            {
                foreach (Character character in characters.Character)
                {
                    if (idSelectedHero == character.Id)
                    {
                        fightingHero = character;
                    }
                    if (idSelectedVillain == character.Id)
                    {
                        fightingVillain = character;
                    }
                }
            }
        }
        public void FightMenu()
        {
            if (idSelectedPlanet != 0 && idSelectedVillain != 0)
            {
                IdToObject("Planet");
                while (true)
                {
                    Console.Clear();
                    Console.Write("\n ");
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write("1");
                    Console.ResetColor();
                    Console.WriteLine(" Villain vs Hero\n");

                    Console.Write(" ");
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write("2");
                    Console.ResetColor();
                    Console.WriteLine(" Villain vs Avengers\n");

                    Console.Write(" ");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(" Select an option: ");
                    Console.ResetColor();

                    byte option = 0;
                    try
                    {
                        option = byte.Parse(Console.ReadLine());
                    }
                    catch (Exception)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Wrong type of value given!\nExpected Byte");
                        Console.ReadLine();
                        Console.Clear();
                    }
                    switch (option)
                    {
                        case 1:
                            if (idSelectedHero != 0)
                            {
                                FightController(1);
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write(" No hero selected!");
                                Console.ReadLine();
                                Console.Clear();
                            }
                            goto exit;
                        case 2:
                            if (avengersList.Any())
                            {
                                FightController(2);
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write(" Avengers team empty!");
                                Console.ReadLine();
                                Console.Clear();
                            }
                            goto exit;
                        default:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write(" No option of value: " + option + " found!");
                            Console.ReadLine();
                            Console.Clear();
                            break;
                    }
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(" Please select a Planet and a Villain!");
                Console.ReadLine();
                Console.Clear();
            }
        exit:;
            Console.Clear();
        }
        private bool AvengersStillAlive()
        {
            bool alive = false;
            foreach (Character character in avengersList)
            {
                if (character.Health > 0)
                {
                    alive = true;
                }
            }
            return alive;
        }
        public void FightController(byte option)
        {
            IdToObject("Character");
            Random random = new Random();
            byte whoAttacksFirst = (byte)random.Next(2); //generates either a 0 or a 1
            Console.Write("Selecting who has the first turn (at random)");
            System.Threading.Thread.Sleep(500);
            Console.Write(" .");
            System.Threading.Thread.Sleep(1000);
            Console.Write(" .");
            System.Threading.Thread.Sleep(1000);
            Console.Write(" .\n");
            System.Threading.Thread.Sleep(250);
            if (whoAttacksFirst == 1)
            {
                Console.WriteLine("The Hero attacks first!");     
            }
            else
            {
                Console.WriteLine("The Villain attacks first!");
            }
            System.Threading.Thread.Sleep(2250);
            if (option == 1)
            {
                FightingInitializer(1);
                while (fightingHero.Health > 0 && fightingVillain.Health > 0)
                {
                    if(whoAttacksFirst == 1)
                    {
                        CharacterVsCharacter(fightingHero, fightingVillain);
                    }
                    else
                    {
                        CharacterVsCharacter(fightingVillain, fightingHero);
                    }
                }
            }
            else
            {
                FightingInitializer(2);
                while (AvengersStillAlive() && fightingVillain.Health > 0)
                {
                    break;
                }
            }
        }
        private void FightingInitializer(byte option)
        {
            PlanetDetails();
            Console.WriteLine("\n" + fightingPlanet.Name + " Modifiers Activated!\n");

            fightingVillain.Health = (short)(fightingVillain.Health + fightingPlanet.Modifiers.VillainHealthModifier);
            fightingVillain.Attack = (short)(fightingVillain.Attack + fightingPlanet.Modifiers.VillainAttackModifier);
            Console.WriteLine(fightingVillain.Name + ": new attack = " + fightingVillain.Attack);
            Console.WriteLine(fightingVillain.Name + ": new health = " + fightingVillain.Health + "\n");

            if (option == 1)
            {
                fightingHero.Health = (short)(fightingHero.Health + fightingPlanet.Modifiers.HeroHealthModifier);
                fightingHero.Attack = (short)(fightingHero.Attack + fightingPlanet.Modifiers.HeroAttackModifier);
                Console.WriteLine(fightingHero.Name + ": new attack = " + fightingHero.Attack);
                Console.WriteLine(fightingHero.Name + ": new health = " + fightingHero.Health);
            }
            else
            {
                foreach (Character character in avengersList)
                {
                    character.Health = (short)(character.Health + fightingPlanet.Modifiers.HeroHealthModifier);
                    character.Attack = (short)(character.Attack + fightingPlanet.Modifiers.HeroAttackModifier);
                    Console.WriteLine(character.Name + ": new attack = " + character.Attack);
                    Console.WriteLine(character.Name + ": new health = " + character.Health);
                }
            }
            Console.ReadLine();
        }
        private void CharacterVsCharacter(Character avatar1, Character avatar2)
        {
            Random random = new Random();
            short damage;
            damage = (short)(avatar1.Attack * (random.Next(60, 101) / 100f));
            Console.WriteLine(avatar1.Name + " attacks " + avatar2.Name + " and deals " + damage + " points of damage");
            avatar2.Health = (short)(avatar2.Health - damage);
            Console.WriteLine(avatar2.Name + " HP = " + avatar2.Health+"\n");

            if (avatar2.Health <= 0)
            {
                Console.WriteLine(avatar2.Name + " was defeated");
                Console.ReadLine();
            }

            if(avatar2.Health > 0)
            {
                damage = (short)(avatar2.Attack * (random.Next(60, 101) / 100f));
                Console.WriteLine(avatar2.Name + " attacks " + avatar1.Name + " and deals " + damage + " points of damage");
                avatar1.Health = (short)(avatar1.Health - damage);
                Console.WriteLine(avatar1.Name + " HP = " + avatar1.Health + "\n");
            }
            if(avatar1.Health <= 0)
            {
                Console.WriteLine(avatar1.Name + " was defeated");
                Console.ReadLine();
            }

            System.Threading.Thread.Sleep(4000);
        }
        private void AddHeroToAvengers(byte idHeroToAdd)
        {
            bool heroExists = false;
            foreach (Character hero in avengersList)
            {
                if (hero.Id == idHeroToAdd)
                {
                    heroExists = true;
                }
            }
            if (!heroExists)
            {
                foreach (Character character in characters.Character)
                {
                    if (idHeroToAdd == character.Id)
                    {
                        avengersList.Add(character);
                    }
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\nHero added succesfully!");
                Console.ReadLine();
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
                    Console.Write(i + ") ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(character.Name);
                    i++;
                }
            }
            Console.ReadLine();
        }
        public string PlanetChecker()
        {
            if (idSelectedPlanet != 0)
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
