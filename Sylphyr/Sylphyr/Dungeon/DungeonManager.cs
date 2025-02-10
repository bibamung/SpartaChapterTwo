using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sylphyr.Character;


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
    class DungeonManager : SingleTon<DungeonManager>
    {
        int TotalGold = 0, TotalExp = 0;
        int GainGold = 0;
        Random rand = new Random(DateTime.Now.Millisecond);
        List<Monster> currentStageMonsters = new List<Monster>();
        DungeonScene scene = new DungeonScene();

        Dictionary<int, List<Monster>> stageMonsters = new Dictionary<int, List<Monster>>();

        public Player player = new Player("성원", CharacterClass.Thief);

        public DungeonManager()
        {
            TotalGold = 0; TotalExp = 0;
            // 1~10 스테이지 (초반 몬스터)
            for (int i = 1; i <= 10; i++)
                stageMonsters[i] = new List<Monster>
                { //1000,1001,1002,1003,1004,1005
                    new Monster(1000,"슬라임",100,100,5,5,5,0.1f,5,5,5,5,5),
                    new Monster(1001,"블루 슬라임",100,100,5,5,5,0.1f,5,5,5,5,5),
                    new Monster(1002,"레드 슬라임",100,100,5,5,5,0.1f,5,5,5,5,5),
                    new Monster(1003,"퍼플 슬라임",100,100,5,5,5,0.1f,5,5,5,5,5),
                    new Monster(1004,"골드 슬라임",100,100,5,5,5,0.1f,5,5,5,5,5),
                    new Monster(1005,"초코보",100,100,5,5,5,0.1f,5,5,5,5,5),
                };

            // 11~20 스테이지 (중반 초반)
            for (int i = 11; i <= 20; i++)
                stageMonsters[i] = new List<Monster>
                { //1003,1004,1005,1006,1007,1008,1009
                    new Monster(1003,"퍼플 슬라임",100,100,5,5,5,0.1f,5,5,5,5,5),
                    new Monster(1004,"골드 슬라임",100,100,5,5,5,0.1f,5,5,5,5,5),
                    new Monster(1005,"초코보",100,100,5,5,5,0.1f,5,5,5,5,5),
                    new Monster(1006,"고블린",100,100,5,5,5,0.1f,5,5,5,5,5),
                    new Monster(1007,"코볼트",100,100,5,5,5,0.1f,5,5,5,5,5),
                    new Monster(1008,"홉고블린",100,100,5,5,5,0.1f,5,5,5,5,5),
                    new Monster(1009,"오우거",100,100,5,5,5,0.1f,5,5,5,5,5)
                };

            // 21~30 스테이지 (중반 후반)
            for (int i = 21; i <= 30; i++)
                stageMonsters[i] = new List<Monster>
                {//1006,1007,1008,1009,1010,1011,1012
                    new Monster(1006,"고블린",100,100,5,5,5,0.1f,5,5,5,5,5),
                    new Monster(1007,"코볼트",100,100,5,5,5,0.1f,5,5,5,5,5),
                    new Monster(1008,"홉고블린",100,100,5,5,5,0.1f,5,5,5,5,5),
                    new Monster(1009,"오우거",100,100,5,5,5,0.1f,5,5,5,5,5),
                    new Monster(1010,"오크",100,100,5,5,5,0.1f,5,5,5,5,5),
                    new Monster(1011,"리자드",100,100,5,5,5,0.1f,5,5,5,5,5),
                    new Monster(1012,"좀비",100,100,5,5,5,0.1f,5,5,5,5,5)
                };

            // 31~40 스테이지 (후반)
            for (int i = 31; i <= 40; i++)
                stageMonsters[i] = new List<Monster>
                {//1010,1011,1012,1013,1014,1015,1016,1017
                    new Monster(1010,"오크",100,100,5,5,5,0.1f,5,5,5,5,5),
                    new Monster(1011,"리자드",100,100,5,5,5,0.1f,5,5,5,5,5),
                    new Monster(1012,"좀비",100,100,5,5,5,0.1f,5,5,5,5,5),
                    new Monster(1013,"스켈레톤",100,100,5,5,5,0.1f,5,5,5,5,5),
                    new Monster(1014,"임프",100,100,5,5,5,0.1f,5,5,5,5,5),
                    new Monster(1015,"인큐버스",100,100,5,5,5,0.1f,5,5,5,5,5),
                    new Monster(1016,"서큐버스",100,100,5,5,5,0.1f,5,5,5,5,5),
                    new Monster(1017,"듀라한",100,100,5,5,5,0.1f,5,5,5,5,5)
                };

            // 41~45 스테이지 (최후반)
            for (int i = 41; i <= 45; i++)
                stageMonsters[i] = new List<Monster>
                {//1013,1014,1015,1016,1017,1018
                    new Monster(1013,"스켈레톤",100,100,5,5,5,0.1f,5,5,5,5,5),
                    new Monster(1014,"임프",100,100,5,5,5,0.1f,5,5,5,5,5),
                    new Monster(1015,"인큐버스",100,100,5,5,5,0.1f,5,5,5,5,5),
                    new Monster(1016,"서큐버스",100,100,5,5,5,0.1f,5,5,5,5,5),
                    new Monster(1017,"듀라한",100,100,5,5,5,0.1f,5,5,5,5,5),
                    new Monster(1018,"리치",100,100,5,5,5,0.1f,5,5,5,5,5)
                };
        }

        public List<Monster> GetMonstersForStage(int stage)
        {
            List<Monster> selectedMonsters = new List<Monster>();
            int monsterCount = rand.Next(1, 5); // 1~4개 등장

            if (stage == 1)                                             //1스테이지
            {
                monsterCount = rand.Next(1, 3);
                while (selectedMonsters.Count < monsterCount)
                {
                    Monster randomMonster = stageMonsters[stage][0];
                    selectedMonsters.Add(randomMonster);
                }

                return selectedMonsters;
            }
            else if (!(stage % 10 == 0) && stage < (int)BossStage.Rtan)   //1stage도 아니고 보스스테이지가 아닐 경우
            {
                // 몬스터를 랜덤하게 선택하면서 중복을 최대 2번까지만 허용
                while (selectedMonsters.Count < monsterCount)
                {
                    Monster randomMonster = stageMonsters[stage][rand.Next(stageMonsters[stage].Count)];
                    int existingCount = stageMonsters[stage].Count(m => m.MonsterId == randomMonster.MonsterId);

                    if (existingCount < 2) // 같은 몬스터가 2번 미만일 때 추가
                    {
                        selectedMonsters.Add(randomMonster);
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
                            new Monster(1019,"고블린킹",100,100,5,5,5,0.1f,5,5,5,5,5),
                            new Monster(1006,"고블린",100,100,5,5,5,0.1f,5,5,5,5,5),
                            new Monster(1006,"고블린",100,100,5,5,5,0.1f,5,5,5,5,5),
                            new Monster(1006,"고블린",100,100,5,5,5,0.1f,5,5,5,5,5)
                        };
                        return selectedMonsters;
                    case (int)BossStage.LichKing:
                        selectedMonsters = new List<Monster>
                        {
                            new Monster(1020,"리치킹",100,100,5,5,5,0.1f,5,5,5,5,5),
                            new Monster(1018,"리치",100,100,5,5,5,0.1f,5,5,5,5,5),
                            new Monster(1018,"리치",100,100,5,5,5,0.1f,5,5,5,5,5),
                            new Monster(1018,"리치",100,100,5,5,5,0.1f,5,5,5,5,5)
                        };
                        return selectedMonsters;
                    case (int)BossStage.BlueDragon:
                        selectedMonsters = new List<Monster>
                        {
                            new Monster(1022,"수룡",100,100,5,5,5,0.1f,5,5,5,5,5)
                        };
                        return selectedMonsters;
                    case (int)BossStage.RedDragon:
                        selectedMonsters = new List<Monster>
                        {
                            new Monster(1023,"패룡",100,100,5,5,5,0.1f,5,5,5,5,5)
                        };
                        return selectedMonsters;
                    case (int)BossStage.Rtan:
                        selectedMonsters = new List<Monster>
                        {
                            new Monster(1024, "르탄이", 100, 100, 5, 5, 5, 0.1f, 5, 5, 5, 5, 5)
                        };
                        return selectedMonsters;
                    case (int)BossStage.SSHManager:
                        selectedMonsters = new List<Monster>
                        {
                            new Monster(1025,"고위정령 송승환",100,100,5,5,5,0.1f,5,5,5,5,5)
                        };
                        return selectedMonsters;
                    case (int)BossStage.YSBManager:
                        selectedMonsters = new List<Monster>
                        {
                            new Monster(1026,"고위정령 윤수빈",100,100,5,5,5,0.1f,5,5,5,5,5),
                        };
                        return selectedMonsters;
                    case (int)BossStage.HSHManager:
                        selectedMonsters = new List<Monster>
                        {
                            new Monster(1027,"고위정령 홍성현",100,100,5,5,5,0.1f,5,5,5,5,5)
                        };
                        return selectedMonsters;
                    case (int)BossStage.HHSManager:
                        selectedMonsters = new List<Monster>
                        {
                            new Monster(1028,"고위정령 한효승",100,100,5,5,5,0.1f,5,5,5,5,5)
                        };
                        return selectedMonsters;
                }
            }
            return selectedMonsters;
        }

        public void DungeonStart(int stage)
        {
            currentStageMonsters = GetMonstersForStage(stage);      //현재 스테이지에 랜덤한 몬스터 저장

            List<string> OrderByAttackChar = OrderByCharacterSpeed(currentStageMonsters, player);       //몬스터 + 플레이어의 Speed를 내림차순으로 나열

            while (stageMonsters.Count > 0 && player.CurrentHp > 0)     //스테이지 몬스터가 없으면 끝
            {
                Console.Clear();
                Console.WriteLine($"{stage}Stage Battle!!\n");

                scene.DisplayPlayerHpBar(player);

                scene.DisplayHealthBar(currentStageMonsters);           //현재 스테이지 몬스터 정보 출력
                Console.WriteLine("\n1. 공격\n");
                Console.WriteLine("\n2. 스킬사용\n");

                Console.Write("원하시는 행동을 선택해주세요.\n>> ");

                int selectMonster, behavior;
                bool isVaildNum = int.TryParse(Console.ReadLine(), out behavior);


                if (isVaildNum)          //만약 올바른 입력을 받았을 경우
                {
                    if (behavior == 1)
                    {
                        Console.Clear();
                        scene.DisplayHealthBar(currentStageMonsters);

                        Console.Write("공격할 몬스터를 선택해주세요.\n>> ");

                        isVaildNum = int.TryParse(Console.ReadLine(), out selectMonster);
                        if (isVaildNum)
                        {
                            if (selectMonster > 0 && selectMonster <= stageMonsters.Count)      //선택한 몬스터의 번호가 0보다 크고 스테이지 내 몬스터의 수보다 작을경우 실행
                            {
                                //전투실행
                                BasicAttackBattle(stage, currentStageMonsters, player, selectMonster, OrderByAttackChar);

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
                    else if (behavior == 2)
                    {
                        SkillAttackBattle(stage, currentStageMonsters, player, OrderByAttackChar);
                    }
                }
                else
                {
                    Console.WriteLine("잘못입력하셨습니다.");
                }

            }

            //보상 설정
            player.AddExp(TotalExp);
            player.AddRewardGold(TotalGold, out GainGold);

            

            scene.DisplayReward(player,TotalGold,TotalExp);
        }

        public List<string> OrderByCharacterSpeed(List<Monster> currentStageMonsters, Player player)
        {
            List<string> result = new List<string>();

            currentStageMonsters.Sort(new Comparison<Monster>((n1, n2) => n2.Speed.CompareTo(n1)));

            foreach (var monsterSpeed in currentStageMonsters)
            {

                if (player.TotalStat.Speed < monsterSpeed.Speed)
                {
                    result.Add(monsterSpeed.MonsterName);
                }
                else
                {
                    result.Add(player.Name);
                    result.Add(monsterSpeed.MonsterName);
                }

            }

            return result;
        }

        public float Damage(float Atk, float Def, float criticalDamage, bool isCritical)
        {
            float finalDamage = 0.0f;
            if (isCritical)
            {
                finalDamage = Atk * criticalDamage - (Def / (Def + 50.0f)) * 100.0f;
            }
            else
            {
                finalDamage = Atk * criticalDamage - (Def / (Def + 50.0f)) * 100.0f;
            }
            return finalDamage;
        }

        public void BasicAttackBattle(int stage, List<Monster> currentStageMonsters, Player player, int selectMonster, List<string> OrderByAttackChar)
        {
            Console.Clear();
            scene.DisplayHealthBar(currentStageMonsters);

            foreach (var monster in stageMonsters[stage])       //스테이지에 등장하는 몬스터의 배열을 한바퀴 돌림
            {
                int count = 0;
                if (OrderByAttackChar[count++] == player.Name)                       //이번에 공격할 캐릭터가 플레이어일 경우
                {
                    scene.BasicAttack(player, currentStageMonsters[selectMonster - 1]);
                    if (currentStageMonsters[selectMonster - 1].CurrentHp <= 0)
                    {
                        TotalExp += currentStageMonsters[selectMonster - 1].DropExp;
                        TotalGold += currentStageMonsters[selectMonster - 1].DropGold;
                        currentStageMonsters.RemoveAt(selectMonster - 1);
                    }
                }

                else
                {
                    //todo : 플레이어 피격
                    scene.MonsterAttack(monster, player);
                    if (player.CurrentHp <= 0) 
                    {
                        //GameOver();
                    }

                }

            }


        }

        public void SkillAttackBattle(int stage, List<Monster> currentStageMonsters, Player player, List<string> OrderByAttackChar)
        {
            Console.Clear();
            Console.WriteLine();

            scene.DisplayPlayerHpBar(player);

            scene.DisplayHealthBar(currentStageMonsters);           //현재 스테이지 몬스터 정보 출력

            //플레이어 스킬 리스트 출력
            Console.WriteLine("\n0. 돌아가기\n");

            Console.Write("사용하실 스킬을 선택해주세요.\n>> ");

            int useSkill, selectMonster, skillType;
            bool isVaildNum = int.TryParse(Console.ReadLine(), out useSkill);

            if (isVaildNum)
            {
                if (useSkill >= 1 && useSkill < 4/*원래는 플레이어가 가지고있는 스킬 수만큼*/)
                {
                    //플레이어의 스킬이 광역기 공격일 경우
                    #region 광역기 스킬 공격을 하였을때
                    if (true/*player.Skill[skillUse - 1] == (int)SkillType.WideArea*/)
                    {
                        foreach (var monster in stageMonsters[stage])       //스테이지에 등장하는 몬스터의 배열을 한바퀴 돌림
                        {
                            int count = 0;
                            if (OrderByAttackChar[count++] == player.Name)                       //이번에 공격할 캐릭터가 플레이어일 경우
                            {
                                //모든 몬스터 데미지 출력
                                foreach (var monster1 in currentStageMonsters)
                                {
                                    int monster1Index = 0;
                                    scene.SkillAttack(player, monster1, useSkill);
                                    if (monster1.CurrentHp <= 0)
                                    {
                                        TotalExp += monster1.DropExp;
                                        TotalGold += monster1.DropGold;
                                        currentStageMonsters.RemoveAt(monster1Index);
                                        monster1Index--;    
                                    }
                                    monster1Index++;
                                }
                            }
                            else
                            {
                                scene.MonsterAttack(monster, player);
                                if (player.CurrentHp <= 0)
                                {
                                    player.Dead();
                                }
                            }
                        }
                    }
                    #endregion


                    //플레이어의 스킬이 1인 타겟일 경우
                    #region 단일 타겟팅 스킬을 사용한 경우
                    else if (false/*player.Skill[skillUse - 1] == (int)SkillType.OneTarget*/)
                    {
                        while (true)
                        {
                            isVaildNum = int.TryParse(Console.ReadLine(), out selectMonster);
                            if (isVaildNum)
                            {
                                if (selectMonster > 0 && selectMonster <= stageMonsters.Count)      //선택한 몬스터의 번호가 0보다 크고 스테이지 내 몬스터의 수보다 작을경우 실행
                                {
                                    foreach (var monster in stageMonsters[stage])       //스테이지에 등장하는 몬스터의 배열을 한바퀴 돌림
                                    {
                                        int count = 0;
                                        if (OrderByAttackChar[count++] == player.Name)                       //이번에 공격할 캐릭터가 플레이어일 경우
                                        {
                                            scene.SkillAttack(player, currentStageMonsters[selectMonster], useSkill);
                                            if (currentStageMonsters[selectMonster].CurrentHp == 0)
                                            {
                                                TotalExp += currentStageMonsters[selectMonster].DropExp;
                                                TotalGold += currentStageMonsters[selectMonster].DropGold;
                                                currentStageMonsters.RemoveAt(selectMonster);
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
                            isVaildNum = int.TryParse(Console.ReadLine(), out selectMonster);
                            if (isVaildNum)
                            {
                                if (selectMonster > 0 && selectMonster <= stageMonsters.Count)      //선택한 몬스터의 번호가 0보다 크고 스테이지 내 몬스터의 수보다 작을경우 실행
                                {
                                    foreach (var monster in stageMonsters[stage])       //스테이지에 등장하는 몬스터의 배열을 한바퀴 돌림
                                    {
                                        int count = 0;
                                        if (OrderByAttackChar[count++] == player.Name)                       //이번에 공격할 캐릭터가 플레이어일 경우
                                        {
                                            scene.DefIgnoreSkillAttack(player, currentStageMonsters[selectMonster], useSkill);
                                            if (currentStageMonsters[selectMonster].CurrentHp == 0)
                                            {
                                                TotalExp += currentStageMonsters[selectMonster].DropExp;
                                                TotalGold += currentStageMonsters[selectMonster].DropGold;
                                                currentStageMonsters.RemoveAt(selectMonster);
                                            }

                                        }

                                    }

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

            }
            else
            {
                Console.WriteLine("숫자를 입력해주세요.");
            }

        }

        








    }

}
