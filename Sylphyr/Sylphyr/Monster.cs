using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sylphyr
{
    public class Monster
    {
        int monsterId;
        string monsterName;
        float hp;
        float atk;
        float dex;
        float luk;
        float critical;
        float critcalDamage;
        int speed;

        int dropGold;
        float dropExp;
        public Monster(int monsterId, string monsterName, float hp, 
            float atk, float dex, float luk, float critical, 
            float critcalDamage, int speed, int dropGold, float dropExp)
        {
            this.monsterId = monsterId;
            this.monsterName = monsterName;
            this.hp = hp;
            this.atk = atk;
            this.dex = dex;
            this.luk = luk;
            this.critical = critical;
            this.critcalDamage = critcalDamage;
            this.speed = speed;
            this.dropGold = dropGold;
            this.dropExp = dropExp;
        }
    }
}
