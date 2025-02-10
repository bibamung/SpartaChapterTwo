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
        public int Value { get; set; }
        public int Price { get; set; }
        public string Slot { get; set; }
        public string Desc { get; set; }
        public bool isEquip { get; set; }  // 장착 여부
        public bool purChase {  get; set; } // 구매 여부

        private Random rand = new Random();

        private List<Item> randomItem = new List<Item>();  // 랜덤 방어구
        private List<Potion> randomPotion = new List<Potion>(); // 랜덤 포션
        private List<Weapon> randomWeapon = new List<Weapon>();  // 랜덤 무기

        private static List<Item> ItemsList = new List<Item>();


        // 한글과 영문의 너비를 맞춰주는 함수
        static string AlignText(string text, int totalWidth)
        {
            int textWidth = text.Sum(c => (c >= 0xAC00 && c <= 0xD7A3) ? 2 : 1); // 한글 2칸, 영문 1칸
            return text + new string(' ', totalWidth - textWidth);
        }


        public Item(int id, string name, int stat, int value, string slot, int price, string desc, bool purchase , bool isequip)
        {
            ID = id;
            Name = name;
            Stat = stat;
            Value = value;
            Slot = slot;
            Price = price;
            Desc = desc;
            purChase = purchase;
            isEquip = isequip;
        }




        // 테스트용 아이템 추가
        public void addTestItems()  // 임시 작업
        {
            ItemsList = new List<Item>
            {
                new Item(1000, "낡은 로브", 2, 10, "0", 500, "줘도 안 입을 로브, 냄새도 나는 것 같다.", false , false),
                new Item(1001, "강철 각반", 1, 5, "1", 1500, "강한 검이다.", false, false),
                new Item(1002, "마법 신발", 3, 12, "2", 2000, "마법을 증폭시키는 지팡이.", false, false),
                new Item(1003, "가죽 반지", 0, 10, "3", 800, "가죽으로 만든 방어구.", false, false),
                new Item(1004, "낡은 반지", 0, 10, "3", 1000, "낡은 반지이다.", false, false)
            };
        }

        


        // 상점에서 아이템 리스트 출력
        public void shopScene(Player player, Inventory inventory, Potion potion, Weapon weapon, bool isfail = false)
        {
            int itemct = 3;    // 방어구 아이템 개수
            int weaponct = 3;  // 무기 아이템 개수
            int potionct = 2;  // 포션 아이템 개수

            var randomWeapons = weapon.weaponItem.OrderBy(x => rand.Next()).Take(3).ToList();
            var randomarmorItems = ItemsList
                      .Where(item => item.Slot == "0" || item.Slot == "1" || item.Slot == "2" || item.Slot == "3")
                      .OrderBy(x => rand.Next()).Take(3).ToList();

            // 랜덤한 포션 2개 선택
            var randomPotions = potion.potionItem.OrderBy(x => rand.Next()).Take(2).ToList();


            while (true)  // 상점 출력
            {
                randomItem.Clear();  // 기존 리스트를 비움
                randomPotion.Clear();
                randomWeapon.Clear();

                Console.Clear();
                Console.WriteLine("상점");
                Console.WriteLine("아이템을 사고 파는 곳\n");

                /*
                // 테이블 헤더 출력
                Console.WriteLine("  " + AlignText("아이템명", 15) + " | " + "       " +
                                  AlignText("스탯", 10) + " " + " | " +
                                  AlignText("슬롯", 10) + " | " +
                                  AlignText("가격", 7) + " | " +
                                  "설명");

                Console.WriteLine(new string('-', 110));
                */

                Console.WriteLine("무기");

                // 무기 아이템 목록을 출력
                for (int x = 0; x < weaponct; x++)  // 무기
                {
                    var weaponItem = randomWeapons[x];  // 랜덤 무기

                    randomWeapon.Add(weaponItem);  // 무기 목록에 추가

                    string statname = "공격력";
                    string weaponslot = "무기";

                    // 정렬된 텍스트 출력
                    if (!weaponItem.wpurChase)
                    Console.WriteLine("- " + AlignText(weaponItem.Name, 15) + " | " +
                                      AlignText(statname, 10) + "   " +
                                      AlignText("+" + weaponItem.Value, 5) + " | " +
                                      AlignText(weaponslot, 7) + " | " +
                                      AlignText(weaponItem.Price + "G", 7) + " | " +
                                      weaponItem.Desc);
                    else
                    {
                        Console.WriteLine("- " + AlignText(weaponItem.Name, 15) + " | " +
                                      AlignText(statname, 10) + "   " +
                                      AlignText("+" + weaponItem.Value, 5) + " | " +
                                      AlignText(weaponslot, 7) + " | " +
                                      AlignText(weaponItem.Price + "G", 7) + " | " +
                                      weaponItem.Desc + "구매 완료");
                    }
                }

                Console.WriteLine();
                Console.WriteLine(new string('-', 110));

                Console.WriteLine("방어구");

                

                // 아이템 목록을 번호를 붙여서 출력
                for (int x = 0; x < itemct; x++)     // 방어구
                {
                    var item = randomarmorItems[x];

                    randomItem.Add(item);  //

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
                    if (!item.purChase)
                    {
                        Console.WriteLine("- " + AlignText(item.Name, 15) + " | " +
                                          AlignText(statname, 10) + "   " +
                                          AlignText("+" + item.Value, 5) + " | " +
                                          AlignText(slotname, 7) + " | " +
                                          AlignText(item.Price + "G", 7) + " | " +
                                          item.Desc);
                    }
                    else
                    {
                        Console.WriteLine("- " + AlignText(item.Name, 15) + " | " +
                                         AlignText(statname, 10) + "   " +
                                         AlignText("+" + item.Value, 5) + " | " +
                                         AlignText(slotname, 7) + " | " +
                                         AlignText(item.Price + "G", 7) + " | " +
                                         item.Desc + "구매 완료");
                    }
                    //Console.WriteLine($"- {item.Name} | {statname} +{item.Value} | 슬롯: {slotname} | 가격: {item.Price}G | 설명: {item.Desc}");
                }


                
                Console.WriteLine();
                Console.WriteLine(new string('-', 110));
                Console.WriteLine("포션");

                for (int x = 0; x < potionct; x++)
                {
                    var potion1 = randomPotions[x];

                    randomPotion.Add(potion1);

                    string hpmp = "";  // HPMP 구분
                    if (potion1.Stat == 0) hpmp = "HP";
                    else hpmp = "MP";

                    // 정렬된 텍스트 출력
                    if (!potion1.isBuy)
                    {
                        Console.WriteLine("- " + AlignText(potion1.Name, 15) + " | " +
                                          AlignText(hpmp, 10) + "   " +
                                          AlignText("+" + potion1.Value, 5) + " | " +
                                          AlignText(potion1.Price + "G", 7) + " | " +
                                          potion1.Desc);
                    }
                    else
                    {
                        Console.WriteLine("- " + AlignText(potion1.Name, 15) + " | " +
                                          AlignText(hpmp, 10) + "   " +
                                          AlignText("+" + potion1.Value, 5) + " | " +
                                          AlignText(potion1.Price + "G", 7) + " | " +
                                          potion1.Desc + "구매 완료");
                    }
                }

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
        public void buyequipmentDisplay(Player player, Inventory inventory, bool isfail = false, bool ispurChase = false, bool needGold = false)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("상점 - 아이템 구매\n");
                
                /*
                // 테이블 헤더 출력
                Console.WriteLine("     " + AlignText("아이템명", 15) + " | " + "       " +
                                  AlignText("스탯", 10) + " " + " | " +
                                  AlignText("슬롯", 10) + " | " +
                                  AlignText("가격", 7) + " | " +
                                  "설명");
                Console.WriteLine(new string('-', 110));
                */

                int index = 1;  // 아이템 구매 번호

                Console.WriteLine("무기");
                foreach (var weaponItem in randomWeapon)
                {
                    string statname = "공격력";
                    string weaponslot = "무기";

                    if (!weaponItem.wpurChase)
                    {
                        // 정렬된 텍스트 출력
                        Console.WriteLine("- " + index + ". " + AlignText(weaponItem.Name, 15) + " | " +
                                          AlignText(statname, 10) + "   " +
                                          AlignText("+" + weaponItem.Value, 5) + " | " +
                                          AlignText(weaponslot, 7) + " | " +
                                          AlignText(weaponItem.Price + "G", 7) + " | " +
                                          weaponItem.Desc);
                        index++;
                    }
                    else
                    {
                        // 정렬된 텍스트 출력
                        Console.WriteLine("- " + index + ". " + AlignText(weaponItem.Name, 15) + " | " +
                                          AlignText(statname, 10) + "   " +
                                          AlignText("+" + weaponItem.Value, 5) + " | " +
                                          AlignText(weaponslot, 7) + " | " +
                                          AlignText(weaponItem.Price + "G", 7) + " | " +
                                          weaponItem.Desc + " 구매 완료");
                        index++;
                    }
                }


                Console.WriteLine();
                Console.WriteLine(new string('-', 110));
                Console.WriteLine("방어구");

                foreach (var item in randomItem)
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
                                          AlignText("+" + item.Value, 5) + " | " +
                                          AlignText(slotname, 7) + " | " +
                                          AlignText(item.Price + "G", 7) + " | " +
                                          item.Desc + " 구매 완료");
                    }
                    else
                    {
                        // 정렬된 텍스트 출력
                        Console.WriteLine("- " + index + ". " + AlignText(item.Name, 15) + " | " +
                                          AlignText(statname, 10) + "   " +
                                          AlignText("+" + item.Value, 5) + " | " +
                                          AlignText(slotname, 7) + " | " +
                                          AlignText(item.Price + "G", 7) + " | " +
                                          item.Desc);
                    }
                        //Console.WriteLine($"- {index}. {item.Name} | {statname} +{item.Value} | 슬롯: {slotname} | 가격: {item.Price}G | 설명: {item.Desc}");
                        index++;
                }

                Console.WriteLine();
                Console.WriteLine(new string('-', 110));
                Console.WriteLine("포션");

                foreach (var potionItem in randomPotion)
                {
                    string hpmp = "";  // HPMP 구분
                    if (potionItem.Stat == 0) hpmp = "HP";
                    else hpmp = "MP";

                    if (!potionItem.isBuy)
                    {
                        // 정렬된 텍스트 출력
                        Console.WriteLine("- " + index + ". " + AlignText(potionItem.Name, 15) + " | " +
                                          AlignText(hpmp, 10) + "   " +
                                          AlignText("+" + potionItem.Value, 5) + " | " +
                                          AlignText(potionItem.Price + "G", 7) + " | " +
                                          potionItem.Desc);
                    }
                    else
                    {
                        // 정렬된 텍스트 출력
                        Console.WriteLine("- " + index + ". " + AlignText(potionItem.Name, 15) + " | " +
                                          AlignText(hpmp, 10) + "   " +
                                          AlignText("+" + potionItem.Value, 5) + " | " +
                                          AlignText(potionItem.Price + "G", 7) + " | " +
                                          potionItem.Desc + " 구매 완료");
                    }
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
                else if (needGold)
                {
                    Console.WriteLine();
                    Console.WriteLine("골드가 부족합니다.");
                    needGold = false;
                }

                int input = GetInput(0, randomItem.Count + randomPotion.Count + randomWeapon.Count); // 번호 선택

                Item selectedItem = null;
                Weapon selectedWeapon = null;
                Potion selectedPotion = null;

                if (input >= 1 && input <= randomWeapon.Count)
                {
                    selectedWeapon = randomWeapon[input - 1];
                }
                else if (input > randomWeapon.Count && input <= randomWeapon.Count + randomItem.Count)
                {
                    selectedItem = randomItem[input - randomWeapon.Count - 1];
                }
                else if (input > randomWeapon.Count + randomItem.Count && input <= randomWeapon.Count + randomItem.Count + randomPotion.Count)
                {
                    selectedPotion = randomPotion[input - randomWeapon.Count - randomItem.Count - 1];
                }

                switch (input)
                {
                    case -1:
                        isfail = true;
                        break;
                    case 0:
                        return;
                    case 1:
                    case 2:
                    case 3:
                        if (selectedWeapon != null)
                        {
                            if (player.Gold >= selectedWeapon.Price && !selectedWeapon.wpurChase)
                            {
                                player.RemoveGold(selectedWeapon.Price);
                                inventory.AddWeapon(selectedWeapon);
                                selectedWeapon.wpurChase = true;
                            }
                            else if (selectedWeapon.wpurChase) ispurChase = true;
                            else if (player.Gold < selectedWeapon.Price) needGold = true;
                        }
                        break;
                    case 4:
                    case 5:
                    case 6:
                        if (selectedItem != null)
                        {
                            if (player.Gold >= selectedItem.Price && !selectedItem.purChase)
                            {
                                player.RemoveGold(selectedItem.Price);
                                inventory.AddItem(selectedItem);
                                selectedItem.purChase = true;
                            }
                            else if (selectedItem.purChase) ispurChase = true;
                            else if (player.Gold < selectedItem.Price) needGold = true;
                        }
                        break;
                    case 7:
                    case 8:
                    case 9:
                        if (selectedPotion != null)
                        {
                            if (player.Gold >= selectedPotion.Price && !selectedPotion.isBuy)
                            {
                                player.RemoveGold(selectedPotion.Price);
                                inventory.AddPotion(selectedPotion);
                                selectedPotion.isBuy = true;
                            }
                            else if (selectedPotion.isBuy) ispurChase = true;
                            else if (player.Gold >= selectedPotion.Price) needGold = true;
                        }
                        break;
                    default:
                        isfail = true;
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
                                      AlignText("+" + item.Value, 5) + " | " +
                                      AlignText(slotname, 10) + " | " +
                                      AlignText(item.Price + "G", 7) + " | " +
                                      item.Desc);

                    //Console.WriteLine($"- {index}. {item.Name} | {statname} +{item.Value} | 슬롯: {slotname} | 가격: {item.Price}G | 설명: {item.Desc}");
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
                            player.AddGold(getgold, false);
                            inventory.RemoveItem(selectedItem);
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
