using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sylphyr
{
    public class Weapon
    {
        public int ID {  get; set; }
        public string Name { get; }
        public int Stat {  get; set; }
        public int Value { get; set; }
        public int Price { get; set; }
        public int Slot { get; set; }
        public string Desc { get; }
        public bool wpurChase { get; set; }
        public bool wisEquip { get; set; }
        public List<Weapon> weaponItem;

        public Weapon(int id, string name, int stat, int value, int price, int slot, string desc, bool wpurchase, bool isequip)
        {
            ID = id;
            Name = name;
            Stat = stat;
            Value = value;
            Price = price;
            Slot = slot;
            Desc = desc;
            wpurChase = wpurchase;
            wisEquip = isequip;
        }

        public void addWeapon()
        {
            weaponItem = new List<Weapon>
            {
                new Weapon (1000, "낡은 검", 4, 10, 500, 4, "부서지기 직전이다.", false, false),
                new Weapon (1001, "평범한 검", 4, 20, 1000, 4, "평범한 검.", false, false),
                new Weapon (1002, "고급 검", 4, 30, 2000, 4, "튼튼한 검.", false, false),
                new Weapon (1003, "최고급 검", 4, 40, 3000, 4, "실력에 비해 비싼 검.", false, false),
                new Weapon (1004, "화려한 검", 4, 50, 6000, 4, "검이 화려하다고 성능이 좋은건 아님.", false, false)
            };
        }
    }
}
