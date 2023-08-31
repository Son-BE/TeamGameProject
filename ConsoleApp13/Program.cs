using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;

namespace TeamProject
{
    internal class Program
    {
        private static Character player;
        private static int ItemCount;
        private static Item[] inventory;
        private static Monster[] minion;
        private static Monster[] spawnMopArr;
        private static Identification login;
        private static List<Potion> potionlist;
        private static bool isLoggedIn = false; // 사용자 로그인 여부
        private static bool IsHeal = false;

        static void Main(string[] args)
        {
            GameDataSetting();
            if (!isLoggedIn) // 로그인되지 않았다면
            {
                FirstScene(); // 시작화면 출력
            }
            else
            {
                DisplayGameIntro(); // 이미 로그인 되어있다면 인트로 출력
            }

        }

        static void GameDataSetting()
        {
            // 로그인 기능
            login = new Identification("", "");

            // 캐릭터 정보 세팅
            player = new Character("Son", "전사", 1, 10, 10, 100, 100, 1500);

            // 인벤토리 생성
            inventory = new Item[10];

            // 아이템 정보 세팅
            AddItem(new Item("판금갑옷", "단단한 갑옷입니다.", 0, 10));
            AddItem(new Item("비철단도", "사거리가 짧은 단도입니다", 10, 0));

            potionlist = new List<Potion> { new Potion("HP포션", "사용 시 Hp를 30 회복시켜줍니다.", 30) };
            potionlist[0].Count = 3;

            // 미니언 추가
            minion = new Monster[4];

            AddMinion(new Monster(1, "원거리 미니언", "원거리에서 공격합니다.", 15, 15, 5));
            AddMinion(new Monster(3, "근거리 미니언", "근거리에서 공격합니다.", 25, 25, 10));
            AddMinion(new Monster(5, "대포   미니언", "강력한 공격을 합니다.", 50, 50, 15));
            AddMinion(new Monster(10, "슈퍼  미니언", "강력한 공격을 합니다.", 100, 100, 20));

        }


        static void AddItem(Item item)
        {
            if (ItemCount > inventory.Length) Console.WriteLine("인벤토리가 가득 찼습니다. ");
            else
            {
                // 아이템 인벤토리에 세팅
                inventory[ItemCount] = item;
                ++ItemCount;
            }
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


        static void FirstScene()
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
            Console.WriteLine("##                                                                                                                    ##");
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("## 원하시는 행동을 입력해주세요.                                                                                      ##");
            Console.ResetColor();
            Console.WriteLine("##                                                                                                                    ##");
            Console.WriteLine("##                                                                                                                    ##");
            Console.WriteLine("##  1. ID 생성하기                                                                                                    ##");
            Console.WriteLine("##  2. 로그인하기                                                                                                     ##");
            Console.WriteLine("##                                                                                                                    ##");
            Console.WriteLine("##                                                                                                                    ##");
            Console.WriteLine("########################################################################################################################");

            int input = CheckValidInput(1, 2);
            switch (input)
            {
                case 1:
                    CreateID();
                    break;
                case 2:
                    DisplayLogin();
                    break;
            }
        }


        static void CreateID()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("┌────────────────────────────┐\n" +
                              "│원하시는 ID를 입력해주세요. │\n" +
                              "└────────────────────────────┘");
            Console.ResetColor();
            string newUsername = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("┌────────────────────────────┐\n" +
                              "│원하시는 PW를 입력해주세요. │\n" +
                              "└────────────────────────────┘");
            Console.ResetColor();
            string newPassword = Console.ReadLine();

            login = new Identification(newUsername, newPassword);
            player.Name = newUsername; // 새로운 ID를 캐릭터 정보에 저장

            Console.WriteLine("닉네임이 성공적으로 생성되었습니다!");
            Console.WriteLine("계속하려면 아무 키나 누르세요.");
            Console.ReadLine();

            DisplayLogin();

        }


