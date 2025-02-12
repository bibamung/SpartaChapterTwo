using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Sylphyr.Character;
using Sylphyr.Scene;
using Sylphyr.YJH;


namespace Sylphyr.Dungeon
{
    public enum BossStage
    {
        GoblinKing = 10, LichKing = 20, BlueDragon = 30, RedDragon = 40,
        Rtan = 46, SSHManager = 47, YSBManager = 48, HSHManager = 49, HHSManager = 50
    }
    public enum SkillType
    {
        OneTarget = 0, WideArea = 1, DefIgnore = 2
    }
    class DungeonManager
    {
        int TotalGold = 0, TotalExp = 0;
        Random rand = new Random(DateTime.Now.Millisecond);
        List<Monster> currentStageMonsters = new List<Monster>();
        DungeonScene scene = new DungeonScene();
        Dictionary<int, List<Monster>> stageMonsters = new Dictionary<int, List<Monster>>();
        public Player player = GameManager.Instance.player;
        List<Monster> monsterlist = DataManager.Instance.monsters;
        
        static public int[] clearCount =
        {
            0,0,0,0,0,0,0,0,0,0,
            0,0,0,0,0,0,0,0,0,0,
            0,0,0,0,0,0,0,0,0,0,
            0,0,0,0,0,0,0,0,0,0,
            0,0,0,0,0,0,0,0,0,0
        };
        
        public Monster GetMonster(int id)
        {
            Monster monster;
            monster = monsterlist.SingleOrDefault(monster => monster.MonsterId == id)!;

            Monster m = new Monster(monster.MonsterId, monster.MonsterName, monster.MaxHp, monster.CurrentHp, monster.Atk, monster.Def, monster.Dex, monster.CriticalChance, monster.CriticalDamage, monster.Speed, monster.DropGold, monster.DropExp);

            return m;
        }

        public Monster GetMonster(string name)
        {
            Monster monster;
            monster = monsterlist.SingleOrDefault(monster => monster.MonsterName == name)!;

            Monster m = new Monster(monster.MonsterId, monster.MonsterName, monster.MaxHp, monster.CurrentHp, monster.Atk, monster.Def, monster.Dex, monster.CriticalChance, monster.CriticalDamage, monster.Speed, monster.DropGold, monster.DropExp);

            return m;
        }

