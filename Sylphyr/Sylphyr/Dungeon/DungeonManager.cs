using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sylphyr.KJE;


namespace Sylphyr.Dungeon
{
    class DungeonManager : SingleTon<DungeonManager>
    {
        Random rand = new Random();
        MonsterList monsterList=new MonsterList();
        List<Monster> stageMonsters = new List<Monster>();
        DungeonScene scene = new DungeonScene();
        
        
        public Player player = new Player("성원",CharacterClass.Thief);


        public void DungeonStart(int stage)
        { 
           
            stageMonsters = monsterList.GetRandomMonsters(stage);

            List<string> OrderByAttackChar = new List<string>();

            stageMonsters.Sort((m1, m2) => m1.Speed.CompareTo(m2));

            foreach (var monsterSpeed in stageMonsters)
            {
                
                if (player.TotalStat.Speed < monsterSpeed.Speed)
                {
                    OrderByAttackChar.Add(monsterSpeed.MonsterName);
                }
                else
                {
                    OrderByAttackChar.Add(player.Name);
                    OrderByAttackChar.Add(monsterSpeed.MonsterName);
                }

            }

            while (stageMonsters.Count > 0)
            {
                Console.WriteLine($"{stage}Stage Battle!!");
                scene.DisplayHealthBar(stageMonsters);

                int selectMonster;
                bool isVaildNum = int.TryParse(Console.ReadLine(), out selectMonster);
                foreach (var monster in stageMonsters) 
                {
                    foreach (var character in OrderByAttackChar)
                    {
                        if (character == player.Name)
                        {
                            if (isVaildNum)
                            {
                                if (selectMonster > 0 && selectMonster <= stageMonsters.Count)
                                {
                                    float evasionRate = 100.0f * (stageMonsters[selectMonster].Dex / stageMonsters[selectMonster].Dex + 50);
                                    if (rand.NextSingle() > evasionRate)    //회피하지 못했을 경우
                                    {
                                        Console.Clear();
                                        if (rand.NextSingle() < player.TotalStat.CriticalChance)        //크리티컬이 터졌을 경우
                                        {
                                            stageMonsters[selectMonster].Hp -=
                                                (player.TotalStat.Atk) * 1.5f -
                                                (stageMonsters[selectMonster].Def /
                                                (stageMonsters[selectMonster].Def + 50.0f)) * 100.0f;

                                            //todo : 공격 성공시 출력 내용 함수 호출
                                        }
                                        else
                                        {
                                            stageMonsters[selectMonster].Hp -=
                                                player.TotalStat.Atk -
                                                (stageMonsters[selectMonster].Def /
                                                (stageMonsters[selectMonster].Def + 50.0f)) * 100.0f;
                                            //todo : 크리티컬 공격시 출력 내용 함수 호출
                                        }
                                    }
                                    else
                                    {
                                        //todo : 회피하였을때 출력 함수 재생 => 함수 만들어야함.
                                    }

                                }
                                else
                                {
                                    Console.WriteLine("잘못 입력하셨습니다.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("숫자를 입력하세요.");
                            }

                        }
                        else
                        {
                            //todo : 플레이어 피격
                            float evasionRate = 100.0f * (player.TotalStat.Dex / player.TotalStat.Dex + 50.0f);
                            if (rand.NextSingle() > evasionRate)    //회피하지 못했을 경우
                            {
                                Console.Clear();
                                if (rand.NextSingle() < player.TotalStat.CriticalChance)        //크리티컬이 터졌을 경우
                                {
                                    stageMonsters[selectMonster].Hp -=
                                        (player.TotalStat.Atk) * 1.5f -
                                        (stageMonsters[selectMonster].Def /
                                        (stageMonsters[selectMonster].Def + 50.0f)) * 100.0f;

                                    //todo : 플레이어 피격시 출력 내용 함수 호출
                                }
                                else
                                {
                                    stageMonsters[selectMonster].Hp -=
                                        player.TotalStat.Atk -
                                        (stageMonsters[selectMonster].Def /
                                        (stageMonsters[selectMonster].Def + 50.0f)) * 100.0f;
                                    //todo : 플레이어 크리티컬 피격시 출력 내용 함수 호출
                                }
                            }

                        }



                    }






                }


            }


        }


    }
}
