using Sylphyr.YJH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sylphyr
{
    public class Weapon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Stat { get; set; }
        public int Value { get; set; }
        public string Slot { get; set; }
        public int Price { get; set; }
        public string Desc { get; set; }
        public bool WpurChase { get; set; } = false;
        public bool WisEquip { get; set; } = false;
        public Weapon()
        {

        }

        public Weapon(int id, string name, int stat, int value, string slot, int price, string desc)
        {
            Id = id;
            Name = name;
            Stat = stat;
            Value = value;
            Slot = slot;
            Price = price;
            Desc = desc;
        }

        public Weapon(int id, string name, int stat, int value, string slot, int price, string desc, bool wpurChase, bool wisEquip)
        {
            Id = id;
            Name = name;
            Stat = stat;
            Value = value;
            Slot = slot;
            Price = price;
            Desc = desc;
            WpurChase = wpurChase;
            WisEquip = wisEquip;
        }
    }
}
