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
        
        public void DungeonStart(int stage)
        {
            
        }
    }
}