        public DungeonManager()
        {
            // 1~10 스테이지 (초반 몬스터)
            for (int i = 1; i <= 10; i++)
                stageMonsters[i] = new List<Monster>
                { //1000,1001,1002,1003,1004,1005
                    monsterlist.SingleOrDefault(monster => monster.MonsterId == 1000)!,
                    monsterlist.SingleOrDefault(monster => monster.MonsterId == 1001)!,
                    monsterlist.SingleOrDefault(monster => monster.MonsterId == 1002)!,
                    monsterlist.SingleOrDefault(monster => monster.MonsterId == 1003)!,
                    monsterlist.SingleOrDefault(monster => monster.MonsterId == 1004)!,
                    monsterlist.SingleOrDefault(monster => monster.MonsterId == 1005)!
                };

            // 11~20 스테이지 (중반 초반)
            for (int i = 11; i <= 20; i++)
                stageMonsters[i] = new List<Monster>
                { //1003,1004,1005,1006,1007,1008,1009
                    monsterlist.SingleOrDefault(monster => monster.MonsterId == 1003)!,
                    monsterlist.SingleOrDefault(monster => monster.MonsterId == 1004)!,
                    monsterlist.SingleOrDefault(monster => monster.MonsterId == 1005)!,
                    monsterlist.SingleOrDefault(monster => monster.MonsterId == 1006)!,
                    monsterlist.SingleOrDefault(monster => monster.MonsterId == 1007)!,
                    monsterlist.SingleOrDefault(monster => monster.MonsterId == 1008)!,
                    monsterlist.SingleOrDefault(monster => monster.MonsterId == 1009)!
                };

            // 21~30 스테이지 (중반 후반)
            for (int i = 21; i <= 30; i++)
                stageMonsters[i] = new List<Monster>
                {//1006,1007,1008,1009,1010,1011,1012
                    monsterlist.SingleOrDefault(monster => monster.MonsterId == 1006)!,
                    monsterlist.SingleOrDefault(monster => monster.MonsterId == 1007)!,
                    monsterlist.SingleOrDefault(monster => monster.MonsterId == 1008)!,
                    monsterlist.SingleOrDefault(monster => monster.MonsterId == 1009)!,
                    monsterlist.SingleOrDefault(monster => monster.MonsterId == 1010)!,
                    monsterlist.SingleOrDefault(monster => monster.MonsterId == 1011)!,
                    monsterlist.SingleOrDefault(monster => monster.MonsterId == 1012)!
                };

            // 31~40 스테이지 (후반)
            for (int i = 31; i <= 40; i++)
                stageMonsters[i] = new List<Monster>
                {//1010,1011,1012,1013,1014,1015,1016,1017
                    monsterlist.SingleOrDefault(monster => monster.MonsterId == 1010)!,
                    monsterlist.SingleOrDefault(monster => monster.MonsterId == 1011)!,
                    monsterlist.SingleOrDefault(monster => monster.MonsterId == 1012)!,
                    monsterlist.SingleOrDefault(monster => monster.MonsterId == 1013)!,
                    monsterlist.SingleOrDefault(monster => monster.MonsterId == 1014)!,
                    monsterlist.SingleOrDefault(monster => monster.MonsterId == 1015)!,
                    monsterlist.SingleOrDefault(monster => monster.MonsterId == 1016)!,
                    monsterlist.SingleOrDefault(monster => monster.MonsterId == 1017)!
                };

            // 41~45 스테이지 (최후반)
            for (int i = 41; i <= 45; i++)
                stageMonsters[i] = new List<Monster>
                {//1013,1014,1015,1016,1017,1018
                    monsterlist.SingleOrDefault(monster => monster.MonsterId == 1013)!,
                    monsterlist.SingleOrDefault(monster => monster.MonsterId == 1014)!,
                    monsterlist.SingleOrDefault(monster => monster.MonsterId == 1015)!,
                    monsterlist.SingleOrDefault(monster => monster.MonsterId == 1016)!,
                    monsterlist.SingleOrDefault(monster => monster.MonsterId == 1017)!,
                    monsterlist.SingleOrDefault(monster => monster.MonsterId == 1018)!
                };
        }
        public List<Monster> GetMonstersForStage(int stage)
        {
            List<Monster> selectedMonsters = new List<Monster>();
            int monsterCount = rand.Next(1, 5); // 1~4개 등장

            if (stage == 1)                                             //1스테이지
            {
                monsterCount = 2;
                while (selectedMonsters.Count < monsterCount)
                {
                    Monster randomMonster = stageMonsters[stage][0];
                    selectedMonsters.Add(GetMonster(1000));
                }

                return selectedMonsters;
            }
            else if (!(stage % 10 == 0) && stage < (int)BossStage.Rtan && stage > 1)   //1stage도 아니고 보스스테이지가 아닐 경우
            {
                // 몬스터를 랜덤하게 선택하면서 중복을 최대 2번까지만 허용
                while (selectedMonsters.Count < monsterCount)
                {
                    Monster randomMonster = stageMonsters[stage][rand.Next(stageMonsters[stage].Count)];
                    int existingCount = stageMonsters[stage].Count(m => m.MonsterId == randomMonster.MonsterId);

                    if (existingCount < 2) // 같은 몬스터가 2번 미만일 때 추가
                    {
                        selectedMonsters.Add(GetMonster(randomMonster.MonsterId));
                    }

                }

                return selectedMonsters;
            }

            else
            {
                switch (stage)
                {
                    case (int)BossStage.GoblinKing:
                        selectedMonsters = new List<Monster>
                        {
                            GetMonster(1019), GetMonster(1005), GetMonster(1005), GetMonster(1005)
                        };
                        return selectedMonsters;
                    case (int)BossStage.LichKing:
                        selectedMonsters = new List<Monster>
                        {
                            GetMonster(1020), GetMonster(1018), GetMonster(1018), GetMonster(1018)
                        };
                        return selectedMonsters;
                    case (int)BossStage.BlueDragon:
                        selectedMonsters = new List<Monster>
                        {
                            GetMonster(1022)
                        };
                        return selectedMonsters;
                    case (int)BossStage.RedDragon:
                        selectedMonsters = new List<Monster>
                        {
                            GetMonster(1023)
                        };
                        return selectedMonsters;
                    case (int)BossStage.Rtan:
                        selectedMonsters = new List<Monster>
                        {
                            GetMonster(1024)
                        };
                        return selectedMonsters;
                    case (int)BossStage.SSHManager:
                        selectedMonsters = new List<Monster>
                        {
                            GetMonster(1025)
                        };
                        return selectedMonsters;
                    case (int)BossStage.YSBManager:
                        selectedMonsters = new List<Monster>
                        {
                            GetMonster(1026)
                        };
                        return selectedMonsters;
                    case (int)BossStage.HSHManager:
                        selectedMonsters = new List<Monster>
                        {
                            GetMonster(1027)
                        };
                        return selectedMonsters;
                    case (int)BossStage.HHSManager:
                        selectedMonsters = new List<Monster>
                        {
                            GetMonster(1028)
                        };
                        return selectedMonsters;
                }
            }
            return selectedMonsters;
        }

