using System.Text;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading;

namespace VSProjectСsharp
{

    abstract class FunctionHelper
    {

        public static int randomNum(Random random, int min, int max)
        {
            return random.Next(min, max + 1);
        }

        //public static void clickContinue() {
        //     bool x;
        //     Console.WriteLine("\nНажміть Enter або Space щоб продовжити\n");
        //     while (x = true) {
        //         int key = _getch();
        //         if () {
        //             x = false;
        //             break;
        //         }
        //     }
        // } ///////

        public static void progressBar(int sleep, string saveLoadBoost, bool boost, int boostDiapazon)
        {
            Random random = new Random();
            Console.WriteLine("\n");
            Console.WriteLine($"                   {saveLoadBoost}...");
            Console.WriteLine("    ");
            Console.ForegroundColor = ConsoleColor.Green;
            if (boost == true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                string list = "qwertyuiopasdfghjklzxcvbnm QWERTYUIOPASDFGHJKLZXCVBNM !@#$%^&* ";
                int count = list.Count();

                for (int i = 0; i < boostDiapazon; i++)
                {
                    int a = randomNum(random, 0, count - 1);
                    char l = list[a];

                    Console.Write((char)(randomNum(random, 0, 32767) % (255 - 47) + 48) + ' ');//hack pentagon data
                }
                Console.WriteLine("Loading welldone");
                Thread.Sleep(200);
                Console.Clear();
            }
            else
            {
                for (int i = 0; i < 40; i++)
                {
                    Thread.Sleep(sleep);
                    Console.Write("|");
                }
                Console.Clear();
            }

        }
    };


    class Program
    {

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;
            Random randNum = new Random();

            FunctionHelper.progressBar(100, "Loading", false, 0);// progressBar(0, "Loading", true, 80000); see in fullscreen
            ManageData manageData = new ManageData();

            Player player = null;
            Engine engine = new Engine();
            Monsters monster = null;
            Event ev = new Event(engine);
            SkillInterface skills = null;

            int choiceLoad = 0;
            int fraction;
            string name = "";
            int mesto = 0;
            int choice = 0;

            Console.WriteLine("Ласкаво Просимо у SurvivalMoon");
            Console.WriteLine("Якщо ви уже грали можете загрузить прогрес \n1)Загрузить\n2)Все з нуля");
            choiceLoad = Convert.ToInt16(Console.ReadLine());

            if (choiceLoad == 1)
            {
                //player = manageData.loadPlayer();
                if (player == null)
                {
                    //FunctionHelper.clickContinue();
                    Console.Clear();

                    Console.WriteLine("Як ви назвете свого героя?");
                    name = Convert.ToString(Console.ReadLine());

                    Console.WriteLine("Яку ви виберете фракцію?");
                    Console.WriteLine("1)Танк + до блок урона\n2)Варвар + до урона\n3)Розбійник + до уклонению");
                    fraction = Convert.ToInt16(Console.ReadLine());


                    if (fraction < 1 || fraction > 3)
                    {
                        Console.WriteLine("Ошибка Неправильно веддені дані ");
                        return;
                    }
                    player = engine.createPlayer(name, fraction);
                }

                //FunctionHelper.clickContinue();
                Console.Clear();
            }
            else if (choiceLoad == 2)
            {
                Console.WriteLine("Як ви назвете свого героя?");
                name = Convert.ToString(Console.ReadLine());

                Console.WriteLine("Яку ви виберете фракцію?");
                Console.WriteLine("1)Танк + до блок урона\n2)Варвар + до урону\n3)Розбійник + до уклонению");
                fraction = Convert.ToInt16(Console.ReadLine());

                if (fraction < 1 || fraction > 3)
                {
                    Console.WriteLine("Ошибка Неправильно веддені дані ");
                    return;
                }
                player = engine.createPlayer(name, fraction);
            }
            else
            {
                Console.WriteLine("Помилка 1253");
                return;
            }


