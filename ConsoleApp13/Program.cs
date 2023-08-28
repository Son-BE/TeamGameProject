using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TeamProject
{
    internal class Program
    {
        private static Character player;
        private static Item[] inventory;
        private static int ItemCount;
        private static Monster[] minion;

        
        static void Main(string[] args)
        {
            GameDataSetting();
            DisplayGameIntro();
        }

        static void GameDataSetting()
        {
            // 캐릭터 정보 세팅
            player = new Character("Son", "전사", 1, 10, 10, 100, 100, 1500);

            // 인벤토리 생성
            inventory = new Item[10];

            // 아이템 추가
            AddItem(new Item("판금갑옷", "단단한 갑옷입니다.", 0, 10));
            AddItem(new Item("비철단도", "사거리가 짧은 단도입니다", 10, 0));

            // 미니언 추가
            minion = new Monster[4];

            AddMinion(new Monster(1, "  원거리 미니언", "원거리에서 공격합니다.", 15, 15, 5));
            AddMinion(new Monster(3, "  근거리 미니언", "근거리에서 공격합니다.", 25, 25, 10));
            AddMinion(new Monster(5, "  대포   미니언", "강력한 공격을 합니다.", 50, 50, 15));
            AddMinion(new Monster(10,"  슈퍼  미니언", "강력한 공격을 합니다.", 100, 100, 20));


        }
        static void AddItem(Item item)
        {
            // 아이템 장착
            inventory[ItemCount] = item;
            ++ItemCount;
        }
        static void AddMinion(Monster monster)
        {
            if (monster == null)
                minion = new Monster[4];
            for (int i = 0; i < minion.Length; i++)
            {
                if (minion[i] == null)
                {
                    minion[i] = monster;
                    break;
                }
            }
        }
        static void DisplayGameIntro()
        {
            Console.Clear();
            Console.WriteLine("########################################################################################################################");
            Console.WriteLine("##                                                                                                                    ##");
            Console.WriteLine("##               ######     #    ####### #######        #######    #     # ####### ######  #######                    ##");
            Console.WriteLine("##               #     #   # #      #       #    #      #          ##   ## #     # #     # #                          ##");
            Console.WriteLine("##               #     #  #   #     #       #    #      #          # # # # #     # #     # #                          ##");
            Console.WriteLine("##               ######  #     #    #       #    #      #####      #  #  # #     # #     # #####                      ##");
            Console.WriteLine("##               #     # #######    #       #    #      #          #     # #     # #     # #                          ##");
            Console.WriteLine("##               #     # #     #    #       #    #      #          #     # #     # #     # #                          ##");
            Console.WriteLine("##               ######  #     #    #       #    ###### #######    #     # ####### ######  #######                    ##");
            Console.WriteLine("##                                                                                                                    ##");
            Console.WriteLine("########################################################################################################################");
            Console.WriteLine("## 스파르타 마을에 오신 여러분 환영합니다.                                                                            ##");
            Console.WriteLine("## 이제 전투를 시작할 수 있습니다.                                                                                    ##");
            Console.WriteLine("##                                                                                                                    ##");
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("## 1.상태보기                                                                                                         ##");
            Console.WriteLine("## 2.인벤토리                                                                                                         ##");
            Console.WriteLine("## 3.전투시작                                                                                                         ##");
            Console.ResetColor();
            Console.WriteLine("##                                                                                                                    ##");
            Console.WriteLine("## 원하시는 행동을 입력해주세요.                                                                                      ##");
            Console.WriteLine("##                                                                                                                    ##");
            Console.WriteLine("########################################################################################################################");

            int input = CheckValidInput(1, 3);
            switch (input)
            {
                case 1:
                    ShowStatus();
                    break;
                case 2:
                    ShowInventory();
                    break;
                case 3:
                    StartBattle();
                    break;
            }

        }
        static void ShowStatus()
        {
            Console.Clear();
            Console.WriteLine("┌──────────────────────────────────────────────────────────┐");
            Console.WriteLine("│상태창 확인                                               │");
            Console.WriteLine("│ 캐릭터의 정보가 표시됩니다.                              │");
            Console.WriteLine("│                                                          │");
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine($"│ LV    : {player.Level}                                                │");
            Console.WriteLine($"│ Name  : {player.Name}                                              │");
            Console.WriteLine($"│ Atk   : {player.Atk}                                               │");
            Console.WriteLine($"│ Def   : {player.Def}                                               │");
            Console.WriteLine($"│ HP    : {player.CurHp}                                              │");
            Console.WriteLine($"│ Gold  : {player.Gold}                                             │");
            Console.ResetColor();
            Console.WriteLine("│                                                          │");
            Console.WriteLine("│ 원하시는 행동을 입력해주세요.                            │ ");
            Console.WriteLine("│ 0. 나가기                                                │ ");
            Console.WriteLine("│                                                          │ ");
            Console.WriteLine("│                                                          │ ");
            Console.WriteLine("│                                                          │ ");
            Console.WriteLine("│                                                          │ ");
            Console.WriteLine("│                                                          │ ");
            Console.WriteLine("│                                                          │ ");
            Console.WriteLine("└──────────────────────────────────────────────────────────┘");


            int input = CheckValidInput(0, 0);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;

            }

        }
        static void ShowInventory()
        {
            Console.Clear();
            Console.WriteLine("┌──────────────────────────────────────────────────────────┐");
            Console.WriteLine($"│인벤토리                    {"│",31}");
            Console.WriteLine($"│ 아이템의 정보가 표시됩니다.{"│",31}");
            Console.WriteLine($"│                            {"│",31}");
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine($"│ 아이템 목록                 {"│",30}");
            Console.WriteLine($"│ 1.                         {"│",31}");
            Console.WriteLine($"│ 2.                         {"│",31}");
            Console.WriteLine($"│ 3.                         {"│",31}");
            Console.WriteLine($"│ 4.                         {"│",31}");
            Console.WriteLine($"│ 5.                         {"│",31}");
            Console.WriteLine($"│ 6.                         {"│",31}");
            Console.ResetColor();
            Console.WriteLine($"│{"│",59}");
            Console.WriteLine($"│ 원하시는 행동을 입력해주세요. {"│",28}");
            Console.WriteLine($"│ 1. 아이템 장착관리           {"│",29}");
            Console.WriteLine($"│ 0. 나가기                    {"│",29}");
            Console.WriteLine($"│{"│",59}");
            Console.WriteLine($"│{"│",59}");
            Console.WriteLine($"│{"│",59}");
            Console.WriteLine($"│{"│",59}");
            Console.WriteLine($"│{"│",59}");
            Console.WriteLine("└──────────────────────────────────────────────────────────┘");

            int input = CheckValidInput(0, 1);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;
                case 1:
                    DoItemEquipment();
                    break;

            }
        }

        static void DoItemEquipment()
        {

        }

        static void StartBattle()
        {
            Console.Clear();
            Console.WriteLine("┌──────────────────────────────────────────────────────────┐");
            Console.WriteLine($"│Battle!!                     {"│",30}");
            Console.WriteLine($"│ 미니언 목록입니다.          {"│",30}");
            Console.WriteLine($"│                             {"│",30}");
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
            for (int i = 0; i < minion.Length; i++)
            {
                if (minion[i].IsMonsterDead)
                {
                    Console.WriteLine($"│Lv.{minion[i].Level} {minion[i].Name} Dead             {"│",21}");
                }
                else
                {
                    Console.WriteLine($"│[{i + 1}] Lv.{minion[i].Level} {minion[i].Name} HP {minion[i].CurrentHp}   {"│",26}");
                }
            }
            Console.ResetColor();
            Console.WriteLine($"│{"│",59}");
            Console.WriteLine($"│ [내 정보] {"│",48}");
            Console.WriteLine($"│ Level : {player.Level}{"│",49}");
            Console.WriteLine($"│ Name  : {player.Name}{"│",47}");
            Console.WriteLine($"│ HP    : {player.CurHp} / {player.MaxHp}{"│",41}");
            Console.WriteLine($"│{"│",59}");
            Console.WriteLine($"│ 원하시는 행동을 입력해주세요.{"│",29}");
            Console.WriteLine($"│ 0. 나가기{"│",49}");
            Console.WriteLine($"│ 1. 공격{"│",51}");
            Console.WriteLine($"│{"│",59}");
            Console.WriteLine($"│{"│",59}");
            Console.WriteLine($"│{"│",59}");
            Console.WriteLine($"│{"│",59}");
            Console.WriteLine($"│{"│",59}");
            Console.WriteLine($"│{"│",59}");
            Console.WriteLine("└──────────────────────────────────────────────────────────┘");

            

            int input = CheckValidInput(0, 1);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;
                case 1:
                    AttackMinion();
                    break;
            }
        }
        static void AttackMinion()
        {
            Console.Clear();
            Console.WriteLine("┌──────────────────────────────────────────────────────────┐");
            Console.WriteLine($"│Battle!!                     {"│",30}");
            Console.WriteLine($"│ 미니언 목록입니다.          {"│",30}");
            Console.WriteLine($"│                             {"│",30}");
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;


            for (int i = 0; i < minion.Length; i++)
            {
                if (minion[i].IsMonsterDead)
                {
                    Console.WriteLine($"│[{i + 1}] {minion[i].Name} (Dead)              {"│",19}");
                }
                else
                {
                    Console.WriteLine($"│[{i + 1}] {minion[i].Name} HP : {minion[i].CurrentHp}  {"│",30}");
                }
            }
            Console.ResetColor();
            Console.WriteLine($"│{"│",59}");
            Console.WriteLine($"│ [내 정보] {"│",48}");
            Console.WriteLine($"│ Level : {player.Level}{"│",49}");
            Console.WriteLine($"│ Name  : {player.Name}{"│",47}");
            Console.WriteLine($"│ HP    : {player.CurHp} / {player.MaxHp}{"│",41}");
            Console.WriteLine($"│{"│",59}");
            Console.WriteLine("│ 공격할 미니언을 선택하세요.                              │ ");
            Console.WriteLine($"│ 1. {minion[0].Name}{"│",40}");
            Console.WriteLine($"│ 2. {minion[1].Name}{"│",40}");
            Console.WriteLine($"│ 3. {minion[2].Name}{"│",40}");
            Console.WriteLine($"│ 4.  {minion[3].Name}{"│",40}");
            Console.WriteLine($"│{"│",59}");
            Console.WriteLine($"│{"│",59}");
            Console.WriteLine($"│{"│",59}");
            Console.WriteLine($"│{"│",59}");
            Console.WriteLine("└──────────────────────────────────────────────────────────┘");

            int input = CheckValidInput(0, minion.Length);
            if (input == 0)
            {
                StartBattle();
                return;
            }

            int targetIndex = input - 1;

            if (minion[targetIndex].IsMonsterDead)
            {
                Console.WriteLine("이미 죽은 몬스터를 선택하셨습니다.");
                Console.ReadLine();
                AttackMinion();
                return;
            }

            int minDamage = (int)(player.Atk * 0.9);
            int maxDamage = (int)(player.Atk * 1.1);
            int damage = new Random().Next(minDamage, maxDamage + 1);
            Console.Clear();
                                    
            Console.WriteLine($"{player.Name}의 공격!");
            Console.WriteLine($"{minion[targetIndex].Name}에게 공격을 가했습니다. [데미지 : {damage}]");
            Console.WriteLine("");
            Console.WriteLine($"{minion[targetIndex].Name}");
          
                minion[targetIndex].CurrentHp -= damage;

            if (minion[targetIndex].CurrentHp <= 0)
            {
                minion[targetIndex].IsMonsterDead = true;
                minion[targetIndex].CurrentHp = 0;
            }

            Console.ReadLine();
            StartBattle();

        }




        static int CheckValidInput(int min, int max)
        {
            while (true)
            {
                string input = Console.ReadLine();

                bool parseSuccess = int.TryParse(input, out var ret);
                if (parseSuccess)
                {
                    if (ret >= min && ret <= max)
                        return ret;
                }

                Console.WriteLine("잘못된 입력입니다.");
            }
        }
    }
    public class Character
    {
        public string Name { get; set; }
        public string Job { get; set; }
        public int Level { get; set; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public int MaxHp { get; set; }
        public int CurHp { get; set; }
        public int Gold { get; set; }


        public Character(string name, string job, int level, int atk, int def, int maxhp, int curHp, int gold)
        {
            Name = name;
            Job = job;
            Level = level;
            Atk = atk;
            Def = def;
            MaxHp = maxhp;
            CurHp = curHp;
            Gold = gold;
        }
    }
    public class Item
    {

        public string Name { get; set; }
        public string Information { get; set; }
        public int Atk { get; set; }
        public int Def { get; set; }

        public bool Equip { get; set; }

        public Item(string name, string information, int atk, int def)
        {
            Name = name;
            Information = information;
            Atk = atk;
            Def = def;
        }
    }
    public class Monster
    {

        public string Name { get; set; }
        public string Information { get; set; }
        public int MaxHp { get; set; }
        public int Level { get; set; }

        public int CurrentHp { get; set; }
        public int ATK { get; set; }

        public bool IsMonsterDead { get; set; }


        public Monster(int level, string name, string information, int maxHp, int currentHp, int atk)
        {
            Level = level;
            Name = name;
            Information = information;
            MaxHp = maxHp;
            CurrentHp = currentHp;
            ATK = atk;
        }
    }
}