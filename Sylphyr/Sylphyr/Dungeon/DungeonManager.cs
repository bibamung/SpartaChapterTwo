using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sylphyr.Dungeon
{
    class DungeonManager : SingleTon<DungeonManager>
    {
        MonsterList monsterList=new MonsterList();
        List<Monster> stageMonsters = new List<Monster>();
        DungeonScene scene = new DungeonScene();
        
        public void DungeonStart(int stage)
        { 
           
            stageMonsters = monsterList.GetRandomMonsters();
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
