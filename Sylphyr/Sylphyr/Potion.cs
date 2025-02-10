using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sylphyr
{
    public class Potion
    {
        public int ID { get; set; }
        public string Name { get; }
        public int Stat {  get; set; }
        public int Value { get; set; }
        public int Price { get; set; }
        public string Desc { get; set; }
        public bool isBuy { get; set; }
        public bool isUse { get; set; }
        public List<Potion> potionItem;

        public Potion(int id, string name, int stat, int value, int price, string desc, bool isbuy, bool isuse)
        {
            ID = id;
            Name = name;
            Stat = stat;
            Value = value;
            Price = price;
            Desc = desc;
            isBuy = isbuy;
            isUse = isuse;
        }

        public void addpotion()
        {
            potionItem = new List<Potion>
            {
                new Potion (1001, "일반 HP포션", 0, 20, 500, "맛없는 포션.", false, false),
                new Potion (1002, "상급 HP포션", 0, 30, 700, "약이 맛있는게 이상하지.", false, false),
                new Potion (1003, "고급 HP포션", 0, 40, 1000, "효과가 좋은 만큼 맛이 없다.", false, false),
                new Potion (1004, "최고급 HP포션", 0, 50, 2000, "맛있는 포션 성능이 좋은데 왜 맛있음?", false, false),
                new Potion (1005, "일반 MP포션", 1, 20, 500, "맛없는 포션.", false, false),
                new Potion (1006, "상급 MP포션", 1, 30, 700, "약이 맛있는게 이상하지.", false, false),
                new Potion (1007, "고급 MP포션", 1, 40, 1000, "효과가 좋은 만큼 맛이 없다.", false, false),
                new Potion (1008, "최고급 MP포션", 1, 50, 2000, "맛있는 포션 성능이 좋은데 왜 맛있음?", false, false)
            };
        }






    }
}