        public void StageSelect()
        {
            TotalGold = 0; TotalExp = 0;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Sylphyr 던전에 오신것을 환영합니다.");
                Console.WriteLine("스테이지는 1~50스테이지까지 제공되어 있습니다.");
                Console.WriteLine("(※ 돌아가시려면 0을 누르십시오)");
                Console.Write("원하시는 스테이지를 입력해주세요\n>>  ");
                int stage;
                bool isValidNum = int.TryParse(Console.ReadLine(), out stage);
                if (isValidNum)
                {
                    if (stage >= 1 && stage <= 50)
                    {
                        if (player.BestStage == stage - 1 || stage <= player.BestStage)
                        {
                            DungeonBattleStart(stage);
                        }
                        else
                        {
                            Console.WriteLine("이전 스테이지를 클리어하지 못하였습니다.");
                            Console.ReadKey();
                        }
                    }
                    else if (stage == 0)
                    {
                        MainScene mainScene = new MainScene();
                        mainScene.Run();
                    }
                    else
                    {
                        Console.WriteLine("1~50스테이지를 입력해 주세요");
                        Console.Write("any key to continue");
                        Console.ReadKey();
                    }

                }
                else
                {
                    Console.WriteLine("잘못 입력하셨습니다.");
                    Console.Write("any key to continue");
                    Console.ReadKey();
                }
            }
        }

