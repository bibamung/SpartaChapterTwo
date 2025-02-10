using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Sylphyr.Character;

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
        public bool purChase {  get; set; } // 구매 여부
        private Random rand = new Random();

        private List<int> randNums;
        private List<Item> randomItem;

        private static Dictionary<int, Item> ItemsDictionary = new Dictionary<int, Item>();


        // 한글과 영문의 너비를 맞춰주는 함수
        static string AlignText(string text, int totalWidth)
        {
            int textWidth = text.Sum(c => (c >= 0xAC00 && c <= 0xD7A3) ? 2 : 1); // 한글 2칸, 영문 1칸
            return text + new string(' ', totalWidth - textWidth);
        }


        public Item(int id, string name, int stat, int stat2, string slot, int price, string desc, bool purchase , bool isequip)
        {
            ID = id;
            Name = name;
            Stat = stat;
            Stat2 = stat2;
            Slot = slot;
            Price = price;
            Desc = desc;
            purChase = purchase;
            isEquip = isequip;
        }


        // 테스트용 아이템 추가
        public void addTestItems()  // 임시 작업
        {
            ItemsDictionary = new Dictionary<int, Item>
            {
                { 1000, new Item(1000, "낡은 로브", 2, 10, "상의", 500, "줘도 안 입을 로브, 냄새도 나는 것 같다.", false , false) },
                { 1001, new Item(1001, "강철 검", 1, 5, "무기", 1500, "강한 검이다.", false, false) },
                { 1002, new Item(1002, "마법 지팡이", 3, 12, "하의", 2000, "마법을 증폭시키는 지팡이.", false, false) },
                { 1003, new Item(1003, "가죽 방어구", 0, 10, "장신구", 800, "가죽으로 만든 방어구.", false, false) }
            };
        }







        // 상점에서 아이템 리스트 출력
        public void shopScene(Player player, Inventory inventory, bool isfail = false)
        {
            int itemct = 2;  // 상점 아이템 개수
            randNums = new List<int>();  // 중복 방지를 위한 리스트
            randomItem = new List<Item>();
            while (randNums.Count < itemct)   // 중복 방지
            {
                int randIndex = rand.Next(1000, 1004);  // 임시 작업
                if (!randNums.Contains(randIndex))  
                {
                    randNums.Add(randIndex);
                }
            }


            while (true)
            {
                randomItem.Clear();  // 기존 리스트를 비움
                Console.Clear();
                Console.WriteLine("상점");
                Console.WriteLine("아이템을 사고 파는 곳\n");

                Console.WriteLine("장비 - 상점\n");

                // 테이블 헤더 출력
                Console.WriteLine("  " + AlignText("아이템명", 15) + " | " + "       "+
                                  AlignText("스탯", 10) + " " +" | " +
                                  AlignText("슬롯", 10) + " | " +
                                  AlignText("가격", 7) + " | " +
                                  "설명");

                Console.WriteLine(new string('-', 110));

                // 아이템 목록을 번호를 붙여서 출력
                for (int x = 0; x < itemct; x++)
                {
                    var item = ItemsDictionary[randNums[x]];

                    randomItem.Add(item);

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

                    // 정렬된 텍스트 출력
                    Console.WriteLine("- " + AlignText(item.Name, 15) + " | " +
                                      AlignText(statname, 10)+"   " +
                                      AlignText("+" + item.Stat2, 5) + " | " +
                                      AlignText(slotname, 10) + " | " +
                                      AlignText(item.Price + "G", 7) + " | " +
                                      item.Desc);

                    //Console.WriteLine($"- {item.Name} | {statname} +{item.Stat2} | 슬롯: {slotname} | 가격: {item.Price}G | 설명: {item.Desc}");
                }

                Console.WriteLine();
                Console.WriteLine(new string('-', 110));
                Console.WriteLine();
                Console.WriteLine("포션 - 상점\n");



                // 메뉴 선택
                Console.WriteLine("\n1. 아이템 구매");
                Console.WriteLine("2. 아이템 판매");
                Console.WriteLine("0. 나가기");

                if (isfail)
                {
                    Console.WriteLine();
                    Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.");
                    isEquip = false;
                }

                int input = GetInput(0, 2);

                switch (input)
                {
                    case 1:
                        buyequipmentDisplay(player, inventory);
                        break;
                    case 2:
                        selltemDisplay(player, inventory);
                        break;
                    case 0:
                        return;
                    default:
                        isfail = true;
                        break;
                }
            }
        }

        // 아이템 구매 로직
        public void buyequipmentDisplay(Player player, Inventory inventory, bool isfail = false, bool ispurChase = false)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("상점 - 아이템 구매\n");
                // 테이블 헤더 출력
                Console.WriteLine("     " + AlignText("아이템명", 15) + " | " + "       " +
                                  AlignText("스탯", 10) + " " + " | " +
                                  AlignText("슬롯", 10) + " | " +
                                  AlignText("가격", 7) + " | " +
                                  "설명");
                
                Console.WriteLine(new string('-', 110));

                int itemct = 2; // 상점 아이템 개수
                int index = 1;  // 아이템 구매 번호

                foreach(var item in randomItem)
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
                    
                    if (item.purChase)
                    {
                        // 정렬된 텍스트 출력
                        Console.WriteLine("- " + index + ". " + AlignText(item.Name, 15) + " | " +
                                          AlignText(statname, 10) + "   " +
                                          AlignText("+" + item.Stat2, 5) + " | " +
                                          AlignText(slotname, 10) + " | " +
                                          AlignText(item.Price + "G", 7) + " | " +
                                          item.Desc + " 구매 완료");
                    }
                    else
                    {
                        // 정렬된 텍스트 출력
                        Console.WriteLine("- " + index + ". " + AlignText(item.Name, 15) + " | " +
                                          AlignText(statname, 10) + "   " +
                                          AlignText("+" + item.Stat2, 5) + " | " +
                                          AlignText(slotname, 10) + " | " +
                                          AlignText(item.Price + "G", 7) + " | " +
                                          item.Desc);
                    }
                        //Console.WriteLine($"- {index}. {item.Name} | {statname} +{item.Stat2} | 슬롯: {slotname} | 가격: {item.Price}G | 설명: {item.Desc}");
                        index++;
                }

                Console.WriteLine();
                Console.WriteLine("0. 나가기");

                if (isfail)
                {
                    Console.WriteLine();
                    Console.WriteLine("다시 입력해주세요.");
                    isfail = false;
                }
                else if (ispurChase)
                {
                    Console.WriteLine();
                    Console.WriteLine("이미 보유한 아이템 입니다.");
                    ispurChase = false;
                }

                int input = GetInput(0, randomItem.Count); // 번호 선택

                Item selectedItem = null;

                if (input >= 1 && input <= randomItem.Count)
                {
                    selectedItem = randomItem[input - 1];
                }

                switch (input)
                {
                    case -1:
                        isfail = true;
                        break;
                    case 0:
                        return;
                    default:
                        if (player.Gold >= selectedItem.Price && !selectedItem.purChase)
                        {
                            player.RemoveGold(selectedItem.Price);
                            inventory.AddItem(selectedItem);
                            selectedItem.purChase = true;
                        }
                        else if (selectedItem.purChase) ispurChase = true;
                        else isfail = true;
                        break;
                }
            }
        }



        public void selltemDisplay(Player player, Inventory inventory, bool isfail = false, bool isEquipMesege = false)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("상점 - 아이템 판매");
                Console.WriteLine();
                // 테이블 헤더 출력
                Console.WriteLine("     " + AlignText("아이템명", 15) + " | " + "       " +
                                  AlignText("스탯", 10) + " " + " | " +
                                  AlignText("슬롯", 10) + " | " +
                                  AlignText("가격", 7) + " | " +
                                  "설명");

                Console.WriteLine(new string('-', 110));

                if (inventory.invenitems.Count == 0)
                {
                    Console.WriteLine("인벤토리가 비어 있습니다.");
                }

                int index = 1;  // 아이템 구매 번호

                foreach (var item in inventory.invenitems)
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

                    string equippedText = item.isEquip ? "[E] " : "";

                    // 정렬된 텍스트 출력
                    Console.WriteLine($"- {index}. {equippedText}" +
                                      AlignText(item.Name, 15) + " | " +
                                      AlignText(statname, 10) + "   " +
                                      AlignText("+" + item.Stat2, 5) + " | " +
                                      AlignText(slotname, 10) + " | " +
                                      AlignText(item.Price + "G", 7) + " | " +
                                      item.Desc);

                    //Console.WriteLine($"- {index}. {item.Name} | {statname} +{item.Stat2} | 슬롯: {slotname} | 가격: {item.Price}G | 설명: {item.Desc}");
                    index++;
                }

                Console.WriteLine();
                Console.WriteLine("0. 나가기");

                if (isfail)
                {
                    Console.WriteLine();
                    Console.WriteLine("다시 입력해주세요.");
                    isfail = false;
                }
                
                else if (isEquipMesege)
                {
                    Console.WriteLine("장착 중인 아이템은 판매 불가능합니다.");
                    isEquipMesege = false;
                }

                int input = GetInput(0, inventory.invenitems.Count);

                Item selectedItem = null;

                if (input >= 1 && input <= inventory.invenitems.Count)
                {
                    selectedItem = inventory.invenitems[input - 1]; // 유효한 범위 내에서 선택
                }

                switch (input)
                {
                    case -1:
                        isfail = true;
                        break;
                    case 0:
                        return;
                    default:
                        if (!isEquip)
                        {
                            int getgold = (int)(selectedItem.Price * 0.8);
                            player.AddGold(getgold);
                            inventory.invenitems.Remove(selectedItem);
                        }
                        else isEquipMesege = true;
                        break;
                }
            }
        }







        // 사용자 입력 받기
        private static int GetInput(int min, int max)
        {


            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">> ");
            if (int.TryParse(Console.ReadLine(), out int input) && (input >= min) && (input <= max)) return input;
            else return -1;

        }
    }
}
