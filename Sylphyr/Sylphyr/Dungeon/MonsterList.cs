using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sylphyr.Dungeon
{
    public class MonsterList
    {
        public List<Monster> MonsterLists { get; set; }

        public MonsterList()
        {
            //슬라임, 고블린, 홉고블린 오우거 오크 리자드 드래곤 스켈레톤 좀비
            MonsterLists = new List<Monster>()
            {
                new Monster(1000,"슬라임",100,5,5,1,3,1.5f,1,50,50),
                new Monster(1001,"고블린",150,15,10,5,5,1.5f,3,100,150),
                new Monster(1002,"홉고블린",250,20,20,7,10,1.5f,10,200,300),
                new Monster(1003,"오우거",1,1,1,1,1,1,1,1,1),
                new Monster(1004,"오크",1,1,1,1,1,1,1,1,1),
                new Monster(1005,"리자드",1,1,1,1,1,1,1,1,1),
                new Monster(1006,"스켈레톤",1,1,1,1,1,1,1,1,1),
                new Monster(1007,"좀비",1,1,1,1,1,1,1,1,1),
                new Monster(1007,"좀비",1,1,1,1,1,1,1,1,1)
            };

        }
    }
}