        public void DungeonBattleStart(int stage)
        {
            currentStageMonsters = GetMonstersForStage(stage);      //현재 스테이지에 랜덤한 몬스터 저장

            List<string> OrderByAttackChar = OrderByCharacterSpeed(currentStageMonsters, player);       //몬스터 + 플레이어의 Speed를 내림차순으로 나열

            while (stageMonsters.Count > 0 && player.CurrentHp > 0)     //스테이지 몬스터가 없으면 끝
            {

                while (true)
                {
                    Console.Clear();
                    Console.WriteLine($"{stage}Stage Battle!!\n");

                    scene.DisplayPlayerHpBar(player);

                    scene.DisplayHealthBar(currentStageMonsters);           //현재 스테이지 몬스터 정보 출력
                    Console.WriteLine("\n1. 공격\n");
                    Console.WriteLine("2. 스킬사용\n");
                    Console.WriteLine("3. 회복 아이템 사용\n");


                    Console.Write("원하시는 행동을 선택해주세요.\n>> ");
                    int selectMonster, behavior;
                    bool isVaildNum = int.TryParse(Console.ReadLine(), out behavior);


                    if (isVaildNum)          //만약 올바른 입력을 받았을 경우
                    {

                        if (behavior == 1)
                        {
                            Console.Clear();
                            scene.DisplayPlayerHpBar(player);
                            scene.DisplayHealthBar(currentStageMonsters);
                            Console.Write("공격할 몬스터를 선택해주세요.\n>> ");
                            while (true)
                            {
                                isVaildNum = int.TryParse(Console.ReadLine(), out selectMonster);
                                if (isVaildNum)
                                {
                                    if (selectMonster > 0 && selectMonster <= stageMonsters.Count)      //선택한 몬스터의 번호가 0보다 크고 스테이지 내 몬스터의 수보다 작을경우 실행
                                    {
                                        //전투실행
                                        BasicAttackBattle(stage, currentStageMonsters, player, selectMonster, OrderByAttackChar);
                                        break;
                                    }
                                    else if (selectMonster > stageMonsters.Count)
                                    {
                                        Console.WriteLine("올바른 번호를 선택해주세요.");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("잘못입력하셨습니다.");
                                }
                            }
                        }
                        else if (behavior == 2)
                        {
                            SkillAttackBattle(stage, currentStageMonsters, player, OrderByAttackChar);
                        }
                        else if (behavior == 3)
                        {
                            GameManager.Instance.inventory.ConsumeDisplay(player);
                        }
                        else
                        {
                            Console.WriteLine("잘못입력하셨습니다.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("잘못입력하셨습니다.");
                    }
                }

            }

            //보상 설정
            player.AddExp(TotalExp);
            player.AddRewardGold(TotalGold, out TotalGold);
            scene.DisplayReward(player, TotalGold, TotalExp);
        }

        //스피드 순으로 나열
        public List<string> OrderByCharacterSpeed(List<Monster> currentStageMonsters, Player player)
        {
            List<string> result = new List<string>();

            currentStageMonsters.Sort(new Comparison<Monster>((n1, n2) => n2.Speed.CompareTo(n1.Speed)));


            foreach (var monsterSpeed in currentStageMonsters)
            {
                if (player.TotalStat.Speed >= monsterSpeed.Speed)
                {
                    if (!result.Contains(player.Name))
                    {
                        result.Add(player.Name);
                    }
                    result.Add(monsterSpeed.MonsterName);
                }
                else
                {
                    result.Add(monsterSpeed.MonsterName);
                }

            }

            if (!result.Contains(player.Name))
            {
                result.Add(player.Name);
            }

            return result;
        }

        public void BasicAttackBattle(int stage, List<Monster> currentStageMonsters, Player player, int selectMonster, List<string> OrderByAttackChar)
        {
            Console.Clear();
            scene.DisplayHealthBar(currentStageMonsters);

            Monster monster = null;

            for (int i = 0; i < OrderByAttackChar.Count; i++)       //스테이지에 등장하는 몬스터의 배열을 한바퀴 돌림
            {

                if (OrderByAttackChar[i] == player.Name)
                {
                    scene.BasicAttack(player, currentStageMonsters[selectMonster - 1]);

                    if (currentStageMonsters[selectMonster - 1].CurrentHp <= 0)
                    {
                        TotalExp += currentStageMonsters[selectMonster - 1].DropExp;
                        TotalGold += currentStageMonsters[selectMonster - 1].DropGold;
                        for (int j = 0; j < OrderByAttackChar.Count; j++)
                        {
                            if (OrderByAttackChar[j] == currentStageMonsters[selectMonster - 1].MonsterName)
                            {
                                OrderByAttackChar.RemoveAt(j);
                            }
                        }
                        currentStageMonsters.RemoveAt(selectMonster - 1);
                        
                        if (currentStageMonsters.Count() <= 0)
                        {
                            scene.DisplayReward(player, TotalGold, TotalExp);
                            if (player.BestStage < stage) player.SetBestStage(stage);
                            clearCount[stage - 1]++;
                            Console.WriteLine("press any key to continue...");
                            Console.ReadKey(true);
                            GameManager.Instance.main.Run();
                        }

                    }
                }
                else
                {
                    monster = GetMonster(OrderByAttackChar[i]);

                    scene.MonsterAttack(monster, player);

                    if (player.CurrentHp <= 0)
                    {
                        player.Dead();
                    }
                }

            }

        }

        public void SkillAttackBattle(int stage, List<Monster> currentStageMonsters, Player player, List<string> OrderByAttackChar)
        {
            Monster monster;
            while (true)
            {
                Console.Clear();
                Console.WriteLine();

                scene.DisplayPlayerHpBar(player);

                scene.DisplayHealthBar(currentStageMonsters);           //현재 스테이지 몬스터 정보 출력

                //플레이어 스킬 리스트 출력
                scene.DisplayPlayerSkill(player);
                Console.WriteLine("\n0. 돌아가기\n");

                Console.Write("사용하실 스킬을 선택해주세요.\n>> ");

                int useSkill, selectMonster;
                bool isVaildNum = int.TryParse(Console.ReadLine(), out useSkill);

                if (isVaildNum)
                {
                    if (useSkill >= 1 && useSkill <= player.learnedSkills.Count())
                    {
                        if (player.CurrentMp >= player.learnedSkills[useSkill - 1].UseMp)
                        {
                            player.UseMp(player.learnedSkills[useSkill - 1].UseMp);
                            //플레이어의 스킬이 광역기 공격일 경우
                            #region 광역기 스킬 공격을 하였을때
                            if (player.learnedSkills[useSkill - 1].SkillType == (int)SkillType.WideArea)
                            {

                                for (int i = 0; i < OrderByAttackChar.Count; i++)       //스테이지에 등장하는 몬스터의 배열을 한바퀴 돌림
                                {
                                    if (OrderByAttackChar[i] == player.Name)                       //이번에 공격할 캐릭터가 플레이어일 경우
                                    {
                                        //모든 몬스터 데미지 출력
                                        for (int j = 0; j < currentStageMonsters.Count; j++)
                                        {
                                            scene.SkillAttack(player, currentStageMonsters[j], useSkill);
                                            if (currentStageMonsters[j].CurrentHp <= 0)
                                            {
                                                TotalExp += currentStageMonsters[j].DropExp;
                                                TotalGold += currentStageMonsters[j].DropGold;
                                                for (int n = 0; n < OrderByAttackChar.Count; n++)
                                                {
                                                    if (OrderByAttackChar[n] == currentStageMonsters[j].MonsterName)
                                                    {
                                                        OrderByAttackChar.RemoveAt(n);
                                                    }
                                                }
                                                currentStageMonsters.RemoveAt(j);
                                            }
                                            if (currentStageMonsters.Count <= 0)
                                            {
                                                scene.DisplayReward(player, TotalGold, TotalExp);
                                                if (player.BestStage < stage) player.SetBestStage(stage);
                                                clearCount[stage - 1]++;
                                                Console.WriteLine("press any key to continue...");
                                                Console.ReadKey(true);
                                                GameManager.Instance.main.Run();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        monster = GetMonster(OrderByAttackChar[i]);

                                        scene.MonsterAttack(monster, player);

                                        if (player.CurrentHp <= 0)
                                        {
                                            player.Dead();
                                        }

                                    }
                                    break;
                                }

                            }

                            #endregion


                            //플레이어의 스킬이 1인 타겟일 경우
                            #region 단일 타겟팅 스킬을 사용한 경우
                            else if (player.learnedSkills[useSkill - 1].SkillType == (int)SkillType.OneTarget)
                            {
                                while (true)
                                {
                                    Console.Clear();
                                    scene.DisplayPlayerHpBar(player);
                                    scene.DisplayHealthBar(currentStageMonsters);
                                    Console.Write("공격할 몬스터를 선택해주세요.\n>> ");

                                    isVaildNum = int.TryParse(Console.ReadLine(), out selectMonster);
                                    if (isVaildNum)
                                    {
                                        if (selectMonster > 0 && selectMonster <= stageMonsters.Count)      //선택한 몬스터의 번호가 0보다 크고 스테이지 내 몬스터의 수보다 작을경우 실행
                                        {
                                            for (int i = 0; i < OrderByAttackChar.Count; i++)       //스테이지에 등장하는 몬스터의 배열을 한바퀴 돌림
                                            {

                                                if (OrderByAttackChar[i] == player.Name)                       //이번에 공격할 캐릭터가 플레이어일 경우
                                                {
                                                    scene.SkillAttack(player, currentStageMonsters[selectMonster - 1], useSkill);
                                                    if (currentStageMonsters[selectMonster - 1].CurrentHp <= 0)
                                                    {
                                                        TotalExp += currentStageMonsters[selectMonster - 1].DropExp;
                                                        TotalGold += currentStageMonsters[selectMonster - 1].DropGold;
                                                        for (int j = 0; j < OrderByAttackChar.Count; j++)
                                                        {
                                                            if (OrderByAttackChar[j] == currentStageMonsters[selectMonster - 1].MonsterName)
                                                            {
                                                                OrderByAttackChar.RemoveAt(j);
                                                            }
                                                        }
                                                        currentStageMonsters.RemoveAt(selectMonster - 1);
                                                    }
                                                    if (currentStageMonsters.Count <= 0)
                                                    {
                                                        scene.DisplayReward(player, TotalGold, TotalExp);
                                                        if (player.BestStage < stage) player.SetBestStage(stage);
                                                        clearCount[stage - 1]++;
                                                        Console.WriteLine("press any key to continue...");
                                                        Console.ReadKey(true);
                                                        GameManager.Instance.main.Run();
                                                    }
                                                }
                                                else
                                                {
                                                    monster = GetMonster(OrderByAttackChar[i]);

                                                    scene.MonsterAttack(monster, player);

                                                    if (player.CurrentHp <= 0)
                                                    {
                                                        player.Dead();
                                                    }
                                                }
                                            }
                                            break;

                                        }


                                        else if (selectMonster > stageMonsters.Count)
                                        {
                                            Console.WriteLine("올바른 번호를 선택해주세요.");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("숫자를 입력해주세요.");
                                    }
                                }

                            }

                            #endregion
                            else
                            {
                                while (true)
                                {
                                    Console.Clear();
                                    scene.DisplayPlayerHpBar(player);
                                    scene.DisplayHealthBar(currentStageMonsters);
                                    Console.Write("공격할 몬스터를 선택해주세요.\n>> ");

                                    isVaildNum = int.TryParse(Console.ReadLine(), out selectMonster);
                                    if (isVaildNum)
                                    {
                                        if (selectMonster > 0 && selectMonster <= stageMonsters.Count)      //선택한 몬스터의 번호가 0보다 크고 스테이지 내 몬스터의 수보다 작을경우 실행
                                        {
                                            for (int i = 0; i < OrderByAttackChar.Count; i++)       //스테이지에 등장하는 몬스터의 배열을 한바퀴 돌림
                                            {

                                                if (OrderByAttackChar[i] == player.Name)                       //이번에 공격할 캐릭터가 플레이어일 경우
                                                {
                                                    scene.DefIgnoreSkillAttack(player, currentStageMonsters[selectMonster - 1], useSkill);

                                                    if (currentStageMonsters[selectMonster - 1].CurrentHp <= 0)
                                                    {
                                                        TotalExp += currentStageMonsters[selectMonster - 1].DropExp;
                                                        TotalGold += currentStageMonsters[selectMonster - 1].DropGold;
                                                        for (int j = 0; j < OrderByAttackChar.Count; j++)
                                                        {
                                                            if (OrderByAttackChar[j] == currentStageMonsters[selectMonster - 1].MonsterName)
                                                            {
                                                                OrderByAttackChar.RemoveAt(j);
                                                            }
                                                        }
                                                        currentStageMonsters.RemoveAt(selectMonster - 1);
                                                    }
                                                    if (currentStageMonsters.Count <= 0)
                                                    {
                                                        scene.DisplayReward(player, TotalGold, TotalExp);
                                                        if (player.BestStage < stage) player.SetBestStage(stage);
                                                        clearCount[stage - 1]++;
                                                        Console.WriteLine("press any key to continue...");
                                                        Console.ReadKey(true);
                                                        GameManager.Instance.main.Run();
                                                    }

                                                }
                                                else
                                                {
                                                    monster = GetMonster(OrderByAttackChar[i]);

                                                    scene.MonsterAttack(monster, player);

                                                    if (player.CurrentHp <= 0)
                                                    {
                                                        player.Dead();
                                                    }
                                                }

                                            }
                                            break;

                                        }


                                        else if (selectMonster > stageMonsters.Count)
                                        {
                                            Console.WriteLine("올바른 번호를 선택해주세요.");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("숫자를 입력해주세요.");
                                    }

                                }

                            }

                        }
                        else
                        {
                            Console.WriteLine("스킬을 사용하기 위한 마나가 부족합니다.");
                            Console.WriteLine("press any key to continue...");
                            Console.ReadKey(true);
                        }

                    }
                    else if (useSkill == 0)
                    {
                        break;
                    }

                }
                else
                {
                    Console.WriteLine("숫자를 입력해주세요.");
                }

            }

        }

    }

}
