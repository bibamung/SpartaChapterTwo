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
        public float Hp {  get; set; }
        public float MaxHp { get; set; }
        public float Atk { get; set; }
        public float Def { get; set; }
        public float Luk { get; set; }
        public float Critical { get; set; }
        public float CritcalDamage { get; set; }
        public int Speed { get; set; }

        int DropGold { get; set; }
        float DropExp { get; set; }
        
        public Monster(int monsterId, string monsterName, float MaxHp, float hp, 
            float atk, float def, float luk, float critical, 
            float critcalDamage, int speed, int dropGold, float dropExp)
        {
            MonsterId = monsterId;
            MonsterName = monsterName;
            Hp = hp;
            Atk = atk;
            Def = def;
            Luk = luk;
            Critical = critical;
            CritcalDamage = critcalDamage;
            Speed = speed;
            DropGold = dropGold;
            DropExp = dropExp;
        }
    }
}
