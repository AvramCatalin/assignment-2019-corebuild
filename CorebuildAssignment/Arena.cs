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
            while (true)
            {
                ColorWriter.SpaceWriteLine("Yellow", "Black", dataName + " Selector\n");
                bool errorGiven = false;
                if (dataName == "Planet")
                {
                    foreach (Planet planet in planets.Planet)
                    {
                        ColorWriter.SpaceWrite("Yellow", "Black", planet.Id.ToString());
                        ColorWriter.SpaceWrite("Cyan", " " + planet.Name + '\n');
                        ColorWriter.SpaceWriteLine("DarkGray", " \u2022 " + planet.Description);
                    }
                }
                else
                {
                    foreach (Character character in characters.Character)
                    {
                        if (character.IsVillain && dataName == "Villain")
                        {
                            ColorWriter.SpaceWrite("Yellow", "Black", character.Id.ToString());
                            ColorWriter.SpaceWrite("Cyan", " " + character.Name + '\n');
                            ColorWriter.SpaceWriteLine("DarkGray", " \u2022 " + character.Description);
                        }
                        if (!character.IsVillain && dataName == "Hero")
                        {
                            ColorWriter.SpaceWrite("Yellow", "Black", character.Id.ToString());
                            ColorWriter.SpaceWrite("Cyan", " " + character.Name + '\n');
                            ColorWriter.SpaceWriteLine("DarkGray", " \u2022 " + character.Description);
                        }
                    }
                }
                ColorWriter.Write("Yellow", "\n Select a " + dataName.ToLower() + " : ");
                byte idSelectedEntity = 0;
                try
                {
                    idSelectedEntity = byte.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                    SpecialMessage.ErrorMessage("Wrong type of value given!\n Expected Byte");
                    errorGiven = true;
                }
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
                    while (true)
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
                        ColorWriter.WriteLine("Yellow", "\n Options: \n");
                        ColorWriter.SpaceWrite("Yellow", "Black", "1");
                        if (dataName == "Hero" && multiple)
                        {
                            ColorWriter.WriteLine("Gray", " Add to the Avengers team\n");
                        }
                        else
                        {
                            ColorWriter.WriteLine("Gray", " Confirm selected " + dataName.ToLower()+'\n');
                        }
                        ColorWriter.SpaceWrite("Yellow", "Black", "2");
                        ColorWriter.WriteLine("Gray", " Go back to the selection menu");
                        if (dataName == "Hero" && multiple)
                        {
                            Console.WriteLine();
                            ColorWriter.SpaceWrite("Yellow", "Black", "3");
                            ColorWriter.WriteLine("Gray", " Show the Avengers team\n");

                            ColorWriter.SpaceWrite("Yellow", "Black", "4");
                            ColorWriter.WriteLine("Gray", " Clear the Avengers team\n");

                            ColorWriter.SpaceWrite("Yellow", "Black", "5");
                            ColorWriter.WriteLine("Gray", " Confirm Avengers Team");

                        }
                        ColorWriter.Write("Yellow", "\n Select an option: ");
                        try
                        {
                            option = byte.Parse(Console.ReadLine());
                        }
                        catch (Exception)
                        {
                            SpecialMessage.ErrorMessage("Wrong type of value given!\n Expected Byte!");
                            errorGiven2 = true;
                        }
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
                                    goto mainExit;
                                }
                                break;
                            case 2:
                                Console.Clear();
                                goto localExit;
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
                                        SpecialMessage.SuccessMessage("The Avengers team was cleared!");
                                    }
                                    else
                                    {
                                        SpecialMessage.ErrorMessage("The Avengers team is already empty!");
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
                                    goto mainExit;
                                }
                                else
                                {
                                    goto default;
                                }
                            default:
                                if (!errorGiven2)
                                {
                                    SpecialMessage.ErrorMessage("No option of value = " + option + " was found");
                                }
                                break;
                        }
                    }
                localExit:;
                }
                else
                {
                    if (!errorGiven)
                    {
                        SpecialMessage.ErrorMessage("No " + dataName.ToLower() + " of id = " + idSelectedEntity + " was found");
                    }
                }
            }
        mainExit:
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
            ColorWriter.WriteLine("Cyan"," "+planets.Planet[idSelectedPlanet - 1].Name);
            ColorWriter.WriteLine("DarkGray", " \u2022 " + planets.Planet[idSelectedPlanet - 1].Description);
            ColorWriter.Write("DarkGray", " \u00BB " + "Villain Attack Modifier: ");
            if (planets.Planet[idSelectedPlanet - 1].Modifiers.VillainAttackModifier > 0)
            {
                ColorWriter.WriteLine("Green", "+" + planets.Planet[idSelectedPlanet - 1].Modifiers.VillainAttackModifier.ToString());
            }
            else
            {
                ColorWriter.WriteLine("Red",planets.Planet[idSelectedPlanet - 1].Modifiers.VillainAttackModifier.ToString());
            }
            ColorWriter.Write("DarkGray", " \u00BB " + "Villain Health Modifier: ");
            if (planets.Planet[idSelectedPlanet - 1].Modifiers.VillainHealthModifier > 0)
            {
                ColorWriter.WriteLine("Green", "+" + planets.Planet[idSelectedPlanet - 1].Modifiers.VillainHealthModifier.ToString());
            }
            else
            {
                ColorWriter.WriteLine("Red", planets.Planet[idSelectedPlanet - 1].Modifiers.VillainHealthModifier.ToString());
            }
            ColorWriter.Write("DarkGray", " \u00BB " + "Hero Attack Modifier: ");
            if (planets.Planet[idSelectedPlanet - 1].Modifiers.HeroAttackModifier > 0)
            {
                ColorWriter.WriteLine("Green", "+" + planets.Planet[idSelectedPlanet - 1].Modifiers.HeroAttackModifier.ToString());
            }
            else
            {
                ColorWriter.WriteLine("Red", planets.Planet[idSelectedPlanet - 1].Modifiers.HeroAttackModifier.ToString());
            }
            ColorWriter.Write("DarkGray", " \u00BB " + "Hero Health Modifier: ");
            if (planets.Planet[idSelectedPlanet - 1].Modifiers.HeroHealthModifier > 0)
            {
                ColorWriter.WriteLine("Green", "+" + planets.Planet[idSelectedPlanet - 1].Modifiers.HeroHealthModifier.ToString());
            }
            else
            {
                ColorWriter.WriteLine("Red", planets.Planet[idSelectedPlanet - 1].Modifiers.HeroHealthModifier.ToString());
            }
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
                    if (whoAttacksFirst == 1)
                    {
                        CharacterVsCharacter(fightingHero, fightingVillain);
                    }
                    else
                    {
                        CharacterVsCharacter(fightingVillain, fightingHero);
                    }
                }
                if (fightingHero.Health > 0 && fightingVillain.Health <= 0)
                {
                    Console.WriteLine("\n" + fightingHero.Name + " wins!");
                }
                else
                {
                    Console.WriteLine("\n" + fightingVillain.Name + " wins!");
                }
                Console.ReadLine();
            }
            else
            {
                FightingInitializer(2);
                while (AvengersStillAlive() && fightingVillain.Health > 0)
                {
                    foreach (Character avenger in avengersList)
                    {
                        if (avenger.Health > 0 && fightingVillain.Health > 0)
                        {
                            if (whoAttacksFirst == 1)
                            {
                                CharacterVsCharacter(avenger, fightingVillain);
                            }
                            else
                            {
                                CharacterVsCharacter(fightingVillain, avenger);
                            }
                        }
                    }
                }
                if (AvengersStillAlive() && fightingVillain.Health <= 0)
                {
                    Console.WriteLine("\n" + "The Avengers win!");
                }
                else
                {
                    Console.WriteLine("\n" + fightingVillain.Name + " wins!");
                }
                Console.ReadLine();
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
            //avatar1 attacks avatar2
            if (avatar2.Health > 0 && avatar1.Health > 0)
            {
                damage = (short)(avatar1.Attack * (random.Next(60, 101) / 100f));
                Console.WriteLine(avatar1.Name + " attacks " + avatar2.Name + " and deals " + damage + " points of damage");
                avatar2.Health = (short)(avatar2.Health - damage);
                Console.WriteLine(avatar2.Name + " HP = " + avatar2.Health + "\n");
            }
            if (avatar2.Health <= 0)
            {
                Console.WriteLine(avatar2.Name + " was defeated!");
            }
            //avatar2 attacks avatar1
            if (avatar2.Health > 0 && avatar1.Health > 0)
            {
                damage = (short)(avatar2.Attack * (random.Next(60, 101) / 100f));
                Console.WriteLine(avatar2.Name + " attacks " + avatar1.Name + " and deals " + damage + " points of damage");
                avatar1.Health = (short)(avatar1.Health - damage);
                Console.WriteLine(avatar1.Name + " HP = " + avatar1.Health + "\n");
            }
            if (avatar1.Health <= 0)
            {
                Console.WriteLine(avatar1.Name + " was defeated!");
            }
            System.Threading.Thread.Sleep(2000);
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
