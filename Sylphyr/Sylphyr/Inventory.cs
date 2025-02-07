using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sylphyr.KJE;

namespace Sylphyr
{
    public class Inventory
    {   
        // 인벤토리에 담을 아이템 리스트
        public List<Item> invenitems = new List<Item>();


        // 한글과 영문의 너비를 맞춰주는 함수
        static string AlignText(string text, int totalWidth)
        {
            int textWidth = text.Sum(c => (c >= 0xAC00 && c <= 0xD7A3) ? 2 : 1); // 한글 2칸, 영문 1칸
            return text + new string(' ', totalWidth - textWidth);
        }


        // 인벤토리에서 아이템 추가
        public void AddItem(Item item)
        {
            invenitems.Add(item);
            Console.WriteLine($"{item.Name}이(가) 인벤토리에 추가되었습니다.");
        }

        // 인벤토리에서 아이템 제거
        public void RemoveItem(Item item)
        {
            invenitems.Remove(item);
            Console.WriteLine($"{item.Name}이(가) 인벤토리에서 제거되었습니다.");
        }



        // 인벤토리 메뉴
        public void invenDisplay(Player player, bool isfail = false)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("인벤토리");
                Console.WriteLine("보유한 아이템을 관리할 수 있습니다.");
                Console.WriteLine();


                Console.WriteLine("     " + AlignText("아이템명", 15) + " | " + "       " +
                                  AlignText("스탯", 10) + " " + " | " +
                                  AlignText("슬롯", 10) + " | " +
                                  AlignText("가격", 7) + " | " +
                                  "설명");

                Console.WriteLine(new string('-', 110));

                if (invenitems.Count == 0)
                {
                    Console.WriteLine("인벤토리가 비어 있습니다.");
                }


                foreach (var item in invenitems)
                {
                    string slotname = "";
                    string statname = "";

                    // 장비 부위 구분
                    if (item.Slot == "0")
                        slotname = "상의";
                    else if (item.Slot == "1")
                        slotname = "하의";
                    else if (item.Slot == "2")
                        slotname = "신발";
                    else
                        slotname = "장신구";

                    // 스텟 구분
                    if (item.Stat == 0) statname = "크리데미지";
                    else if (item.Stat == 1) statname = "민첩";
                    else if (item.Stat == 2) statname = "행운";
                    else if (item.Stat == 3) statname = "방어력";

                    string equippedText = item.isEquip ? "[E] " : "";

                    Console.WriteLine($"- {equippedText}" + AlignText(item.Name, 15) + " | " +
                                          AlignText(statname, 10) + "   " +
                                          AlignText("+" + item.Stat2, 5) + " | " +
                                          AlignText(slotname, 10) + " | " +
                                          AlignText(item.Price + "G", 7) + " | " +
                                          item.Desc);

                    //Console.WriteLine($"- {item.Name} | {statname} +{item.Stat2} | {slotname}  | 가격: {item.Price}G | 설명: {item.Desc}");
                }



                Console.WriteLine();
                Console.WriteLine("1. 장착 관리");
                Console.WriteLine("0. 나가기");
                Console.WriteLine();

                if (isfail)
                {
                    Console.WriteLine("다시 입력해주세요.");
                    isfail = false;
                }

                int input = GetInput(0, 1);

                switch (input)
                {
                    case -1:
                        isfail = true;
                        break;
                    case 0:
                        return;
                    case 1:
                        EquipDisplay(player);
                        break;
                }

            }


        }


        // 장비 착용 해제 메뉴
        public void EquipDisplay(Player player, bool isfail = false)   
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("인벤토리 - 장착 관리");
                Console.WriteLine();

                // 테이블 헤더 출력
                Console.WriteLine("     " + AlignText("아이템명", 15) + " | " + "       " +
                                  AlignText("스탯", 10) + " " + " | " +
                                  AlignText("슬롯", 10) + " | " +
                                  AlignText("가격", 7) + " | " +
                                  "설명");

                Console.WriteLine(new string('-', 110));

                if (invenitems.Count == 0)
                {
                    Console.WriteLine("인벤토리가 비어 있습니다.");
                }


                bool inputfail = false;

                int i = 1;  // 아이템 장착 해제 번호

                foreach (var item in invenitems)
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
                    else statname = "방어력";

                    string equippedText = item.isEquip ? "[E] " : "";

                    Console.WriteLine($"- {i}. {equippedText}" + AlignText(item.Name, 15) + " | " +
                                          AlignText(statname, 10) + "   " +
                                          AlignText("+" + item.Stat2, 5) + " | " +
                                          AlignText(slotname, 10) + " | " +
                                          AlignText(item.Price + "G", 7) + " | " +
                                          item.Desc);

                    // Console.WriteLine($"- {i}. {item.Name} | {statname} +{item.Stat2} | {slotname} | 가격: {item.Price}G | 설명: {item.Desc}");
                    i++;
                }

                Console.WriteLine();
                Console.WriteLine("0. 나가기");

                if (isfail)
                {
                    Console.WriteLine();
                    Console.WriteLine("다시 입력해주세요.");
                    isfail = false;
                }

                int input = GetInput(0, invenitems.Count);

                Item selectedItem = null;

                if (input >= 1 && input <= invenitems.Count)
                {
                    selectedItem = invenitems[input - 1]; // 유효한 범위 내에서 선택
                                                          // selectedItem에 대해 필요한 작업을 수행
                }


                switch (input)
                {
                    case -1:
                        isfail = true;
                        break;
                    case 0:
                        return;
                    default:
                        if (!selectedItem.isEquip)
                        {
                            selectedItem.isEquip = true;  // 아이템 장착
                            switch (selectedItem.Stat)
                            {
                                case 0:
                                    player.EnhancedStat.CriticalDamage += selectedItem.Stat2;
                                    break;
                                case 1:
                                    player.EnhancedStat.Dex += selectedItem.Stat2;
                                    break;
                                case 2:
                                    player.EnhancedStat.Luk += selectedItem.Stat2;
                                    break;
                                case 3:
                                    player.EnhancedStat.Def += selectedItem.Stat2;
                                    break;
                            }
                        }
                        else
                        {
                            selectedItem.isEquip = false;  // 아이템 해제
                            switch (selectedItem.Stat)
                            {
                                case 0:
                                    player.EnhancedStat.CriticalDamage -= selectedItem.Stat2;
                                    break;
                                case 1:
                                    player.EnhancedStat.Dex -= selectedItem.Stat2;
                                    break;
                                case 2:
                                    player.EnhancedStat.Luk -= selectedItem.Stat2;
                                    break;
                                case 3:
                                    player.EnhancedStat.Def -= selectedItem.Stat2;
                                    break;
                            }
                        }
                        break;
                }
            }
        }

















        private static int GetInput(int min, int max)
        {
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.WriteLine(">> ");
            if(int.TryParse(Console.ReadLine(), out int input) && input >= min && input <= max) return input;
            else return -1;
        }
    }
}
