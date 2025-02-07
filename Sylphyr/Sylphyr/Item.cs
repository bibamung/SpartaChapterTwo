using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sylphyr
{
    public class Item
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Stat { get; set; }
        public int Stat2 { get; set; }
        public int Price { get; set; }
        public string Slot { get; set; }
        public string Desc { get; set; }
        public bool isEquip { get; set; }  // 장착 여부

        private static Dictionary<int, Item> ItemsDictionary = new Dictionary<int, Item>();


        public Item(int id, string name, int stat, int stat2, string slot, int price, string desc, bool isequip)
        {
            ID = id;
            Name = name;
            Stat = stat;
            Stat2 = stat2;
            Slot = slot;
            Price = price;
            Desc = desc;
            isEquip = isequip;
        }


        // 테스트용 아이템 추가
        public void addTestItems()
        {
            ItemsDictionary = new Dictionary<int, Item>
            {
                { 1000, new Item(1000, "낡은 로브", 2, 10, "상의", 500, "줘도 안 입을 로브, 냄새도 나는 것 같다.", false) },
                { 1001, new Item(1001, "강철 검", 10, 5, "무기", 1500, "강한 검이다.", false) },
                { 1002, new Item(1002, "마법 지팡이", 8, 12, "하의", 2000, "마법을 증폭시키는 지팡이.", false) },
                { 1003, new Item(1003, "가죽 방어구", 5, 10, "장신구", 800, "가죽으로 만든 방어구.", false) }
            };
        }

        // 상점에서 아이템 리스트 출력
        public void shopScene(Player player, Inventory inventory, bool isfail = false)
        {
            Console.Clear();
            Console.WriteLine("상점\n");
            Console.WriteLine("[아이템 목록]\n");

            // 아이템 목록을 번호를 붙여서 출력
            foreach (var item in ItemsDictionary.Values)
            {
                string slotname = "";
                string statname = "";
                // 장비 부위 구분
                if (item.Slot == "0") slotname = "상의";
                else if (item.Slot == "1") slotname = "하의";
                else if (item.Slot == "2") slotname = "신발";
                else slotname = "장신구";

                // 스텟 구분
                if (item.Stat == 0) statname = "크리데미지";
                else if (item.Stat == 1) statname = "민첩";
                else if (item.Stat == 2) statname = "행운";
                else if (item.Stat == 3) statname = "방어력";


                Console.WriteLine($"- {item.Name} | {statname} +{item.Stat2} | 슬롯: {slotname} | 가격: {item.Price}G | 설명: {item.Desc}");
            }

            // 메뉴 선택
            Console.WriteLine("\n1. 아이템 구매");
            Console.WriteLine("2. 아이템 판매");
            Console.WriteLine("0. 나가기");


            int input = GetInput(0, 2);   // 행동값

            if (isfail) Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.");

            switch (input)
            {
                case 1:
                    BuyItem(player, inventory);
                    break;
                case 2:

                    break;
                case 0:
                    Console.WriteLine("상점을 나갑니다.");
                    return;
                default:
                    isfail = true;
                    break;
            }
        }

        // 아이템 구매 로직
        public void BuyItem(Player player, Inventory inventory, bool isfail = false)
        {
           
            Console.Clear();
            Console.WriteLine("상점\n");
            Console.WriteLine("[아이템 목록]\n");
            Console.WriteLine();
            
            int index = 1;  // 아이템 구매 번호

            foreach (var item in ItemsDictionary.Values)
            {
                string slotname = "";
                string statname = "";

                // 장비 부위 구분
                if (item.Slot == "0") slotname = "상의";
                else if (item.Slot == "1") slotname = "하의";
                else if (item.Slot == "2") slotname = "신발";
                else slotname = "장신구";

                // 스텟 구분
                if (item.Stat == 0) statname = "크리데미지";
                else if (item.Stat == 1) statname = "민첩";
                else if (item.Stat == 2) statname = "행운";
                else if (item.Stat == 3) statname = "방어력";

                Console.WriteLine($"- {index}. {item.Name} | {statname} +{item.Stat2} | 슬롯: {slotname} | 가격: {item.Price}G | 설명: {item.Desc}");
                index++;
            }
            Console.WriteLine("0. 나가기");

            if (isfail)
            {
                Console.WriteLine("다시 입력해주세요.");
                isfail = false;
            }

            int input = GetInput(0, ItemsDictionary.Count); // 번호 선택
            var selectedItem = ItemsDictionary.Values.ToList()[input - 1]; // 번호는 1부터 시작하므로 -1

            
            switch (input)
            {
                case -1:
                    isfail = true;
                    break;
                case 0:
                    return;
                default:
                    if (player.Gold >= selectedItem.Price)
                    {
                        player.Gold -= selectedItem.Price;
                        inventory.AddItem(selectedItem);
                        
                    }
                    else isfail = true;
                    break;
            }
        }
    







        // 사용자 입력 받기
        private static int GetInput(int min, int max)
        {
            int input;
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.WriteLine(">> ");
                if (int.TryParse(Console.ReadLine(), out input) && (input < min) && (input > max)) return input;

            }
        }
    }
}
