using Sylphyr.YJH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sylphyr
{
    public class Potion
    {
        public int Id;
        public string Name;
        public int Stat;
        public int Value;
        public int Price;
        public string Desc;
        public bool IsBuy = false;
        public bool IsUse  = false;
        public int count = 0;

        public Potion() { }

        public Potion(int id, string name, int stat, int value, int price, string desc)
        {
            Id = id;
            Name = name;
            Stat = stat;
            Value = value;
            Price = price;
            Desc = desc;
        }

        public Potion(int id, string name, int stat, int value, int price, string desc, bool isbuy, bool isuse)
        {
            Id = id;
            Name = name;
            Stat = stat;
            Value = value;
            Price = price;
            Desc = desc;
            IsBuy = isbuy;
            IsUse = isuse;
        }
    }

}