        // 로그인 하기(만든 ID와 다르면 로그인 실패)
        static void DisplayLogin()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("┌────────────────────────────┐\n" +
                              "│ID를 입력해주세요.          │\n" +
                              "└────────────────────────────┘");
            Console.ResetColor();
            string enteredUsername = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("┌────────────────────────────┐\n" +
                              "│PW를 입력해주세요.          │\n" +
                              "└────────────────────────────┘");
            Console.ResetColor();
            string enteredPassword = Console.ReadLine();

            if (enteredUsername == login.ID && enteredPassword == login.Password)
            {
                Console.WriteLine("로그인 성공!");
                isLoggedIn = true; // 로그인 플래그를 true로 설정하여 로그인 상태로 변경
                Console.WriteLine("계속하려면 아무 키나 누르세요...");
                Console.ReadKey();
                SelectJob(); // 직업선택창 출력
            }
            else
            {
                Console.WriteLine("잘못된 사용자 이름 또는 비밀번호입니다. 계속하려면 아무 키나 누르세요.");
                Console.ReadKey();
                FirstScene(); // 다시 처음 화면 표시
            }

        }


        static void SelectJob()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("┌────────────────────────────┐\n" +
                              "│원하는 직업을 선택해주세요.  │\n" +
                              "└────────────────────────────┘");
            Console.ResetColor();
            Console.WriteLine("1. 격투가 2. 전사 3. 궁수 4. 마법사");
            string SelectInput = Console.ReadLine();
            int num;
            bool isInt = int.TryParse(SelectInput, out num);

            if (isInt)
            {
                if (num >= 1 && num <= 4)
                    if (num == 1)
                    {
                        Console.WriteLine("격투가를 선택하셨습니다.");
                        player.Job = "격투가";
                        player.Atk = 10;
                        player.Def = 20;
                        player.CurHp = 150;
                        player.MaxHp = 150;
                        Console.WriteLine("계속하려면 아무 키나 누르세요...");
                        Console.ReadKey();
                        DisplayGameIntro();
                    }
                    else if (num == 2)
                    {
                        Console.WriteLine("전사를 선택하셨습니다.");
                        player.Job = " 전사 ";
                        player.Atk = 15;
                        player.Def = 15;
                        player.CurHp = 100;
                        player.MaxHp = 100;
                        Console.WriteLine("계속하려면 아무 키나 누르세요...");
                        Console.ReadKey();
                        DisplayGameIntro();
                    }
                    else if (num == 3)
                    {
                        Console.WriteLine("궁수를 선택하셨습니다.");
                        player.Job = " 궁수 ";
                        player.Atk = 20;
                        player.Def = 10;
                        player.CurHp = 80;
                        player.MaxHp = 80;
                        Console.WriteLine("계속하려면 아무 키나 누르세요...");
                        Console.ReadKey();
                        DisplayGameIntro();
                    }
                    else
                    {
                        Console.WriteLine("마법사를 선택하셨습니다.");
                        player.Job = "마법사";
                        player.Atk = 30;
                        player.Def = 5;
                        player.CurHp = 50;
                        player.MaxHp = 50;
                        Console.WriteLine("계속하려면 아무 키나 누르세요...");
                        Console.ReadKey();
                        DisplayGameIntro();
                    }
                else
                {
                    Console.WriteLine("1~4 의 숫자를 입력해주세요.");
                }
            }
            else
            {
                Console.WriteLine("숫자가 아닙니다.");
                SelectJob();
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
            Console.WriteLine("## 3.회복아이템                                                                                                       ##");
            Console.WriteLine("## 4.전투시작                                                                                                         ##");
            Console.ResetColor();
            Console.WriteLine("##                                                                                                                    ##");
            Console.WriteLine("## 원하시는 행동을 입력해주세요.                                                                                      ##");
            Console.WriteLine("##                                                                                                                    ##");
            Console.WriteLine("########################################################################################################################");

            int input = CheckValidInput(1, 4);
            switch (input)
            {
                case 1:
                    ShowStatus();
                    break;
                case 2:
                    ShowInventory();
                    break;
                case 3:
                    DisplayHeal();
                    break;
                case 4:
                    SpawnMonsters();
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
            Console.WriteLine($"│ job   : {player.Job}                                           │");
            Console.WriteLine($"│ Atk   : {player.Atk} {CheckEquipStats("Atk")}".PadRight(60) + "│");
            Console.WriteLine($"│ Def   : {player.Def} {CheckEquipStats("Def")}".PadRight(60) + "│");
            Console.WriteLine($"│ HP    : {player.CurHp}                                              │");
            Console.WriteLine($"│ Gold  : {player.Gold}                                             │");
            Console.ResetColor();
            Console.WriteLine("│                                                          │");
            Console.WriteLine("│ 원하시는 행동을 입력해주세요.                            │ ");
            Console.WriteLine("│                                                          │ ");
            Console.WriteLine("│ 0. 나가기                                                │ ");
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
            Console.WriteLine($"│ [ 아이템 목록 ]             {"│",30}");

            ShowList(inventory);

            Console.ResetColor();
            Console.WriteLine($"│{"│",59}");
            Console.WriteLine($"│{"│",59}");
            Console.WriteLine($"├──────────────────────────────────────────────────────────┤");
            Console.WriteLine($"│ 원하시는 행동을 입력해주세요.  {" │",27}");
            Console.WriteLine($"│{"│",59}");
            Console.WriteLine($"│{"│",59}");
            Console.WriteLine($"│{"│",59}" + "\n│ ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"아이템 번호 입력 시 해당 아이템 장착 / 해제              ");
            Console.ResetColor();
            Console.WriteLine("│\n" + $"│{"│",59}");
            Console.WriteLine($"│ 0. 나가기                                                │");
            Console.WriteLine($"│{"│",59}");
            Console.WriteLine($"│{"│",59}");
            Console.WriteLine($"│{"│",59}");
            Console.WriteLine("└──────────────────────────────────────────────────────────┘");

            CheckEquipment();

            static void CheckEquipment()
            {
                int input = CheckValidInput(0, inventory.Length);
                if (input == 0) DisplayGameIntro();
                else if (inventory[input - 1] is null)
                {
                    Console.WriteLine("비어있는 인벤토리입니다.");
                    CheckEquipment();
                }
                else
                {
                    if (!inventory[input - 1].IsEquip)
                    {
                        inventory[input - 1].IsEquip = true;
                        ShowInventory();
                    }
                    else
                    {
                        inventory[input - 1].IsEquip = false;
                        ShowInventory();
                    }
                }
            }
        }


        static void ShowList(Item[] inventory)
        {
            int i = 0;
            foreach (Item item in inventory)
            {
                string itemlist = $"│ {i + 1}. ";
                if (item != null)
                {
                    itemlist += $"{CheckItemEquipment(inventory[i].IsEquip)} {inventory[i].Name} " +
                                $"{CheckItemStats(inventory[i])} {inventory[i].Information}";
                }
                int padLen = 59 - Encoding.Default.GetBytes(itemlist).Length;
                if (padLen < 0) padLen = -padLen;
                Console.WriteLine(itemlist + "".PadRight(padLen) + "│");
                i++;
            }
        }

        static void ShowList(Monster[] monsters)
        {
            for (int i = 0; i < spawnMopArr.Length; i++)
            {
                if (monsters[i].IsMonsterDead)
                {
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine($"│Lv.{monsters[i].Level} {monsters[i].Name} DEAD             {"│",23}");
                    Console.BackgroundColor = ConsoleColor.Yellow;
                }
                else
                {
                    Console.WriteLine($"│[{i + 1}] Lv.{monsters[i].Level} {monsters[i].Name} HP {monsters[i].CurrentHp}   {"│",28}");
                }
            }
        }


        static string CheckItemEquipment(bool IsEquip)
        {
            if (IsEquip) return $"[E] ";
            else return "    ";
        }

        static string CheckItemStats(Item item)
        {
            string stats = "";
            if (item.Atk != 0) stats += $"공격력 + {item.Atk.ToString()} ";
            if (item.Def != 0) stats += $"방어력 + {item.Def.ToString()} ";
            return stats;
        }

        static string CheckEquipStats(string Atk)
        {
            int iStat = 0;
            switch (Atk)
            {
                case "Atk":
                    foreach (Item item in inventory)
                    {
                        if (item is null) break;
                        if (item.IsEquip == true) iStat += item.Atk;
                    }
                    break;
                case "Def":
                    foreach (Item item in inventory)
                    {
                        if (item is null) break;
                        if (item.IsEquip == true) iStat += item.Def;
                    }
                    break;
            }

            if (iStat != 0) return $" +({iStat.ToString()})";
            else return "";
        }

        static void SpawnMonsters()
        {
            Random random = new Random();
            int icount = random.Next(1, 5);
            spawnMopArr = new Monster[icount];

            for (int index = 0; icount > 0; index++)
            {
                int irand = random.Next(0, minion.Length);
                spawnMopArr[index] = minion[irand];
                icount--;
            }
        }


        static void StartBattle()
        {
            Console.Clear();
            Console.WriteLine("┌──────────────────────────────────────────────────────────┐");
            Console.WriteLine($"│Battle!!                     {"│",30}");
            Console.WriteLine($"│ 미니언 {spawnMopArr.Length} 마리가 등장했다!          {"│",27}");
            Console.WriteLine($"│                             {"│",30}");
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
            ShowList(spawnMopArr);
            Console.ResetColor();
            Console.WriteLine($"│{"│",59}");
            Console.WriteLine($"│ [내 정보] {"│",48}");
            Console.WriteLine($"│ Level : {player.Level}{"│",49}");
            Console.WriteLine($"│ Name  : {player.Name}{"│",47}");
            Console.WriteLine($"│ HP    : {player.CurHp} / {player.MaxHp}{"│",41}");
            Console.WriteLine($"│{"│",59}");
            Console.WriteLine($"│ 원하시는 행동을 입력해주세요.{"│",29}");
            Console.WriteLine($"│ 0. 나가기{"│",49}");
            Console.WriteLine($"│ 1. 공격하기{"│",47}");
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
            ShowList(spawnMopArr);
            Console.ResetColor();
            Console.WriteLine($"│{"│",59}");
            Console.WriteLine($"│ [내 정보] {"│",48}");
            Console.WriteLine($"│ Level : {player.Level}{"│",49}");
            Console.WriteLine($"│ Name  : {player.Name} ({player.Job}){"│",40}");
            Console.WriteLine($"│ HP    : {player.CurHp} / {player.MaxHp}{"│",41}");
            Console.WriteLine($"│{"│",59}");
            Console.WriteLine("│ 공격할 미니언을 선택하세요.                              │ ");

            int i = 1;
            foreach (Monster minion in spawnMopArr)
            {
                Console.WriteLine($"│ {i}. {minion.Name}{"│",40}");
                i++;
            }
            Console.WriteLine($"│ 0. 돌아가기{"│",47}");
            Console.WriteLine($"│{"│",59}");
            Console.WriteLine($"│{"│",59}");
            Console.WriteLine($"│{"│",59}");
            Console.WriteLine($"│{"│",59}");
            Console.WriteLine("└──────────────────────────────────────────────────────────┘");

            CheckAttackInput();

            static void CheckAttackInput()
            {
                int input = CheckValidInput(0, spawnMopArr.Length);
                if (input == 0)
                {
                    StartBattle();
                    return;
                }
                else if (spawnMopArr[input - 1].IsMonsterDead)
                {
                    Console.WriteLine("이미 죽은 몬스터를 선택하셨습니다.");
                    CheckAttackInput();
                }
                else
                {
                    int itarget = input - 1;
                    // 미니언에게 가해지는 데미지
                    int minDamage = (int)Math.Ceiling(player.Atk * 0.9);
                    int maxDamage = (int)Math.Ceiling(player.Atk * 1.1);

                    // 미니언 HP 반영
                    int minionDamage = new Random().Next(minDamage, maxDamage + 1);
                    spawnMopArr[itarget].CurrentHp -= minionDamage;

                    if (spawnMopArr[itarget].CurrentHp <= 0)
                    {
                        spawnMopArr[itarget].IsMonsterDead = true;
                        spawnMopArr[itarget].CurrentHp = 0;
                    }

                    Console.Clear();
                    Console.WriteLine("Battle!!\n");
                    Console.WriteLine();
                    Console.WriteLine($"{player.Name} 의 공격!");
                    Console.WriteLine($"Lv.{spawnMopArr[itarget].Level} {spawnMopArr[itarget].Name} 에게 공격을 가했습니다. [데미지 : {minionDamage}]");
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine($"Lv.{spawnMopArr[itarget].Level} {spawnMopArr[itarget].Name}");
                    Console.WriteLine($"남은 HP {spawnMopArr[itarget].CurrentHp}");
                    Console.WriteLine();
                    Console.WriteLine();

                    CheckStopBattle();
                    Console.WriteLine("계속하려면 아무키나 누르세요.");
                    Console.ReadLine();
                    EnemyPhase();
                }
            }
        }


        static void EnemyPhase()
        {
            foreach (Monster monster in spawnMopArr)
            {
                // 미니언이 가하는 데미지
                int minLvDamage = monster.Level * 2;
                int maxLvDamage = monster.Level * 3;

                Random random = new Random();
                // 플레이어 HP 반영
                int playerDamage = random.Next(minLvDamage, maxLvDamage + 1);
                player.CurHp -= playerDamage;

                Console.Clear();
                Console.WriteLine("Battle!!\n");
                Console.WriteLine();
                Console.WriteLine($"Lv.{monster.Level} {monster.Name} 의 공격!");
                Console.WriteLine($"{player.Name} 이 공격을 받았습니다. [데미지 : {playerDamage}]");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine($"Lv.{player.Level} {player.Name}");
                Console.WriteLine($"HP {player.CurHp + playerDamage} -> {player.CurHp}");
                Console.WriteLine();
                Console.WriteLine();

                CheckStopBattle();
                Console.WriteLine("계속하려면 아무키나 누르세요.");
                Console.ReadLine();
            }
            AttackMinion();
        }


        static void CheckStopBattle()
        {
            int icount = 0;
            foreach (Monster monster in spawnMopArr)
            {
                if (monster.IsMonsterDead) icount++;
            }

            if (icount == spawnMopArr.Length) WinBattle();

            else if (player.CurHp <= 0)
            {
                player.CurHp = 0;
                LoseBattle();
            }
        }


        static void WinBattle()
        {
            // 레벨업 구현
            player.LevelUp();

            Random rand = new Random();
            // 골드보상
            int victoryReward = rand.Next(100, 500);
            player.Gold += victoryReward;
            string goldChange = player.Gold > 0 ? $"+{victoryReward}" : victoryReward.ToString();

            // 포션보상
            int potionReward = rand.Next(0, spawnMopArr.Length);
            potionlist[0].Count += potionReward;

            Console.Clear();
            Console.WriteLine("┌──────────────────────────────────────────────────────────┐");
            Console.WriteLine($"│Battle!! - Result                                         │");
            Console.WriteLine($"│                                                          │");
            Console.WriteLine($"│                                                          │");
            Console.WriteLine($"│Victory Game                                              │");
            Console.WriteLine($"│                                                          │");
            Console.WriteLine($"│                                                          │");
            Console.WriteLine($"│미니언과의 전투에서 승리하셨습니다.                       │");
            Console.WriteLine($"│                                                          │");
            Console.WriteLine($"│Lv.{player.Level} {player.Job} {player.Name}                                                 │");
            Console.WriteLine($"│남은 HP : {player.CurHp}                                              │");
            Console.WriteLine($"│보유 골드 : {player.Gold}({goldChange})G                                  │");
            Console.WriteLine($"│보유 포션 : {potionlist[0].Count - potionReward} (+{potionReward}) 개                                        │");
            Console.WriteLine($"│                                                          │");
            Console.WriteLine($"│0. 초기화면                                               │");
            Console.WriteLine($"│1. 전투재개                                               │");
            Console.WriteLine("└──────────────────────────────────────────────────────────┘");

            int input = CheckValidInput(0, 1);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;
                case 1:
                    SpawnMonsters();
                    StartBattle();
                    break;
            }
        }


        static void LoseBattle()
        {
            Console.Clear();
            Console.WriteLine("┌──────────────────────────────────────────────────────────┐");
            Console.WriteLine($"│Battle!! - Result                                         ┃");
            Console.WriteLine($"│                                                          ┃");
            Console.WriteLine($"│                                                          ┃");
            Console.WriteLine($"│You Lose                                                  ┃");
            Console.WriteLine($"│                                                          ┃");
            Console.WriteLine($"│                                                          ┃");
            Console.WriteLine($"│전투에서 사망하셨습니다.                                  ┃");
            Console.WriteLine($"│                                                          ┃");
            Console.WriteLine($"│Lv.{player.Level} {player.Job} {player.Name}                                                    ┃");
            Console.WriteLine($"│남은 HP : {player.CurHp}                                             ┃");
            Console.WriteLine($"│                                                          ┃");
            Console.WriteLine($"│0. 초기화면                                               ┃");
            Console.WriteLine("└──────────────────────────────────────────────────────────┘");

            int input = CheckValidInput(0, 0);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;
            }
        }


        static void DisplayHeal()
        {
            Console.Clear();
            Console.WriteLine("┌────────────────────────────────────────────────────────────────┐");
            Console.WriteLine("│ 회복" + "".PadRight(59) + "|");
            Console.WriteLine($"| 포션을 사용하면 체력을 {potionlist[0].Hp} 회복할 수 있습니다.(남은 포션 : {potionlist[0].Count})".PadRight(43) + "|");
            Console.WriteLine("│".PadRight(65) + "|");
            Console.WriteLine("│".PadRight(65) + "|");
            Console.WriteLine($"| Lv.{player.Level} {player.Name} HP {player.CurHp} / {player.MaxHp}".PadRight(65) + "|");
            Console.WriteLine("│".PadRight(65) + "|");
            Console.WriteLine("│".PadRight(65) + "|");
            Console.WriteLine("│ 원하시는 행동을 입력해주세요.".PadRight(52) + "│");
            Console.WriteLine("│".PadRight(65) + "|");
            Console.WriteLine("│".PadRight(65) + "|");
            Console.WriteLine("│ 0. 나가기                                                      │ ");
            Console.WriteLine("│ 1. 사용하기                                                    │ ");
            Console.WriteLine("│".PadRight(65) + "|");
            Console.WriteLine("│".PadRight(65) + "|");
            Console.WriteLine("└────────────────────────────────────────────────────────────────┘");

            ChoiceHeal();
        }

        static void ChoiceHeal()
        {
            int input = CheckValidInput(0, 1);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;
                case 1:
                    UsePotion(potionlist[0]);
                    break;
            }
        }

        static void UsePotion(Potion potion)
        {

            if (potion.Count <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("포션이 부족합니다.");
                Console.ResetColor();
                ChoiceHeal();
            }
            else if (player.CurHp == player.MaxHp)
            {
                Console.WriteLine("체력이 이미 가득 차있습니다.");
                ChoiceHeal();
            }
            else if (player.CurHp + potion.Hp >= player.MaxHp) player.CurHp = player.MaxHp;
            else player.CurHp += potion.Hp;
            potion.Count -= 1;

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("회복을 완료했습니다.");
            Console.ResetColor();
            ChoiceHeal();
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

        public void LevelUp()
        {
            Level += 1;
            Atk += 5;
            MaxHp += 10;
            CurHp += 10;
        }

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
        public string Name { get; }
        public string Information { get; set; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public bool IsEquip { get; set; }

        public Item(string name, string information, int atk, int def)
        {
            Name = name;
            Information = information;
            Atk = atk;
            Def = def;
            IsEquip = false;
        }
    }
    public class Potion
    {
        public string Name { get; }
        public string Information { get; }
        public int Hp { get; }
        public int Count { get; set; }

        //public int Atk { get; set; }
        //public int Def { get; set; }

        public Potion(string name, string information, int hp)
        {
            Name = name;
            Information = information;
            Hp = hp;
            Count = 0;
        }
    }
    public class Monster
    {
        public string Name { get; }
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

    public class Identification
    {
        public string ID { get; set; }
        public string Password { get; set; }

        public Identification(string id, string password)
        {
            ID = id;
            Password = password;
        }
    }
}