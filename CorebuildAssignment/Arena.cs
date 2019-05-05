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
        private Characters characters = new Characters();
        private List<Character> avengersList = new List<Character>();
        private Planet fightingPlanet;
        private Character fightingHero;
        private Character fightingVillain;
        private bool loadingMessageShown;

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
                        ColorWriter.SpaceWrite("Cyan", planet.Name + '\n');
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
                            ColorWriter.SpaceWrite("Cyan", character.Name + '\n');
                            ColorWriter.SpaceWriteLine("DarkGray", " \u2022 " + character.Description);
                        }
                        if (!character.IsVillain && dataName == "Hero")
                        {
                            ColorWriter.SpaceWrite("Yellow", "Black", character.Id.ToString());
                            ColorWriter.SpaceWrite("Cyan", character.Name + '\n');
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
                            Console.Clear();
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
                            ColorWriter.SpaceWriteLine("Gray", "Add to the Avengers team\n");
                        }
                        else
                        {
                            ColorWriter.SpaceWriteLine("Gray", "Confirm selected " + dataName.ToLower() + '\n');
                        }
                        ColorWriter.SpaceWrite("Yellow", "Black", "2");
                        ColorWriter.SpaceWriteLine("Gray", "Go back to the selection menu");
                        if (dataName == "Hero" && multiple)
                        {
                            Console.WriteLine();
                            ColorWriter.SpaceWrite("Yellow", "Black", "3");
                            ColorWriter.SpaceWriteLine("Gray", "Show the Avengers team\n");

                            ColorWriter.SpaceWrite("Yellow", "Black", "4");
                            ColorWriter.SpaceWriteLine("Gray", "Clear the Avengers team\n");

                            ColorWriter.SpaceWrite("Yellow", "Black", "5");
                            ColorWriter.SpaceWriteLine("Gray", "Confirm Avengers Team");

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
        private void PlanetDeserializer()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Planets));
            using (FileStream fileStream = new FileStream(projectDirectory + @"\InputFiles\planets.xml", FileMode.Open))
            {
                planets = (Planets)serializer.Deserialize(fileStream);
            }
        }
        public void PlanetSelector()
        {
            Console.Clear();
            idSelectedPlanet = 0;
            PlanetDeserializer();
            OptionSelector("Planet", false);
        }
        private void PlanetDetails()
        {
            ColorWriter.SpaceWriteLine("Cyan", planets.Planet[idSelectedPlanet - 1].Name);
            ColorWriter.SpaceWriteLine("DarkGray", "\u2022 " + planets.Planet[idSelectedPlanet - 1].Description);
            ColorWriter.SpaceWrite("Gray", "\u00BB " + "Villain Attack Modifier: ");
            if (planets.Planet[idSelectedPlanet - 1].Modifiers.VillainAttackModifier >= 0)
            {
                ColorWriter.WriteLine("Green", "+" + planets.Planet[idSelectedPlanet - 1].Modifiers.VillainAttackModifier.ToString());
            }
            else
            {
                ColorWriter.WriteLine("Red", planets.Planet[idSelectedPlanet - 1].Modifiers.VillainAttackModifier.ToString());
            }
            ColorWriter.SpaceWrite("Gray", "\u00BB " + "Villain Health Modifier: ");
            if (planets.Planet[idSelectedPlanet - 1].Modifiers.VillainHealthModifier >= 0)
            {
                ColorWriter.WriteLine("Green", "+" + planets.Planet[idSelectedPlanet - 1].Modifiers.VillainHealthModifier.ToString());
            }
            else
            {
                ColorWriter.WriteLine("Red", planets.Planet[idSelectedPlanet - 1].Modifiers.VillainHealthModifier.ToString());
            }
            ColorWriter.SpaceWrite("Gray", "\u00BB " + "Hero Attack Modifier: ");
            if (planets.Planet[idSelectedPlanet - 1].Modifiers.HeroAttackModifier >= 0)
            {
                ColorWriter.WriteLine("Green", "+" + planets.Planet[idSelectedPlanet - 1].Modifiers.HeroAttackModifier.ToString());
            }
            else
            {
                ColorWriter.WriteLine("Red", planets.Planet[idSelectedPlanet - 1].Modifiers.HeroAttackModifier.ToString());
            }
            ColorWriter.SpaceWrite("Gray", "\u00BB " + "Hero Health Modifier: ");
            if (planets.Planet[idSelectedPlanet - 1].Modifiers.HeroHealthModifier >= 0)
            {
                ColorWriter.WriteLine("Green", "+" + planets.Planet[idSelectedPlanet - 1].Modifiers.HeroHealthModifier.ToString());
            }
            else
            {
                ColorWriter.WriteLine("Red", planets.Planet[idSelectedPlanet - 1].Modifiers.HeroHealthModifier.ToString());
            }
        }
        private void CharacterDeserializer()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Characters));
            using (FileStream fileStream = new FileStream(projectDirectory + @"\InputFiles\characters.xml", FileMode.Open))
            {
                characters = (Characters)serializer.Deserialize(fileStream);
            }
        }
        public void VillainSelector()
        {
            Console.Clear();
            idSelectedVillain = 0;
            CharacterDeserializer();
            OptionSelector("Villain", false);
        }
        private void CharacterDetails(byte idSelectedCharacter)
        {
            Console.Clear();
            ColorWriter.SpaceWriteLine("Cyan", characters.Character[idSelectedCharacter - 1].Name);
            ColorWriter.SpaceWriteLine("DarkGray", "\u2022 " + characters.Character[idSelectedCharacter - 1].Description);
            ColorWriter.SpaceWrite("Gray", "\u00BB " + "Attack points: ");
            ColorWriter.Write("Magenta", characters.Character[idSelectedCharacter - 1].Attack.ToString());
            ColorWriter.SpaceWriteLine("Yellow", "(Ap)");
            ColorWriter.SpaceWrite("Gray", "\u00BB " + "Health points: ");
            ColorWriter.Write("Green", characters.Character[idSelectedCharacter - 1].Health.ToString());
            ColorWriter.SpaceWriteLine("Yellow", "(Hp)");
        }
        public void HeroSelector()
        {
            Console.Clear();
            idSelectedHero = 0;
            CharacterDeserializer();
            OptionSelector("Hero", false);
        }
        public void AvangersTeam()
        {
            Console.Clear();
            CharacterDeserializer();
            OptionSelector("Hero", true);
        }
        private void IdToObject(string objectType)
        {
            if (objectType == "Planet")
            {
                foreach (Planet planet in planets.Planet)
                {
                    if (idSelectedPlanet == planet.Id)
                    {
                        fightingPlanet = planet;
                    }
                }
            }
            if (objectType == "Character")
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
            loadingMessageShown = false;
            if (idSelectedPlanet != 0 && idSelectedVillain != 0)
            {
                IdToObject("Planet");
                while (true)
                {
                    Console.Clear();
                    ColorWriter.SpaceWriteLine("Yellow", "Black", "Fight Menu\n");
                    ColorWriter.SpaceWrite("Yellow", "Black", "1");
                    ColorWriter.SpaceWriteLine("Gray", "Villain vs Hero\n");
                    ColorWriter.SpaceWrite("Yellow", "Black", "2");
                    ColorWriter.SpaceWriteLine("Gray", "Villain vs Avengers\n");
                    ColorWriter.SpaceWrite("Yellow", "Select an option: ");
                    byte option = 0;
                    bool errorGiven = false;
                    try
                    {
                        option = byte.Parse(Console.ReadLine());
                    }
                    catch (Exception)
                    {
                        SpecialMessage.ErrorMessage("Wrong type of value given!\n Expected Byte");
                        errorGiven = true;
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
                                SpecialMessage.ErrorMessage("No hero selected!");
                            }
                            goto exit;
                        case 2:
                            if (avengersList.Any())
                            {
                                FightController(2);
                            }
                            else
                            {
                                SpecialMessage.ErrorMessage("Avengers team empty!");
                            }
                            goto exit;
                        default:
                            if (!errorGiven)
                            {
                                SpecialMessage.ErrorMessage("No option of value: " + option + " found!");
                            }
                            break;
                    }
                }
            }
            else
            {
                SpecialMessage.ErrorMessage("Please select a Planet and a Villain!");
            }
        exit:;
            CharacterDeserializer();
            AvengersRejuvenator();
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
            byte whoAttacksFirst = (byte)random.Next(2); //generates either 0 or 1
            if (option == 1)
            {
                FightingInitializer(1);
                while (fightingHero.Health > 0 && fightingVillain.Health > 0)
                {
                    if (whoAttacksFirst == 1)
                    {
                        CharacterVsCharacter(fightingHero, fightingVillain, true);
                    }
                    else
                    {
                        CharacterVsCharacter(fightingVillain, fightingHero, false);
                    }
                }
                if (fightingHero.Health > 0 && fightingVillain.Health <= 0)
                {
                    SpecialMessage.WinMessage(fightingHero.Name + " wins!");
                }
                else
                {
                    SpecialMessage.LoseMessage(fightingVillain.Name + " wins!"); ;
                }
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
                                CharacterVsCharacter(avenger, fightingVillain, true);
                            }
                            else
                            {
                                CharacterVsCharacter(fightingVillain, avenger, false);
                            }
                        }
                    }
                }
                if (AvengersStillAlive() && fightingVillain.Health <= 0)
                {
                    SpecialMessage.WinMessage("The Avengers win!");
                }
                else
                {
                    SpecialMessage.LoseMessage(fightingVillain.Name + " wins!"); ;
                }
            }
        }
        private void CharacterNewStats(Character character)
        {
            ColorWriter.SpaceWriteLine("Cyan", character.Name);
            ColorWriter.SpaceWrite("Gray", "\u00BB Attack = " + character.Attack);
            if (character.IsVillain)
            {
                character.Attack = (short)(character.Attack + fightingPlanet.Modifiers.VillainAttackModifier);
                if (fightingPlanet.Modifiers.VillainAttackModifier >= 0)
                {
                    ColorWriter.Write("Green", "+" + fightingPlanet.Modifiers.VillainAttackModifier);
                }
                else
                {
                    ColorWriter.Write("Red", fightingPlanet.Modifiers.VillainAttackModifier.ToString());
                }
                ColorWriter.Write("Gray", " = ");
            }
            else
            {
                character.Attack = (short)(character.Attack + fightingPlanet.Modifiers.HeroAttackModifier);
                if (fightingPlanet.Modifiers.HeroAttackModifier >= 0)
                {
                    ColorWriter.Write("Green", "+" + fightingPlanet.Modifiers.HeroAttackModifier);
                }
                else
                {
                    ColorWriter.Write("Red", fightingPlanet.Modifiers.HeroAttackModifier.ToString());
                }
                ColorWriter.Write("Gray", " = ");
            }
            ColorWriter.Write("Magenta", character.Attack.ToString());
            ColorWriter.SpaceWriteLine("Yellow", "(Ap)");
            ColorWriter.SpaceWrite("Gray", "\u00BB Health = " + character.Health);
            if (character.IsVillain)
            {
                character.Health = (short)(character.Health + fightingPlanet.Modifiers.VillainHealthModifier);
                if (fightingPlanet.Modifiers.VillainHealthModifier >= 0)
                {
                    ColorWriter.Write("Green", "+" + fightingPlanet.Modifiers.VillainHealthModifier);
                }
                else
                {
                    ColorWriter.Write("Red", fightingPlanet.Modifiers.VillainHealthModifier.ToString());
                }
                ColorWriter.Write("Gray", " = ");
            }
            else
            {
                character.Health = (short)(character.Health + fightingPlanet.Modifiers.HeroHealthModifier);
                if (fightingPlanet.Modifiers.HeroHealthModifier >= 0)
                {
                    ColorWriter.Write("Green", "+" + fightingPlanet.Modifiers.HeroHealthModifier);
                }
                else
                {
                    ColorWriter.Write("Red", fightingPlanet.Modifiers.HeroHealthModifier.ToString());
                }
                ColorWriter.Write("Gray", " = ");
            }
            ColorWriter.Write("Green", character.Health.ToString());
            ColorWriter.SpaceWriteLine("Yellow", "(Hp)");
        }
        private void FightingInitializer(byte option)
        {
            Console.Clear();
            ColorWriter.SpaceWriteLine("Yellow", "Black", "Selected Planet\n");
            PlanetDetails();
            Console.WriteLine();
            ColorWriter.SpaceWriteLine("Yellow", "Black", "Selected Villain\n");
            CharacterNewStats(fightingVillain);
            Console.WriteLine();
            if (option == 1)
            {
                ColorWriter.SpaceWriteLine("Yellow", "Black", "Selected Hero\n");
                CharacterNewStats(fightingHero);
            }
            else
            {
                ColorWriter.SpaceWriteLine("Yellow", "Black", "Selected Avengers\n");
                foreach (Character character in avengersList)
                {
                    CharacterNewStats(character);
                    Console.WriteLine();
                }
            }
            SpecialMessage.WaitingOnEnterMessage("Press ENTER when you are ready to start the fight!");
            SpecialMessage.CountdownMessage("Starting the battle in", 5);
        }
        private void CharacterVsCharacter(Character avatar1, Character avatar2, bool heroAttacksFirst)
        {
            Random random = new Random();
            short damage;
            if (!loadingMessageShown)
            {
                loadingMessageShown = true;
                SpecialMessage.LoadingMessage("Selecting who has the first turn (at random)");
                if (heroAttacksFirst)
                {
                    ColorWriter.SpaceWriteLine("Gray", "The Hero attacks first!\n");
                }
                else
                {
                    ColorWriter.SpaceWriteLine("Gray", "The Villain attacks first!\n");
                }
                System.Threading.Thread.Sleep(1000);
            }
            //avatar1 attacks avatar2
            if (avatar2.Health > 0 && avatar1.Health > 0)
            {
                damage = (short)(avatar1.Attack * (random.Next(60, 101) / 100f));
                ColorWriter.SpaceWrite("Cyan", avatar1.Name);
                ColorWriter.SpaceWrite("Gray", "attacks");
                ColorWriter.SpaceWrite("Yellow", avatar2.Name);
                ColorWriter.SpaceWrite("Gray", "and deals");
                ColorWriter.SpaceWrite("Magenta", damage.ToString());
                ColorWriter.SpaceWriteLine("Gray", "points of damage");
                avatar2.Health = (short)(avatar2.Health - damage);
                ColorWriter.Write("Yellow", "\n " + avatar2.Name);
                ColorWriter.SpaceWrite("Gray", "Hp =");
                if (avatar2.Health > 0)
                {
                    ColorWriter.SpaceWriteLine("Green", avatar2.Health + "\n");
                }
                else
                {
                    ColorWriter.SpaceWriteLine("Red", avatar2.Health + "\n");
                }
            }
            if (avatar2.Health <= 0)
            {
                SpecialMessage.DefeatMessage(avatar2.Name + " was defeated!");
            }
            else
            {
                System.Threading.Thread.Sleep(2000);
            }
            //avatar2 attacks avatar1
            if (avatar2.Health > 0 && avatar1.Health > 0)
            {
                damage = (short)(avatar2.Attack * (random.Next(60, 101) / 100f));
                ColorWriter.SpaceWrite("Yellow", avatar2.Name);
                ColorWriter.SpaceWrite("Gray", "attacks");
                ColorWriter.SpaceWrite("Cyan", avatar1.Name);
                ColorWriter.SpaceWrite("Gray", "and deals");
                ColorWriter.SpaceWrite("Magenta", damage.ToString());
                ColorWriter.SpaceWriteLine("Gray", "points of damage");
                avatar1.Health = (short)(avatar1.Health - damage);
                ColorWriter.Write("Cyan", "\n " + avatar1.Name);
                ColorWriter.SpaceWrite("Gray", "Hp =");
                if (avatar1.Health > 0)
                {
                    ColorWriter.SpaceWriteLine("Green", avatar1.Health + "\n");
                }
                else
                {
                    ColorWriter.SpaceWriteLine("Red", avatar1.Health + "\n");
                }
            }
            if (avatar1.Health <= 0)
            {
                SpecialMessage.DefeatMessage(avatar1.Name + " was defeated!");
            }
            else
            {
                System.Threading.Thread.Sleep(2000);
            }
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
                SpecialMessage.SuccessMessage("Hero added succesfully!");
            }
            else
            {
                SpecialMessage.ErrorMessage("Selected Hero is already part of the Avengers Team!");
            }
        }
        private void ShowAvengersTeam()
        {
            Console.WriteLine();
            if (!avengersList.Any())
            {
                SpecialMessage.ErrorMessage("There are no Heroes in the Avengers Team!");
            }
            else
            {
                byte i = 1;
                foreach (Character character in avengersList)
                {
                    ColorWriter.SpaceWrite("Yellow", i.ToString() + ")");
                    ColorWriter.SpaceWriteLine("Cyan", character.Name);
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
        private void AvengersRejuvenator()
        {
            foreach (Character avenger in avengersList)
            {
                foreach (Character character in characters.Character)
                {
                    if (character.Id == avenger.Id)
                    {
                        avenger.Health = character.Health;
                        avenger.Attack = character.Attack;
                    }
                }

            }

        }
    }
}
