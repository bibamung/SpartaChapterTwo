using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sylphyr
{
    public class Monster
    {
        public int MonsterId { get; set; }
        public string MonsterName { get; set; }
        public float CurrentHp {  get; set; }
        public float MaxHp { get; set; }
        public float Atk { get; set; }
        public float Def { get; set; }
        public float Luk { get; set; }
        public float Dex { get; set; }
        
        public float CriticalChance { get; set; }
        public float CritcalDamage { get; set; }
        public int Speed { get; set; }

        int DropGold { get; set; }
        float DropExp { get; set; }
        
        public Monster(int monsterId, string monsterName, float MaxHp, float curHp, 
            float atk, float def, float luk, float critical, float dex, float criticalChance,
            float critcalDamage, int speed, int dropGold, float dropExp)
        {
            MonsterId = monsterId;
            MonsterName = monsterName;
            CurrentHp = curHp;
            Atk = atk;
            Def = def;
            Luk = luk;
            CriticalChance = criticalChance;
            CritcalDamage = critcalDamage;
            Dex = dex;
            Speed = speed;
            DropGold = dropGold;
            DropExp = dropExp;
        }
    }
}
