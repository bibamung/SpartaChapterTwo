using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Sylphyr
{
    public class Monster
    {
        public int MonsterId;
        public string MonsterName;
        public float CurrentHp {  get; set; }
        public float MaxHp;
        public float Atk;
        public float Def;
        public float Dex;
        
        public float CriticalChance;
        public float CriticalDamage;
        public int Speed;

        public int DropGold;
        public int DropExp;
        
        public Monster(int monsterId, string monsterName, float maxHp, float curHp, 
            float atk, float def, float dex, float criticalChance,
            float criticalDamage, int speed, int dropGold, int dropExp)
        {
            MonsterId = monsterId;
            MonsterName = monsterName;
            MaxHp = maxHp;
            CurrentHp = curHp;
            Atk = atk;
            Def = def;
            CriticalChance = criticalChance;
            CriticalDamage = criticalDamage;
            Dex = dex;
            Speed = speed;
            DropGold = dropGold;
            DropExp = dropExp;
        }

    }
}
