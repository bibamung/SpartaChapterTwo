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
        public Player player { get; } = new Player();


        public void DungeonStart(int stage)
        { 
           
            stageMonsters = monsterList.GetRandomMonsters(stage);
            while (stageMonsters.Count > 0)
            {
                Console.WriteLine($"{stage}Stage Battle!!");
                scene.DisplayHealthBar(stageMonsters);

                int selectMonster;
                bool isVaildNum = int.TryParse(Console.ReadLine(), out selectMonster);
                if (isVaildNum)
                {
                    if (selectMonster > 0 && selectMonster <= stageMonsters.Count)
                    {
                        if (rand.Next(0, 100) < 10) 
                        {
                            stageMonsters[selectMonster].Hp-=1;
                        }
                    }
                    else
                    {
                        Console.WriteLine("잘못 입력하셨습니다.");
                    }
                }
            }
        }


    }
}
