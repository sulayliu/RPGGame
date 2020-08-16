using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class Game
    {
        public string Player { get; set; }
        public int NumberOfWins { get; set; }
        public int NumberOfLoses { get; set; }
        public List<Hero> Heroes { get; set; }
        public List<Monster> Monsters { get; set; }
        public List<Weapon> Weapons { get; set; }
        public List<Armor> Armors { get; set; }
        public List<Fight> Fights { get; set; }
        public Game()
        {
            Heroes = new List<Hero>();
            Monsters = new List<Monster>();
            Weapons = new List<Weapon>();
            Armors = new List<Armor>();
            Fights = new List<Fight>();
        }
        public Game(List<Hero> heroes, List<Monster> monsters, List<Weapon> weapons, List<Armor> armors)
        {
            Heroes = heroes;
            Monsters = monsters;
            Weapons = weapons;
            Armors = armors;
            Fights = new List<Fight>();

        }
        public void Start()
        {
            try
            {
                Console.WriteLine("Welcome to the game, please input your name...");

                Player = Console.ReadLine();
                Console.WriteLine("Welcome {0}, the game is starting...", Player);

                // Choose hero module.
                Console.WriteLine("---------------------------------------");
                Console.WriteLine();
                if (Heroes.Count == 0)
                {
                    throw new Exception("No hero in the game, cannot play, quit the game...");
                }

                Console.WriteLine("Choose one hero or get a random one.");
                Console.WriteLine("Press 0 to quit the Game.");

                Hero chosenHero = ChooseHero();
                Console.WriteLine("---------------------------------------");
                Console.WriteLine();

                bool stillGame = true;
                while (stillGame)
                {
                    // Choose monster module
                    if (Monsters.Count == 0)
                    {
                        throw new Exception("No monster in the game, cannot play, quit the game...");
                    }

                    Console.WriteLine("Choose one monster or get a random one.");
                    Console.WriteLine("Press 0 to quit the Game.");

                    Monster chosenMonster = ChooseMonster();

                    Console.WriteLine("---------------------------------------");
                    Console.WriteLine();

                    // Main menu to switch statements.
                    bool inMenu = true;
                    while (inMenu)
                    {
                        Console.WriteLine("Press 0 to quit the Game.");
                        Console.WriteLine("Press 1 to show the statistics.");
                        Console.WriteLine("Press 2 to show the inventory.");
                        Console.WriteLine("Press 3 to select the weapons.");
                        Console.WriteLine("Press 4 to select the armors.");
                        Console.WriteLine("Press any key to FIGHT.");

                        char statValue = Console.ReadKey().KeyChar;
                        Console.WriteLine();
                        try
                        {
                            if (int.Parse(statValue.ToString()) == 0)
                            {
                                throw new Exception("You quit the Game...");
                            }
                            else if (int.Parse(statValue.ToString()) == 1)
                            {
                                chosenHero.ShowStats();
                                inMenu = true;
                            }
                            else if (int.Parse(statValue.ToString()) == 2)
                            {
                                chosenHero.ShowInventory();
                                inMenu = true;
                            }
                            else if (int.Parse(statValue.ToString()) == 3)
                            {
                                // Select weapons.
                                Console.WriteLine("Choose the weapons for the hero...");
                                GetWeapons(chosenHero);
                                inMenu = true;
                            }
                            else if (int.Parse(statValue.ToString()) == 4)
                            {
                                // Select armors.
                                Console.WriteLine("Choose the armors for the hero...");
                                GetArmors(chosenHero);
                                inMenu = true;
                            }
                            else
                            {
                                Console.WriteLine("Quiting from the menu, you can fight now.");
                                inMenu = false;
                            }
                        }
                        catch
                        {
                            Console.WriteLine("Quiting from the menu, you can fight now.");
                            inMenu = false;
                        }
                    }

                    // Fight module.
                    Console.WriteLine("You are joining a fight now...");
                    Fight fight = new Fight(chosenHero, chosenMonster);
                    Fights.Add(fight);
                    fight.FightStart();
                    Console.WriteLine("----------------------------");
                    Console.WriteLine();

                    Console.WriteLine("Do you want to start a new game?");
                    Console.WriteLine("Press Y to start, any other key to quit the game.");
                    char input = Console.ReadKey().KeyChar;
                    Console.WriteLine();
                    if (input == 'Y' || input == 'y')
                    {
                        stillGame = true;
                    }
                    else
                    {
                        stillGame = false;
                    }
                }
                Console.WriteLine("You are quiting the game, bye!");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public Hero ChooseHero()
        {
            Console.WriteLine("The heroes are in the following list...");
            Console.WriteLine("---------------------------------------");
            foreach (Hero hero in Heroes)
            {
                Console.WriteLine(hero);
            }
            Console.WriteLine("Please input the NUMBER of the hero to choose, or press ANY KEY to get a random one.");

            int heroID = 0;
            string inputValue = Console.ReadLine();

            try
            {
                heroID = int.Parse(inputValue);

                if (heroID < 0 || heroID > Heroes.Count)
                {
                    throw new Exception();
                }
            }
            catch
            {
                Console.WriteLine("Get a random hero...");
                Random ran = new Random();
                heroID = ran.Next(1, Heroes.Count);
            }

            if (heroID == 0)
            {
                throw new Exception("You quit the game.");
            }

            Console.WriteLine("Your hero is number {0}", heroID);
            return GetHeroByID(heroID);
        }
        public Monster ChooseMonster()
        {
            Console.WriteLine("The monsters are in the following list...");
            Console.WriteLine("---------------------------------------");
            int topStrength = 0;
            Monster topDamageMonster = Monsters[0];
            int topDefense = 0;
            Monster topDefenseMonster = Monsters[0];
            foreach (Monster monster in Monsters)
            {
                // Get the top damage monster.
                if (monster.Strength > topStrength)
                {
                    topStrength = monster.Strength;
                    topDamageMonster = monster;
                }

                // Get the top defense monster.
                if (monster.Defense > topDefense)
                {
                    topDefense = monster.Defense;
                    topDefenseMonster = monster;
                }

                Console.WriteLine(monster);
            }
            Console.WriteLine("------------------------------------");
            Console.WriteLine();

            Console.WriteLine("Do you want to choose the strongest monster to play?");
            Console.WriteLine("Press Y to choose, any other key to enter normal selection.");

            char choosestrongest = Console.ReadKey().KeyChar;
            Console.WriteLine();

            if (choosestrongest == 'Y' || choosestrongest == 'y')
            {
                bool getMonster = false;
                while (!getMonster)
                {
                    Console.WriteLine("{0}, it is the top damage monster, press 1 to choose", topDamageMonster);
                    Console.WriteLine("{0}, it is the top defense monster, press 2 to choose", topDefenseMonster);

                    char choose = Console.ReadKey().KeyChar;
                    Console.WriteLine();
                    if (choose == '1')
                    {
                        Console.WriteLine("You chose top damage monster");
                        Console.WriteLine(topDamageMonster);
                        return topDamageMonster;
                    }
                    else if (choose == '2')
                    {
                        Console.WriteLine("You chose top defense monster");
                        Console.WriteLine(topDefenseMonster);
                        return topDefenseMonster;
                    }
                    else
                    {
                        Console.WriteLine("Incorrect input try again.");
                        getMonster = false;
                    }
                }
            }

            Console.WriteLine("The monsters are listed above.");
            Console.WriteLine("Please input the NUMBER of the monster to choose, or press ANY KEY to get a random one.");

            int monsterID = 0;
            string inputValue = Console.ReadLine();

            try
            {
                monsterID = int.Parse(inputValue);

                if (monsterID < 0 || monsterID > Monsters.Count)
                {
                    throw new Exception();
                }
            }
            catch
            {
                Console.WriteLine("Get a random monster...");
                Random ran = new Random();
                monsterID = ran.Next(1, Monsters.Count);
            }

            if (monsterID == 0)
            {
                throw new Exception("You quit the game.");
            }

            Console.WriteLine("The monster is number {0}.", monsterID);
            return GetMonsterByID(monsterID);
        }
        public void GetWeapons(Hero hero)
        {
            Console.WriteLine("The weapons are in the following list...");
            Console.WriteLine("---------------------------------------");
            foreach (Weapon weapon in Weapons)
            {
                Console.WriteLine(weapon);
            }

            bool finish = false;

            while (!finish)
            {
                Console.WriteLine("Get the weapon by the NUMBER, press any key to quit selecting weapons.");

                string inputValue = Console.ReadLine();
                Console.WriteLine();
                try
                {
                    int weaponID = int.Parse(inputValue);
                    Console.WriteLine();

                    Weapon selectedWeapon = GetWeaponByID(weaponID);
                    Console.WriteLine("You select the {0}", selectedWeapon.Name);

                    if (weaponID > 0 && weaponID <= Weapons.Count)
                    {
                        if (hero.EquippedWeapon == null)
                        {
                            hero.EquippedWeapon = selectedWeapon;
                        }
                        else
                        {
                            hero.WeaponsBag.Add(hero.EquippedWeapon);
                            hero.EquippedWeapon = selectedWeapon;
                        }

                        finish = false;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                catch
                {
                    Console.WriteLine("Quit selecting weapons...");
                    Console.WriteLine("--------------------------------------");
                    Console.WriteLine();
                    finish = true;
                }
            }
        }
        public void GetArmors(Hero hero)
        {
            Console.WriteLine("The armors are in the following list...");
            Console.WriteLine("---------------------------------------");
            foreach (Armor armor in Armors)
            {
                Console.WriteLine(armor);
            }

            bool finish = false;

            while (!finish)
            {
                Console.WriteLine("Get the armor by the NUMBER, press any key to quit selecting armors.");

                string inputValue = Console.ReadLine();
                Console.WriteLine();

                try
                {
                    int armorID = int.Parse(inputValue);

                    if (armorID > 0 && armorID <= Armors.Count)
                    {
                        if (hero.EquippedArmor == null)
                        {
                            hero.EquippedArmor = GetArmorByID(armorID);
                        }
                        else
                        {
                            hero.ArmorsBag.Add(hero.EquippedArmor);
                            hero.EquippedArmor = GetArmorByID(armorID);
                        }

                        finish = false;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                catch
                {
                    Console.WriteLine("Quit selecting armors...");
                    Console.WriteLine("--------------------------------------");
                    Console.WriteLine();
                    finish = true;
                }
            }
        }
        public Hero GetHeroByID(int heroID)
        {
            foreach (Hero hero in Heroes)
            {
                if (hero.ID == heroID)
                {
                    return hero;
                }
            }
            throw new Exception("Can't find the hero");
        }
        public Monster GetMonsterByID(int monsterID)
        {
            foreach (Monster monster in Monsters)
            {
                if (monster.ID == monsterID)
                {
                    return monster;
                }
            }
            throw new Exception("Can't find the monster");
        }
        public Weapon GetWeaponByID(int weaponID)
        {
            foreach (Weapon weapon in Weapons)
            {
                if (weapon.ID == weaponID)
                {
                    return weapon;
                }
            }
            throw new Exception("Can't find the weapon");
        }
        public Armor GetArmorByID(int armorID)
        {
            foreach (Armor armor in Armors)
            {
                if (armor.ID == armorID)
                {
                    return armor;
                }
            }
            throw new Exception("Can't find the armor");
        }
        public int checkInputValue(string inputValue, int maxNum)
        {
            // Check the inputValue until it is a number.
            bool checkIfInt = false;
            while (!checkIfInt)
            {
                try
                {
                    int.Parse(inputValue);
                    checkIfInt = true;
                }
                catch
                {
                    Console.WriteLine("Your input is not a number, please choose again.");
                    checkIfInt = false;
                }
            }

            while (int.Parse(inputValue) < 0 || int.Parse(inputValue) > maxNum)
            {
                Console.WriteLine("Sorry, the number you inputed is out of range, please choose again.");
                inputValue = Console.ReadLine();
            }
            return int.Parse(inputValue);
        }
    }
    class Hero
    {
        public static int Counter { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
        public int wins { get; set; }
        public int loses { get; set; }
        public int Strength { get; set; }
        public int Defense { get; set; }
        public int OriginalHealth { get; set; }
        public int CurrentHealth { get; set; }
        public int Coins { get; set; }
        public Weapon EquippedWeapon { get; set; }
        public Armor EquippedArmor { get; set; }
        public List<Weapon> WeaponsBag { get; set; }
        public List<Armor> ArmorsBag { get; set; }
        public Hero(string name, int strength, int defense)
        {
            Name = name;
            ID = ++Counter;
            Strength = strength;
            Defense = defense;
            OriginalHealth = int.Parse(ConfigurationManager.AppSettings.Get("OriginalHealthOfHero"));
            CurrentHealth = OriginalHealth;
            Coins = 10;
            ArmorsBag = new List<Armor>();
            WeaponsBag = new List<Weapon>();
        }
        public void ShowStats()
        {
            Console.WriteLine("The current status of the hero:");
            Console.WriteLine("The hero name is {0}, the strength is {1}, the defense is {2}", Name, Strength, Defense);
            Console.WriteLine("Your current health is {0}", CurrentHealth);
            Console.WriteLine("he current equipments of the hero:");
            if (EquippedWeapon == null)
            {
                Console.WriteLine("No weapon of the hero.");
            }
            else
            {
                Console.WriteLine(EquippedWeapon);
            }

            if (EquippedArmor == null)
            {
                Console.WriteLine("No weapon of the hero.");
            }
            else
            {
                Console.WriteLine(EquippedArmor);
            }
            Console.WriteLine("  The hero has won {0} times in the game, and lost {1} in the game.", wins, loses);
            Console.WriteLine("  The hero has {0} coins to use.", Coins);
            Console.WriteLine("------------------------------------------------------------------");
            Console.WriteLine();
        }
        public void ShowInventory()
        {
            Console.WriteLine("The current equipments of the hero:");
            if (EquippedWeapon == null)
            {
                Console.WriteLine("No equiped weapon of the hero.");
                Console.WriteLine("------------------------------");
            }
            else
            {
                Console.WriteLine(EquippedWeapon);
            }
            if (WeaponsBag.Count == 0)
            {
                Console.WriteLine("You don't have weapon in your bag.");
                Console.WriteLine("------------------------------");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("You have the following weapons in your bag.");
                foreach (Weapon weapon in WeaponsBag)
                {
                    int num = WeaponsBag.IndexOf(weapon) + 1;
                    Console.WriteLine("Number {0}:", num);
                    Console.WriteLine(weapon);
                    Console.WriteLine();
                }
            }

            if (EquippedArmor == null)
            {
                Console.WriteLine("No Armor of the hero.");
                Console.WriteLine("------------------------------");
            }
            else
            {
                Console.WriteLine(EquippedArmor);
            }

            if (ArmorsBag.Count == 0)
            {
                Console.WriteLine("You don't have armor in your bag.");
                Console.WriteLine("------------------------------");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("You have the following weapons in your bag.");

                foreach (Armor armor in ArmorsBag)
                {
                    int num = ArmorsBag.IndexOf(armor) + 1;
                    Console.WriteLine("Number {0}:", num);
                    Console.WriteLine(armor);
                    Console.WriteLine();
                }
            }
            Console.WriteLine("----------------------------");
            Console.WriteLine();
        }
        public void GetWeapon()
        {
            if (WeaponsBag.Count == 0)
            {
                Console.WriteLine("You don't have weapon in your bag.");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("You have the following weapons in your bag.");

                foreach (Weapon weapon in WeaponsBag)
                {
                    int num = WeaponsBag.IndexOf(weapon) + 1;
                    Console.WriteLine("Number {0}:", num);
                    Console.WriteLine(weapon);
                }
                Console.WriteLine("Press the number to choose the weapon:");

                string inputValue = Console.ReadLine();
                Console.WriteLine();

                int inputresult = checkInputValue(inputValue, WeaponsBag.Count);
                Weapon chosenWeapon = WeaponsBag[inputresult - 1];
                Console.WriteLine("You choose the number of {0} weapon in your bag.", inputresult);
                Console.WriteLine(chosenWeapon);
                Console.WriteLine("-----------------------------------");
                Console.WriteLine();
                if (EquippedWeapon == null)
                {
                    EquippedWeapon = chosenWeapon;
                }
                else
                {
                    WeaponsBag.Add(EquippedWeapon);
                    EquippedWeapon = chosenWeapon;
                    WeaponsBag.Remove(chosenWeapon);
                }
            }
        }
        public void GetArmor()
        {
            if (ArmorsBag.Count == 0)
            {
                Console.WriteLine("You don't have armor in your bag.");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("You have the following armors in your bag.");

                foreach (Armor armor in ArmorsBag)
                {
                    int num = ArmorsBag.IndexOf(armor) + 1;
                    Console.WriteLine("Number {0}:", num);
                    Console.WriteLine(armor);
                }
                Console.WriteLine("Press the number to choose the armor:");

                string inputValue = Console.ReadLine();
                int inputresult = checkInputValue(inputValue, WeaponsBag.Count);

                Armor chosenArmor = ArmorsBag[inputresult - 1];
                Console.WriteLine("You choose the number of {0} armor in your bag.", inputresult);
                Console.WriteLine(chosenArmor);
                Console.WriteLine("---------------------------------");
                Console.WriteLine();
                if (EquippedArmor == null)
                {
                    EquippedArmor = chosenArmor;
                }
                else
                {
                    ArmorsBag.Add(EquippedArmor);
                    EquippedArmor = chosenArmor;
                    ArmorsBag.Remove(chosenArmor);
                }

            }
        }
        public void BuyHealth()
        {

            if (Coins == 0)
            {
                Console.WriteLine("Sorry, you have 0 coin, you can't buy health.");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("You have {0} coins.", Coins);
                int lostHealth = OriginalHealth - CurrentHealth;
                Console.WriteLine("You lost {0} health", lostHealth);
                Console.WriteLine("Press 0 to quit buying health...");
                string input;
                bool success = false;
                while (!success)
                {
                    if (lostHealth > Coins)
                    {
                        Console.WriteLine("You can buy {0} health, cost you {0} coins.", Coins);
                        Console.WriteLine("Input the number of health you want to buy, or press 0 to quit buying health... ");

                        input = Console.ReadLine();
                        int inputvalue = checkInputValue(input, Coins);

                        if (inputvalue == 0)
                        {
                            Console.WriteLine("You quit buying health.");
                            success = true;
                        }
                        else
                        {
                            Coins -= inputvalue;
                            CurrentHealth += inputvalue;
                            Console.WriteLine("You bought {0} health.", inputvalue);
                            success = true;
                        }
                    }
                    else
                    {
                        Console.WriteLine("You can buy {0} health to full health, only cost you {0} coins.", lostHealth);
                        Console.WriteLine("Input the number of health you want to buy, or press 0 to quit buying health... ");
                        input = Console.ReadLine();
                        Console.WriteLine();

                        int inputvalue = checkInputValue(input, lostHealth);

                        if (inputvalue == 0)
                        {
                            Console.WriteLine("You quit buying health.");
                            success = true;
                        }
                        else
                        {
                            Coins -= inputvalue;
                            CurrentHealth += inputvalue;
                            Console.WriteLine("You bought {0} health.", inputvalue);
                            success = true;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// This function is to check the input value whether it is in the range of the indexes of the list
        /// </summary>
        /// <param name="inputValue">this is the value that use input</param>
        /// <param name="maxNum">the max number of the range, usually it is the count of a list</param>
        /// <returns>it will return the int type of the input, sothat we can use it to catch the value in a list</returns>
        public int checkInputValue(string inputValue, int maxNum)
        {
            // Check the inputValue until it is a number.
            bool checkIfInt = false;
            while (!checkIfInt)
            {
                try
                {
                    while (int.Parse(inputValue) < 0 || int.Parse(inputValue) > maxNum)
                    {
                        Console.WriteLine("Sorry, the number you inputed is out of range, please choose again.");
                        inputValue = Console.ReadLine();
                    }
                    checkIfInt = true;
                }
                catch
                {
                    Console.WriteLine("Your input is not a number, please choose again.");
                    inputValue = Console.ReadLine();
                    checkIfInt = false;
                }
            }
            return int.Parse(inputValue);
        }
        public override string ToString()
        {
            return string.Format("The hero number is {0} and the name is {1}, with the strength of {2}, and defense of {3}", ID, Name, Strength, Defense);
        }
    }
    class Monster
    {
        public static int Counter { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
        public int Strength { get; set; }
        public int Defense { get; set; }
        public int OriginalHealth { get; set; }
        public int CurrentHealth { get; set; }
        public int Coins { get; set; }
        public Monster(string name, int strength, int defense, int originalHealth)
        {
            ID = ++Counter;
            Name = name;
            Strength = strength;
            Defense = defense;
            OriginalHealth = originalHealth;
            CurrentHealth = originalHealth;
            Coins = 20;
        }
        public override string ToString()
        {
            return string.Format("The monster number is {0} and the name is {1}, with the strength of {2}, and defense of {3}", ID, Name, Strength, Defense);
        }
    }
    class Weapon
    {
        public static int Counter { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
        public int Power { get; set; }
        public Weapon(string name, int power)
        {
            ID = ++Counter;
            Name = name;
            Power = power;
        }
        public override string ToString()
        {
            return string.Format("The weapon number is {0}, name is {1}, with the power of {2}.", ID, Name, Power);
        }
    }
    class Armor
    {
        public static int Counter { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
        public int Power { get; set; }
        public Armor(string name, int power)
        {
            ID = ++Counter;
            Name = name;
            Power = power;
        }
        public override string ToString()
        {
            return string.Format("The armor number is {0}, name is {1}, with the power of {2}.", ID, Name, Power);
        }
    }
    enum FightResult
    {
        Win,
        Lose
    }
    class Fight
    {
        public Hero Hero { get; set; }
        public Monster Monster { get; set; }
        public FightResult Result { get; set; }
        public Fight(Hero hero, Monster monster)
        {
            Hero = hero;
            Monster = monster;
        }

        public void FightStart()
        {
            try
            {
                Console.WriteLine("The fight is starting...");
                while (Hero.CurrentHealth > 0 && Monster.CurrentHealth > 0)
                {
                    HeroFight();
                    if (Monster.CurrentHealth > 0)
                    {
                        MonsterFight();
                    }

                }

                if (Hero.CurrentHealth <= 0)
                {
                    Console.WriteLine("Sad! You lost the fight.....");
                    Lose();
                }
                else if (Monster.CurrentHealth <= 0)
                {
                    Console.WriteLine("Great! You won the fight...");
                    Win();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void HeroFight()
        {
            bool fight = false;
            while (!fight)
            {
                Console.WriteLine("Hero turn");
                Console.WriteLine("-----------------------------");
                Console.WriteLine();
                Console.WriteLine("Choose the option you want: ");
                Console.WriteLine("Press 1 to show status.");
                Console.WriteLine("Press 2 to show inventory.");
                Console.WriteLine("Press 3 to choose the weapon.");
                Console.WriteLine("Press 4 to choose the armor.");
                Console.WriteLine("Press 5 to buy health.");
                Console.WriteLine("Press 0 to quit the fight.");
                Console.WriteLine("Press any key to fight directly.");
                char inputValue = Console.ReadKey().KeyChar;
                Console.WriteLine();

                if (inputValue == '0')
                {
                    Hero.CurrentHealth = Hero.OriginalHealth;
                    Monster.CurrentHealth = Monster.OriginalHealth;
                    throw new Exception("You quit the fight.");
                }
                else if (inputValue == '1')
                {
                    Hero.ShowStats();

                    fight = false;
                }
                else if (inputValue == '2')
                {
                    Hero.ShowInventory();

                    fight = false;
                }
                else if (inputValue == '3')
                {
                    Hero.GetWeapon();

                    fight = false;
                }
                else if (inputValue == '4')
                {
                    Hero.GetArmor();
                    fight = false;
                }
                else if (inputValue == '5')
                {
                    Hero.BuyHealth();
                }
                else
                {
                    fight = true;
                }
            }
            int lostHealth = 0;
            if (Hero.EquippedWeapon == null)
            {
                lostHealth = Hero.Strength - Monster.Defense;
            }
            else
            {
                lostHealth = Hero.Strength + Hero.EquippedWeapon.Power - Monster.Defense;
            }

            if (lostHealth <= 0) lostHealth = 0;
            Monster.CurrentHealth -= lostHealth;

            Console.WriteLine();
            Console.WriteLine("Your attack made the monstor lose {0} health.", lostHealth);
            Console.WriteLine();
        }
        public void MonsterFight()
        {
            Console.WriteLine("Monster turn:");
            Console.WriteLine("-----------------------------");
            int lostHealth = 0;
            if (Hero.EquippedArmor == null)
            {
                lostHealth = Monster.Strength - Hero.Defense;
            }
            else
            {
                lostHealth = Monster.Strength - (Hero.Defense + Hero.EquippedArmor.Power);
            }
            if (lostHealth <= 0) lostHealth = 0;
            Hero.CurrentHealth -= lostHealth;
            Console.WriteLine("The attack of the monstor made you lose {0} health.", lostHealth);
            Console.WriteLine();
        }
        public void Win()
        {
            Result = FightResult.Win;
            Hero.wins++;
            Hero.Coins += Monster.Coins;
            Hero.CurrentHealth = Hero.OriginalHealth;
            Monster.CurrentHealth = Monster.OriginalHealth;
            Console.WriteLine("You got {0} coins!", Monster.Coins);
        }
        public void Lose()
        {
            Result = FightResult.Lose;
            Hero.loses++;
            Hero.CurrentHealth = Hero.OriginalHealth;
            Monster.CurrentHealth = Monster.OriginalHealth;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Hero hero1 = new Hero("hero1", 6, 6);
            Hero hero2 = new Hero("hero2", 7, 8);
            Hero hero3 = new Hero("hero3", 8, 7);
            Hero hero4 = new Hero("hero4", 5, 5);
            Hero hero5 = new Hero("hero5", 9, 4);
            List<Hero> heroes = new List<Hero>() { hero1, hero2, hero3, hero4, hero5 };

            Monster monster1 = new Monster("monster1", 10, 10, 100);
            Monster monster2 = new Monster("monster2", 12, 7, 100);
            Monster monster3 = new Monster("monster3", 13, 5, 100);
            Monster monster4 = new Monster("monster4", 11, 6, 100);
            Monster monster5 = new Monster("monster5", 15, 8, 100);
            List<Monster> monsters = new List<Monster>() { monster1, monster2, monster3, monster4, monster5 };

            Weapon Sword = new Weapon("Sword", 10);
            Weapon Dagger = new Weapon("Dagger", 6);
            Weapon Axe = new Weapon("Axe", 10);
            Weapon Bow = new Weapon("Bow", 9);
            List<Weapon> weapons = new List<Weapon>() { Sword, Dagger, Axe, Bow };

            Armor goldenArmor = new Armor("GoldenArmor", 5);
            Armor ironArmor = new Armor("IronArmor", 3);
            Armor leatherArmor = new Armor("LeatherArmor", 1);
            List<Armor> armors = new List<Armor>() { goldenArmor, ironArmor, leatherArmor };

            Game game = new Game(heroes, monsters, weapons, armors);
            game.Start();
            Console.Read();
        }
    }
}