            player.addSkill(skills);
            bool x = true;
            while (x == true)
            {
                Console.WriteLine("1)В подорож\n2)Магазин\n3)Показати статистику героя\n4)Вийти з Ігри");// вибір
                choice = Convert.ToInt16(Console.ReadLine());

                if (choice == 1)
                {
                    Console.WriteLine("1)Піти в Ліс\n2)Піти в Заброшку\n3)Піти в Місто\n4)Піти в Печери\n5)Піти в Катакомби Босс(Важко)");
                    mesto = Convert.ToInt16(Console.ReadLine());

                    if (mesto < 1 || mesto > 5)
                    { // перевіряє
                        Console.WriteLine("Error 404");
                        continue;
                    }

                    monster = engine.generateMonster(player.getLevel(), mesto);
                    ev.distributeFight(mesto, player, monster, skills);
                    engine.converterXP(player);
                }
                else if (choice == 2)
                {
                    ev.shop(player);
                }
                else if (choice == 3)
                {
                    Console.Clear();
                    player.showStaticPlayer();
                    Console.WriteLine();
                    player.showSkill();
                    //FunctionHelper.clickContinue();
                    Console.Clear();
                }
                else if (choice == 4)
                {
                    int choiceSave;
                    Console.WriteLine("Хочете сохранить прогрес?\n 1)ДА\n2)НЕ");
                    choiceSave = Convert.ToInt16(Console.ReadLine());
                    if (choiceSave == 1)
                    {
                        //manageData.savePlayer(player);
                        FunctionHelper.progressBar(0, "Saving", true, 65000);
                        Console.WriteLine("Ігра успішно збережена");
                    }

                    return;
                }

                FunctionHelper.progressBar(30, "Loading", false, 0);
                Console.Clear();

            }


        }




    }

    class BasePerson
    {
        protected int hp = 0;
        protected int hpMax = 0;
        protected int energy = 0;
        protected int level = 0;
        protected string name = "";

        public BasePerson(string name, int level, int energy, int hp)
        {
            this.level = level;
            this.energy = energy;
            this.hp = hp;
            this.hpMax = hp;
            this.name = name;
        }

        public int getLevel() { return level; }
        public void setLevel(int valueLevel) { level = valueLevel; }

        public int getEnergy() { return energy; }
        public void setEnergy(int valueEnergy) { energy = valueEnergy; }

        public int getHp() { return hp; }
        public void setHp(int valueHp)
        {
            if (valueHp > this.hpMax)
            {
                this.hp = this.hpMax;
            }
            else
            {
                this.hp = valueHp;
            }
        }
    };

    class Items
    {
        protected string name = "";
        protected int price;

        public Items(string name, int price)
        {
            this.name = name;
            this.price = price;
        }

        public string getName() { return name; }
        public void setName(string valueName) { name = valueName; }

        public int getPrice() { return price; }
        public void setPrice(int valuePrice) { price = valuePrice; }

    }

    class Armor : Items
    {
        private int protection;
        public Armor(string name, int protection, int price) : base(name, price)
        {
            this.protection = protection;
            this.price = this.protection * 150;
        }

        public int getProtection() { return protection; }
        public void setProtection(int valueProtection) { protection = valueProtection; }
        ~Armor()
        {
        }
    }

    class Weapon : Items
    {

        private int damage;
        public Weapon(string name, int damage, int price) : base(name, price)
        {
            this.damage = damage;
            this.price = this.damage * 200;
        }

        public int getDamage() { return damage; }
        public void setDamage(int valueDamage) { damage = valueDamage; }

        ~Weapon()
        {

        }
    }

    interface SkillInterface
    {
        int use(int damage);
        string getName();
    }

    class Player : BasePerson
    {
        private const int SKILL_COUNT = 3;

        private Weapon weapon = null;
        private Armor armor = null;

        private int miss;
        private int xp;
        private int damage;
        private int block;
        private int chanceBlock;
        private int money = 0;
        private int regenHp = 0;

        private List<SkillInterface> skills = new List<SkillInterface>();

        public Player(string name, int energy, int hp, int miss, int xp, int damage, int block, int chanceBlock, int money) : base(name, 1, energy, hp)
        {
            this.damage = damage;//strenght
            this.block = block;//endurance
            this.xp = xp;
            this.miss = miss;//agility
            this.chanceBlock = chanceBlock;
            this.money = money;
            this.regenHp = hp;

        }

        public void setWeapon(Weapon weapon)
        {
            this.weapon = weapon;
        }

        public void setArmor(Armor armor)
        {
            this.armor = armor;
        }

        //------ Skill Function -----\\

        public bool addSkill(SkillInterface skill)
        {
            if (this.skills.Count < SKILL_COUNT)
            {
                this.skills.Add(skill);

                return true;
            }

            return false;
        }

        public bool changeSkill(int position, SkillInterface skill)
        {
            if (position < 0 || position >= this.skills.Count())
            {
                return false;
            }

            this.skills[position] = skill;

            return true;
        }

        public void showSkill()
        {
            for (int i = 0; i < this.skills.Count(); i++)
            {
                Console.WriteLine($"{i + 1}. {this.skills[i].getName()}");
            }
        }


        public int useSkill(int position)
        {
            return this.skills[position].use(getDamage());
        }


        //----- End Skill Function -----\\
        public int getRegenHp() { return this.regenHp; }
        public void setRegenHp(int regenHp) { this.regenHp = regenHp; }

        public int getMoney() { return this.money; }
        public void setMoney(int valueMoney) { this.money = valueMoney; }

        public int getChanceBlock() { return chanceBlock; }
        public void setChanceBlock(int valueChanceBlock) { chanceBlock = valueChanceBlock; }

        public int getMiss() { return miss; }
        public void setMiss(int valueMiss) { miss = valueMiss; }

        public int getXP() { return xp; }
        public void setXP(int valueXP) { xp = valueXP; }

        public int getDamage() { return damage; }
        public void setDamage(int valueDamage) { damage = valueDamage; }

        public int getBlock() { return block; }
        public void setBlock(int valueBlock) { block = valueBlock; }

        public void showStaticPlayer()
        {
            Console.WriteLine($"lvl = {getLevel()} \nEnergy =  {getEnergy()} \nDamage =  {getDamage()} \nBlock damage =  {getBlock()} \nHp = {getHp()} \nRegenHp ={getRegenHp()}\nXP = {getXP()} \nAgility = {getMiss()}% \nMoney =  {getMoney()}");
        }

        public int damageWithWeapon()
        {
            if (this.weapon != null)
            {
                this.setDamage(this.getDamage() + this.weapon.getDamage());
            }

            return this.getDamage();
        }
        public int blockWithArmor()
        {
            if (this.armor != null)
            {
                this.setBlock(this.getBlock() + this.armor.getProtection());
            }
            return this.getBlock();
        }

        public void regenHpPlayer()
        {
            setHp(regenHp);
        }
        ~Player()
        {

        }

    }


    class Smite : SkillInterface
    {
        private string name = "Smite";

        public int use(int damage)
        {
            return damage;
        }

        public string getName()
        {
            return this.name;
        }
    }

    class StrongSmite : SkillInterface
    {
        private string name = "StrongSmite";

        public int use(int damage)
        {
            return (int)(damage * 1.5);
        }

        public string getName()
        {
            return this.name;
        }

    }

    class Kick : SkillInterface
    {
        private string name = "Kick";

        public int use(int damage)
        {
            return damage * 2;
        }

        public string getName()
        {
            return this.name;
        }
    }

    class MagicSmite : SkillInterface
    {
        private string name = "MagicSmite";

        public int use(int damage)
        {
            return (int)(damage - 20 * 1.3);
        }

        public string getName()
        {
            return this.name;
        }
    }




    class ManageData
    {
        public int loadPlayer()
        {
            return 7;
        }
    }

    class Monsters : BasePerson
    {
        private int scoreGet;
        private int damage;
        private int block;
        private int chanceBlock;
        private int moneyGet;

        private int HpBoss = 300;
        private int DamageBoss = 30;
        private int ScoreGetBoss = 150;

        public Monsters(string name, int scoreGet, int level, int energy, int damage, int block, int hp, int chanceBlock, int moneyGet) : base(name, level, energy, hp)
        {
            this.scoreGet = scoreGet;
            this.damage = damage;
            this.block = block;
            this.chanceBlock = chanceBlock;
            this.moneyGet = moneyGet;
        }

        //boss
        public int getHpBoss() { return HpBoss; }
        public void setHpBoss(int valueHpBoss) { HpBoss = valueHpBoss; }

        public int getDamageBoss() { return DamageBoss; }
        public void setDamageBoss(int valueDamageBoss) { DamageBoss = valueDamageBoss; }

        public int getScoreGetBoss() { return ScoreGetBoss; }
        public void setScoreGetBoss(int valueScoreGetBoss) { ScoreGetBoss = valueScoreGetBoss; }

        //monsters

        public int getMoneyGet() { return moneyGet; }
        public void setMoneyGet(int valueMoneyGet) { moneyGet = valueMoneyGet; }

        public string getName() { return name; }
        public void setName(string valueName) { name = valueName; }

        public int getChanceBlock() { return chanceBlock; }
        public void setChanceBlock(int valueChanceBlock) { chanceBlock = valueChanceBlock; }

        public int getScoreGet() { return scoreGet; }
        public void setScoreGet(int valueScoreGet) { scoreGet = valueScoreGet; }

        public int getDamage() { return damage; }
        public void setDamage(int valueDamage) { damage = valueDamage; }

        public int getBlock() { return block; }
        public void setBlock(int valueBlock) { block = valueBlock; }



        ~Monsters()
        {

        }
    }

    class Engine
    {
        Random randNum = new Random();
        private List<string> masName = new List<string>() { "Дікарь", "Ригач", "Соплежуй", "Лазун", "Паркурщик", "Лопатокрил", "Хітман", "Очкарик", "ТікТокер", "Лінкольн Абрамс", "Ходунок", "Весельнік", "Камнебуй", "Дедінсайд" };
        private List<string> armorName = new List<string>() { "Кольчужна Броня", "Металева Броня", "Броня Бога", "Броня Ада", "Кожана Броня" };
        private List<string> weaponName = new List<string>() { "Кристалис", "Башер", "Даедалус", "ЕхоСабре", "Кая", "Яша", "Сендж" };

        private static int randomNum(Random random, int min, int max)
        {
            return random.Next(min, max + 1);
        }

        private string generateName(bool isWeapon = false)
        {
            if (isWeapon)
            {
                return this.weaponName[randomNum(randNum, 0, 7)];//this->weaponName.size() - 1
            }
            else
            {
                return this.armorName[randomNum(randNum, 0, 5)]; //this->armorName.size() - 1)
            }
        }

        private string generateMonsterName(int location)
        {
            int x = 0, y = 0;

            if (location == 1)
            {
                y = 2;
            }
            else if (location == 2)
            {
                x = 3; y = 7;
            }
            else if (location == 3)
            {
                x = 7; y = 10;
            }
            else if (location == 3)
            {
                x = 10; y = 13;
            }

            string tmp = this.masName[2];

            return this.masName[randomNum(randNum, x, y)];
        }

        public Weapon regenerateWeapon()
        {
            int tmp;
            tmp = randomNum(randNum, 3, 20);
            return new Weapon(generateName(true), tmp, 1000);
        }
        public Armor regenerateArmor()
        {
            int tmp;
            tmp = randomNum(randNum, 3, 20);
            return new Armor(generateName(), tmp, 1500);
        }

        public Player createPlayer(string name, int classType)
        {
            Player player = new Player(name, 30, 60, 0, 0, 0, 0, 15, 0);

            int block = 8, damage = 12, miss = 5;

            if (classType == 1)
            {
                block *= 2;
                player.addSkill(new StrongSmite());
                player.addSkill(new Smite());
                player.addSkill(new Kick());
            }
            else if (classType == 2)
            {
                damage *= 2;
                player.addSkill(new MagicSmite());
                player.addSkill(new Smite());
                player.addSkill(new Kick());
            }
            else
            {
                miss *= 2;
                player.addSkill(new MagicSmite());
                player.addSkill(new Smite());
                player.addSkill(new StrongSmite());
            }
            player.setMiss(miss);
            player.setBlock(block);
            player.setDamage(damage);

            return player;
        }

        public void converterXP(Player player)
        {
            int convertXp = 100;
            if (player.getXP() >= convertXp)
            {
                convertXp += 50;
                player.setLevel(player.getLevel() + 1);
                Console.WriteLine($"\nПоздравляю! у вас {player.getLevel()} lvl");
                player.setXP(0);

                player.setHp(player.getHp() + 10); // hp
                player.setDamage(player.getDamage() + 3); // damage
                player.setMiss(player.getMiss() + 1); // miss
                player.setBlock(player.getBlock() + 3); // block
                player.setChanceBlock(player.getChanceBlock() + 1); // chanceBlock

                //FunctionHelper.clickContinue();
            }
        }

        public Monsters generateMonster(int level, int location, bool isBoss = false)
        {

            int mod = 1;

            if (isBoss)
            {
                mod = 3;
            }

            if (level > 1)
            {
                if (randomNum(randNum, 0, 32767) % 3 == 1)
                {
                    level++;
                }
                else if (randomNum(randNum, 0, 32767) % 3 == 2)
                {
                    level--;
                }
            }
            else
            {
                if (randomNum(randNum, 0, 32767) % 1 == 1)
                {
                    level++;
                }
            }

            return new Monsters(
                this.generateMonsterName(location),
                50 * level * mod,
                level,
                30 * level * mod,
                15 * level * mod,
                10 * level * mod,
                50 * level * mod,
                level,
                90 * level
            );//string name, int scoreGet, int level, int energy, int damage, int block, int hp, int chanceBlock, int moneyGet
        }
    };

    class Event
    {

        private Engine engine = null;
        private List<Weapon> weaponList = null;
        private List<Armor> armorList = null;
        private Random randNum = new Random();
        private bool checkMoney(int playerMoney, int price)
        {
            if (price <= playerMoney)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public Event(Engine engine)
        {
            this.engine = engine;
            this.weaponList = new List<Weapon>();
            this.armorList = new List<Armor>();
        }

        public void shop(Player player)
        {
            bool x = true;
            while (x == true)
            {
                int choiceShop;
                int posProduct = 0;
                //string statusPos = " не куплено ";

                Console.WriteLine("Що ви хочете зробити? \n1)Купити Меч\n2)Купити Броню\n3)Вийти з магазина");
                choiceShop = Convert.ToInt16(Console.ReadLine());
                if (choiceShop == 1)
                {


                    if (this.weaponList.Count == 0)
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            this.weaponList.Add(this.engine.regenerateWeapon());
                        }
                    }

                    Console.WriteLine("Який меч ви хочете купити?\n");

                    for (int i = 0; i < weaponList.Count; i++)
                    {
                        Console.WriteLine($"{i + 1} {weaponList[i].getName()} price = {weaponList[i].getPrice()} || {weaponList[i].getDamage()} damage");
                    }
                    Console.WriteLine("3)Вийти з магазина\n");
                    posProduct = Convert.ToInt16(Console.ReadLine());


                    if (posProduct == 1)
                    {
                        if (this.checkMoney(player.getMoney(), weaponList[posProduct - 1].getPrice()))
                        {
                            player.setMoney(player.getMoney() - weaponList[posProduct - 1].getPrice());
                            player.setWeapon(weaponList[posProduct - 1]);
                            player.damageWithWeapon();
                        }
                        else
                        {
                            Console.WriteLine("У вас недостатньо грошей");
                            continue;
                        }

                    }
                    else if (posProduct == 2)
                    {
                        if (this.checkMoney(player.getMoney(), weaponList[posProduct - 1].getPrice()))
                        {
                            player.setMoney(player.getMoney() - weaponList[posProduct - 1].getPrice());
                            player.setWeapon(weaponList[posProduct - 1]);
                            player.damageWithWeapon();
                        }
                        else
                        {
                            Console.WriteLine("У вас недостатньо грошей");
                            continue;
                        }

                    }
                    else if (posProduct == 3)
                    {
                        x = false;
                        break;
                    }
                }
                if (choiceShop == 2)
                {

                    if (armorList != null)
                    {
                        armorList.Clear();
                        for (int i = 0; i < 5; i++)
                        {
                            armorList.Add(this.engine.regenerateArmor());
                        }
                    }

                    Console.WriteLine("Яку Броню ви хочите купити?\n");

                    for (int i = 0; i < armorList.Count; i++)
                    {
                        Console.WriteLine($"{i + 1} {armorList[i].getName()} price = {armorList[i].getPrice()} || block = {armorList[i].getProtection()} damage");
                    }
                    Console.WriteLine("3)Вийти з магазина");
                    posProduct = Convert.ToInt16(Console.ReadLine());
                    if (posProduct == 1)
                    {

                        if (this.checkMoney(player.getMoney(), armorList[posProduct - 1].getPrice()))
                        {
                            player.setMoney(player.getMoney() - armorList[posProduct - 1].getPrice());
                            player.setArmor(armorList[posProduct - 1]);
                            player.blockWithArmor();
                        }
                        else
                        {
                            Console.WriteLine("У вас недостатньо грошей");
                            continue;
                        }
                    }
                    else if (posProduct == 2)
                    {
                        if (this.checkMoney(player.getMoney(), weaponList[posProduct - 1].getPrice()))
                        {
                            player.setMoney(player.getMoney() - weaponList[posProduct - 1].getPrice());
                            player.setWeapon(weaponList[posProduct - 1]);
                            player.blockWithArmor();
                        }
                        else
                        {
                            Console.WriteLine("У вас недостатньо грошей");
                            continue;
                        }

                    }

                    else if (posProduct == 3)
                    {
                        x = false;
                        break;
                    }
                }
                else if (choiceShop == 3)
                {
                    x = false;
                    break;
                }
                else
                {
                    Console.WriteLine("Error 404 Повторіть спробу");
                    Console.Clear();
                    //FunctionHelper::clickContinue();

                    continue;
                }

            }
        }

        public void showRoundData(string location, string monsterName, int monsterHp, int playerHp)
        {
            Console.Clear();
            Console.WriteLine($"Ви перейшли на локацію {location}");

            Console.WriteLine($"Проти вас {monsterName} його Hp = {monsterHp} Ваше Hp = {playerHp} ");
        }



        public void fightBoss(Player player, Monsters monster, SkillInterface skills)
        {
            int diffDamage;
            int onePass_hpRegen = 0;
            int skill_position;
            while (player.getHp() > 0 && monster.getHpBoss() > 0)
            {

                player.showSkill();
                skill_position = Convert.ToInt16(Console.ReadLine());
                if (skill_position < 1 || skill_position > 3)
                {
                    Console.WriteLine("Error ");
                    Thread.Sleep(400);
                    Console.Clear();
                    continue;
                }

                Console.WriteLine($"Ви використали скіл\n{skill_position}");
                //Console.WriteLine($"Ваш скіл наніс {player.useSkill(skill_position - 1)} урона");
                //monster.setHpBoss(monster.getHpBoss() - player.useSkill(skill_position - 1));

                Console.WriteLine($"Орк наніс {monster.getDamageBoss()}");
                player.setHp(player.getHp() - monster.getDamageBoss());
                Console.WriteLine();

                if (FunctionHelper.randomNum(randNum, 0, 100) <= player.getMiss())
                {
                    Console.WriteLine("Ви уклонились від удара");
                    Thread.Sleep(2500);
                    continue;
                }

                if (FunctionHelper.randomNum(randNum, 0, 100) <= player.getChanceBlock())
                {
                    diffDamage = monster.getDamageBoss() - player.getBlock();
                    if (diffDamage < 0) { diffDamage = 0; }
                    Console.WriteLine($"Ви заблокували {player.getBlock()}");
                    Console.WriteLine($"Орк наніс {diffDamage} ");
                    player.setHp(player.getHp() - diffDamage);
                    Console.WriteLine();
                    Thread.Sleep(2500);
                    continue;
                }

                onePass_hpRegen = FunctionHelper.randomNum(randNum, player.getLevel() * 10, player.getLevel() * 20); // regenHp
                player.setHp(player.getHp() + onePass_hpRegen);
                Console.WriteLine($"Ви востановили {onePass_hpRegen} hp ");
                //FunctionHelper::clickContinue();


                Thread.Sleep(2000);
            }

            if (player.getHp() > 0)
            {
                player.setXP(player.getXP() + monster.getScoreGetBoss());
                player.setMoney(player.getMoney() + monster.getMoneyGet());
                Console.WriteLine($"Ти вийграв(ла) У тебе залишилось {player.getHp()}  \nhp Ти получив(ла) {monster.getScoreGet()} XP\nІ {monster.getMoneyGet()} монеток");
            }
            else
            {
                Console.WriteLine($"Монстр {monster.getName()} вас убив");
                player.regenHpPlayer();
            }


        }


        public void fight(Player player, Monsters monster, SkillInterface skills)
        {
            int diffDamage;
            int skill_position;
            int onePass_hpRegen = 0;

            while (monster.getHp() > 0 && player.getHp() > 0)
            {

                Console.WriteLine("Ваш первий Удар:");
                player.showSkill();
                skill_position = Convert.ToInt16(Console.ReadLine());
                if (skill_position < 1 || skill_position > 3)
                {
                    Console.WriteLine("Error ");
                    Thread.Sleep(400);
                    Console.Clear();
                    continue;
                }

                Console.WriteLine($"Ви використали скіл {skill_position}");
                Console.WriteLine($"Ваш скіл наніс {player.useSkill(skill_position - 1)} урона");
                monster.setHp(monster.getHp() - player.useSkill(skill_position - 1));


                if (FunctionHelper.randomNum(randNum, 0, 100) <= monster.getChanceBlock())
                {
                    diffDamage = player.useSkill(skill_position - 1) - monster.getBlock();
                    if (diffDamage < 0) { diffDamage = 0; }
                    Console.WriteLine($"{monster.getName()} заблокував {monster.getBlock()} урона");
                    Console.WriteLine($"Ви нанесли {diffDamage}");
                    monster.setHp(monster.getHp() - diffDamage);
                    Console.WriteLine();
                    Thread.Sleep(2500);
                    continue;
                }

                if (FunctionHelper.randomNum(randNum, 0, 100) <= player.getMiss())
                {
                    Console.WriteLine("Ви уклонились від удара ");
                    if (FunctionHelper.randomNum(randNum, 0, 100) <= 10)
                    {// krit
                        Console.WriteLine("I нанесли критический урон");
                        diffDamage = player.useSkill(skill_position - 1) * 2;
                        monster.setHp(monster.getHp() - diffDamage);
                    }
                    Thread.Sleep(2500);
                    continue;
                }
                if (FunctionHelper.randomNum(randNum, 0, 100) <= player.getChanceBlock())
                {
                    diffDamage = monster.getDamage() - player.getBlock();
                    if (diffDamage < 0) { diffDamage = 0; }
                    Console.WriteLine($"Ви заблокували {player.getBlock()} урона");
                    Console.WriteLine($"{monster.getName()} наніс {diffDamage} ");
                    player.setHp(player.getHp() - diffDamage);
                    Console.WriteLine();
                    Thread.Sleep(2500);
                    continue;
                }


                Console.WriteLine($"{monster.getName()} наніс {monster.getDamage()} ");
                player.setHp(player.getHp() - monster.getDamage());
                Console.WriteLine();

                onePass_hpRegen = FunctionHelper.randomNum(randNum, player.getLevel() * 10, player.getLevel() * 20); // regenHp
                player.setHp(player.getHp() + onePass_hpRegen);
                Console.WriteLine($"Ви востановили {onePass_hpRegen} hp ");
                //FunctionHelper.clickContinue();
                Thread.Sleep(2500);

            }

            if (player.getHp() > 0)
            {
                player.setXP(player.getXP() + monster.getScoreGet());
                player.setMoney(player.getMoney() + monster.getMoneyGet());
                Console.WriteLine($"Ти вийграв(ла) У тебе залишилось {player.getHp()} hp \nТи получив(ла) {monster.getScoreGet()}  XP\nІ {monster.getMoneyGet()} монеток");
            }
            else
            {
                Console.WriteLine($"Монстр {monster.getName()} вас убив");
                player.regenHpPlayer();
            }

            //FunctionHelper.clickContinue();

        }

        public void distributeFight(int mesto, Player player, Monsters monster, SkillInterface skills)
        {
            int tmp;
            tmp = FunctionHelper.randomNum(randNum, 0, 100);
            if (tmp <= 40)
            {

            }
            else if (tmp >= 41 && tmp <= 60)
            {
                player.setHp(player.getHp() + FunctionHelper.randomNum(randNum, 1, 30));
                Console.WriteLine($"Вам Повезло ви знайшли яблуко  +hp");
            }
            else
            {
                player.setDamage(player.getDamage() + FunctionHelper.randomNum(randNum, 1, 15));
                Console.WriteLine($"Вам Повезло монстр без ноги +damage");
            }
            if (mesto == 1)
            {
                showRoundData("Ліс", monster.getName(), monster.getHp(), player.getHp());
            }
            else if (mesto == 2)
            {
                showRoundData("Заброшка", monster.getName(), monster.getHp(), player.getHp());
            }
            else if (mesto == 3)
            {
                showRoundData("Город", monster.getName(), monster.getHp(), player.getHp());
            }
            else if (mesto == 4)
            {
                showRoundData("Пещери", monster.getName(), monster.getHp(), player.getHp());
            }
            else if (mesto == 5)
            {
                showRoundData("Катакомби", "Орк", monster.getHp(), player.getHp());
            }

            if (mesto == 5)
            {
                fightBoss(player, monster, skills);
            }
            else
            {
                fight(player, monster, skills);
            }

        }
    }
}
