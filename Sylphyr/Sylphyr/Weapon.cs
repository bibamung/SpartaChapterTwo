using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sylphyr
{
    public class Weapon(int id, string name, int stat, int Value, string slot, int price, string desc, bool wpurchase, bool isequip)
    {
        public int ID;
        public string Name;
        public int Stat;
        public int Value;
        public string Slot;
        public int Price;
        public string Desc;
        public bool wpurChase = wpurchase;
        public bool wisEquip = isequip;
    }
}
