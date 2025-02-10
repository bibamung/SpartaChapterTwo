﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Guild;
using Sylphyr.Character;

namespace Sylphyr
{
    public class Inventory
    {   
        // 인벤토리에 담을 아이템 리스트
        public List<Item> invenitems = new List<Item>();
        public List<Weapon> invenweapons = new List<Weapon>();
        public List<Potion> invenpotions = new List<Potion>();
        public List<Weapon> weaponEquip = new List<Weapon>();
        public List<Item> itemsEquip = new List<Item>();

        static string AlignText(string text, int totalWidth)
        {
            int textWidth = text.Sum(c => (c >= 0xAC00 && c <= 0xD7A3) ? 2 : 1); // 한글 2칸, 영문 1칸

            // 만약 textWidth가 totalWidth보다 크다면, totalWidth를 초과한 부분은 제거하여 균등하게 맞추도록.
            if (textWidth > totalWidth)
                return text.Substring(0, totalWidth);

            // 여유공간을 공백으로 채운 후 반환
            return text + new string(' ', totalWidth - textWidth);
        }


        // 인벤토리에서 아이템 추가
        public void AddItem(Item item)
        {
            invenitems.Add(item);
        }

        // 인벤토리에서 아이템 제거
        public void RemoveItem(Item item)
        {
            invenitems.Remove(item);
        }

        public void AddWeapon(Weapon weapon)
        {
            invenweapons.Add(weapon);
        }

        public void RemoveWeapon(Weapon weapon)
        {
            invenweapons.Remove(weapon);
        }

        public void AddPotion(Potion potion)
        {
            invenpotions.Add(potion);
        }

        public void RemovePotion(Potion potion)
        {
            invenpotions.Remove(potion);
        }


        // 인벤토리 메뉴
        public void invenDisplay(Player player, bool isfail = false)
        {
            while (true)
            {
                bool iseee = false;  // 방어구 장착 표시를 정함
                bool iswee = false;  // 무기 장착 표시를 정함

                string equippedText = null;   // 방어구
                string equippedText1 = null;  // 무기
                

                Console.Clear();
                Console.WriteLine("인벤토리");
                Console.WriteLine("보유한 아이템을 관리할 수 있습니다.");
                Console.WriteLine();

                /*
                // 테이블 헤더 출력
                Console.WriteLine("         " + AlignText("아이템명", 15) + " | " + "       " +
                                  AlignText("스탯", 10) + " " + " | " +
                                  AlignText("슬롯", 10) + " | " +
                                  AlignText("가격", 7) + " | " +
                                  "설명");

                Console.WriteLine(new string('-', 110));
                */
                Console.WriteLine("무기");

                if (invenweapons.Count == 0)
                {
                    Console.WriteLine("인벤토리가 비어 있습니다.");
                }

                foreach (var weaponItem  in invenweapons)
                {
                    string statname = "공격력";
                    string weaponslot = "무기";

                    if (weaponEquip.Count > 0 && weaponEquip[0].wisEquip && weaponItem.Desc == weaponEquip[0].Desc)
                    {
                        equippedText1 = "E";
                        iswee = true;
                    }

                    if (weaponItem.wisEquip && iswee)
                    {
                        // 정렬된 텍스트 출력
                        Console.WriteLine($"- [{AlignText(equippedText1, 1)}] " + AlignText(weaponItem.Name, 18) + " | " +
                                          AlignText(statname, 10) + "   " +
                                          AlignText("+" + weaponItem.Value, 5) + " | " +
                                          AlignText(weaponslot, 8) + " | " +
                                          AlignText(weaponItem.Price + "G", 7) + " | " +
                                          weaponItem.Desc);
                        iswee = false;
                    }
                    else
                    {
                        // 정렬된 텍스트 출력
                        Console.WriteLine("- " + AlignText(weaponItem.Name, 22) + " | " +
                                          AlignText(statname, 10) + "   " +
                                          AlignText("+" + weaponItem.Value, 5) + " | " +
                                          AlignText(weaponslot, 8) + " | " +
                                          AlignText(weaponItem.Price + "G", 7) + " | " +
                                          weaponItem.Desc);
                    }
                }

                Console.WriteLine();
                Console.WriteLine(new string('-', 110));
                Console.WriteLine("방어구");

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
                    else statname = "방어력";

                    if (item.isEquip)
                    {
                        equippedText = "E";
                        iseee = true;
                    }

                    if (item.isEquip && iseee)
                    {
                        Console.WriteLine($"- [{AlignText(equippedText, 1)}] " + AlignText(item.Name, 18) + " | " +
                                              AlignText(statname, 10) + "   " +
                                              AlignText("+" + item.Value, 5) + " | " +
                                              AlignText(slotname, 8) + " | " +
                                              AlignText(item.Price + "G", 7) + " | " +
                                              item.Desc);
                        iseee = false;
                    }
                    else
                    {
                        Console.WriteLine($"- " + AlignText(item.Name, 22) + " | " +
                                              AlignText(statname, 10) + "   " +
                                              AlignText("+" + item.Value, 5) + " | " +
                                              AlignText(slotname, 8) + " | " +
                                              AlignText(item.Price + "G", 7) + " | " +
                                              item.Desc);
                    }

                    //Console.WriteLine($"- {item.Name} | {statname} +{item.Value} | {slotname}  | 가격: {item.Price}G | 설명: {item.Desc}");
                }


                Console.WriteLine();
                Console.WriteLine(new string('-', 110));
                Console.WriteLine("포션");

                if (invenpotions.Count == 0)
                {
                    Console.WriteLine("인벤토리가 비어 있습니다.");
                }

                foreach (var potionItem in invenpotions)
                {
                    string hpmp = "";  // HPMP 구분
                    if (potionItem.Stat == 0) hpmp = "HP";
                    else hpmp = "MP";

                    if (!potionItem.isBuy)
                    {
                        // 정렬된 텍스트 출력
                        Console.WriteLine("- " + AlignText(potionItem.Name, 15) + " | " +
                                          AlignText(hpmp, 10) + "   " +
                                          AlignText("+" + potionItem.Value, 5) + " | " +
                                          AlignText(potionItem.Price + "G", 8) + " | " +
                                          potionItem.Desc);
                    }
                    else
                    {
                        // 정렬된 텍스트 출력
                        Console.WriteLine("- " + AlignText(potionItem.Name, 15) + " | " +
                                          AlignText(hpmp, 10) + "   " +
                                          AlignText("+" + potionItem.Value, 5) + " | " +
                                          AlignText(potionItem.Price + "G", 8) + " | " +
                                          potionItem.Desc);
                    }
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
        public void EquipDisplay(Player player, bool isfail = false, bool noneweapon = false, bool ishigh = false, bool islow = false, bool isboots = false, bool isacc = false)   
        {
            while (true)
            {
                int i = 1;  // 아이템 장착 해제 번호
                int index = 0;
                bool iseee = false;  // 방어구 장착 표시를 정함
                bool iswee = false;  // 무기 장착 표시를 정함
                string equippedText = null;   // 방어구 [E]
                string equippedText1 = null;  // 무기   [E]

                Console.Clear();
                Console.WriteLine("인벤토리 - 장착 관리");
                Console.WriteLine();

                /*
                // 테이블 헤더 출력
                Console.WriteLine("         " + AlignText("아이템명", 15) + " | " + "       " +
                                  AlignText("스탯", 10) + " " + " | " +
                                  AlignText("슬롯", 10) + " | " +
                                  AlignText("가격", 7) + " | " +
                                  "설명");

                Console.WriteLine(new string('-', 110));
                */

                Console.WriteLine("무기");

                if (invenweapons.Count == 0)
                {
                    Console.WriteLine("인벤토리가 비어 있습니다.");
                }


                foreach (var weaponItem in invenweapons)
                {
                    string statname = "공격력";
                    string weaponslot = "무기";

                    
                    if (weaponEquip.Count > 0 && weaponEquip[0].wisEquip && weaponItem.Desc == weaponEquip[0].Desc)
                    {
                        equippedText1 = "E";
                        iswee = true;
                    }
                    

                    else iswee = false;

                    if (iswee)
                    {
                        // 정렬된 텍스트 출력
                        Console.WriteLine($"- {i}. [{AlignText(equippedText1, 1)}] " + AlignText(weaponItem.Name, 18) + " | " +
                                          AlignText(statname, 10) + "   " +
                                          AlignText("+" + weaponItem.Value, 5) + " | " +
                                          AlignText(weaponslot, 7) + " | " +
                                          AlignText(weaponItem.Price + "G", 7) + " | " +
                                          weaponItem.Desc);
                    }
                    else
                    {
                        // 정렬된 텍스트 출력
                        Console.WriteLine($"- {i}." + AlignText(weaponItem.Name, 23) + " | " +
                                          AlignText(statname, 10) + "   " +
                                          AlignText("+" + weaponItem.Value, 5) + " | " +
                                          AlignText(weaponslot, 7) + " | " +
                                          AlignText(weaponItem.Price + "G", 7) + " | " +
                                          weaponItem.Desc);
                    }
                    i++;
                }

                Console.WriteLine();
                Console.WriteLine(new string('-', 110));
                Console.WriteLine("방어구");

                if (invenitems.Count == 0)
                {
                    Console.WriteLine("인벤토리가 비어 있습니다.");
                }


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

                    if (item.isEquip)
                    {
                        equippedText = "E";
                        iseee = true;
                    }
                    else iseee = false;

                    if (iseee)
                    {
                        Console.WriteLine($"- {i}. [{AlignText(equippedText, 1)}] " + AlignText(item.Name, 18) + " | " +
                                              AlignText(statname, 10) + "   " +
                                              AlignText("+" + item.Value, 5) + " | " +
                                              AlignText(slotname, 7) + " | " +
                                              AlignText(item.Price + "G", 7) + " | " +
                                              item.Desc);
                    }
                    else
                    {
                        Console.WriteLine($"- {i}. " + AlignText(item.Name, 22) + " | " +
                                              AlignText(statname, 10) + "   " +
                                              AlignText("+" + item.Value, 5) + " | " +
                                              AlignText(slotname, 7) + " | " +
                                              AlignText(item.Price + "G", 7) + " | " +
                                              item.Desc);
                    }
                    i++;
                    index++;
                }


                Console.WriteLine();
                Console.WriteLine(new string('-', 110));
                Console.WriteLine("포션");

                if (invenpotions.Count == 0)
                {
                    Console.WriteLine("인벤토리가 비어 있습니다.");
                }

                foreach (var potionItem in invenpotions)
                {
                    string hpmp = "";  // HPMP 구분
                    if (potionItem.Stat == 0) hpmp = "HP";
                    else hpmp = "MP";

                    if (!potionItem.isBuy)
                    {
                        // 정렬된 텍스트 출력
                        Console.WriteLine($"- {i}. " + AlignText(potionItem.Name, 15) + " | " +
                                          AlignText(hpmp, 10) + "   " +
                                          AlignText("+" + potionItem.Value, 5) + " | " +
                                          AlignText(potionItem.Price + "G", 7) + " | " +
                                          potionItem.Desc);
                    }
                    else
                    {
                        // 정렬된 텍스트 출력
                        Console.WriteLine($"- {i}. " + AlignText(potionItem.Name, 15) + " | " +
                                          AlignText(hpmp, 10) + "   " +
                                          AlignText("+" + potionItem.Value, 5) + " | " +
                                          AlignText(potionItem.Price + "G", 7) + " | " +
                                          potionItem.Desc);
                    }
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
                

                int input = GetInput(0, invenitems.Count + invenpotions.Count + invenweapons.Count);

                Weapon selectedWeapon = null;
                Item selectedItem = null;
                Potion selectedPotion = null;

                if(input >= 1 && input <= invenweapons.Count)
                {
                    selectedWeapon = invenweapons[input - 1];
                    
                }
                else if (input > invenweapons.Count && input <= invenweapons.Count + invenitems.Count)
                {
                    selectedItem = invenitems[input - invenweapons.Count - 1]; // 유효한 범위 내에서 선택
                }
                else if (input > invenweapons.Count + invenitems.Count && input <= invenpotions.Count + invenweapons.Count + invenitems.Count)
                {
                    selectedPotion = invenpotions[input - invenweapons.Count - invenitems.Count - 1];
                }


                switch (input)
                {
                    case -1:
                        isfail = true;
                        break;
                    case 0:
                        return;
                    default:
                        if (selectedWeapon != null)
                        {
                            if (!selectedWeapon.wisEquip)
                            {
                                if (!noneweapon)
                                {
                                    selectedWeapon.wisEquip = true;
                                    weaponEquip.Add(selectedWeapon);
                                    player.EnhancedStat.Atk += selectedWeapon.Value;
                                    noneweapon = true;
                                }
                                else
                                {
                                    selectedWeapon.wisEquip = true;
                                    player.EnhancedStat.Atk -= weaponEquip[0].Value;
                                    player.EnhancedStat.Atk += selectedWeapon.Value;

                                    weaponEquip.Clear();
                                    weaponEquip.Add(selectedWeapon);
                                    noneweapon = true;
                                }
                            }
                            else
                            {
                                selectedWeapon.wisEquip = true;
                                player.EnhancedStat.Atk -= weaponEquip[0].Value;
                                player.EnhancedStat.Atk += selectedWeapon.Value;

                                weaponEquip.Clear();
                                weaponEquip.Add(selectedWeapon);
                            }
                        }

                        else if (selectedItem != null)
                        {
                            // 기존 방어구 부위와 새로 선택한 아이템 부위 비교
                            bool isReplaced = false;
                            foreach (var equippedItem in itemsEquip)
                            {
                                if (equippedItem.Slot == selectedItem.Slot)  // 동일 부위 비교
                                {
                                    // 기존 아이템 스텟 감소
                                    if (equippedItem.Stat == 0) player.EnhancedStat.CriticalDamage -= equippedItem.Value;
                                    else if (equippedItem.Stat == 1) player.EnhancedStat.Dex -= equippedItem.Value;
                                    else if (equippedItem.Stat == 2) player.EnhancedStat.Luk -= equippedItem.Value;
                                    else if (equippedItem.Stat == 3) player.EnhancedStat.Def -= equippedItem.Value;

                                    // 새 아이템으로 교체
                                    equippedItem.isEquip = false;  // 기존 아이템 장착 해제
                                    selectedItem.isEquip = true;   // 새 아이템 장착

                                    // 새 아이템 스텟 증가
                                    if (selectedItem.Stat == 0) player.EnhancedStat.CriticalDamage += selectedItem.Value;
                                    else if (selectedItem.Stat == 1) player.EnhancedStat.Dex += selectedItem.Value;
                                    else if (selectedItem.Stat == 2) player.EnhancedStat.Luk += selectedItem.Value;
                                    else if (selectedItem.Stat == 3) player.EnhancedStat.Def += selectedItem.Value;

                                    // 교체된 아이템을 장착 목록에 추가
                                    itemsEquip.Remove(equippedItem);
                                    itemsEquip.Add(selectedItem);

                                    isReplaced = true;
                                    break;
                                }
                            }

                            if (!isReplaced)
                            {
                                // 해당 부위에 아무것도 없으면 그냥 장착
                                if (selectedItem.Slot == "0" && !ishigh)
                                {
                                    ishigh = true;
                                    selectedItem.isEquip = true;
                                    itemsEquip.Add(selectedItem);
                                    player.EnhancedStat.CriticalDamage += selectedItem.Value;
                                }
                                else if (selectedItem.Slot == "1" && !islow)
                                {
                                    islow = true;
                                    selectedItem.isEquip = true;
                                    itemsEquip.Add(selectedItem);
                                    player.EnhancedStat.Dex += selectedItem.Value;
                                }
                                else if (selectedItem.Slot == "2" && !isboots)
                                {
                                    isboots = true;
                                    selectedItem.isEquip = true;
                                    itemsEquip.Add(selectedItem);
                                    player.EnhancedStat.Luk += selectedItem.Value;
                                }
                                else if (selectedItem.Slot == "3" && !isacc)
                                {
                                    isacc = true;
                                    selectedItem.isEquip = true;
                                    itemsEquip.Add(selectedItem);
                                    player.EnhancedStat.Def += selectedItem.Value;
                                }
                            }
                        }
                        break;
                    case 7:
                    case 8:
                    case 9:
                        /*if (selectedPotion.Stat == 0)   // Stat이 0이면 HP 회복  1이면 MP 회복
                        {
                            player.UseItem(selectedPotion.Value, 0);  // HP 포션 사용  -------------------------------
                        }
                        else player.UseItem(selectedPotion.Value, 1);  // MP 포션 사용 -------------------------------
                        */
                        break;
                }
            }
        }


        public void ItemEEE(Player player, Item selectedItem, Item[] item)
        {
            if (!item[0].isEquip)  // 아이템 장착
            {
                selectedItem.isEquip = true;
                item[0].isEquip = selectedItem.isEquip;
                selectedItem.isEquip = false;

                switch (selectedItem.Stat)
                {
                    case 0:
                        item[0].Value = selectedItem.Value;
                        player.EnhancedStat.CriticalDamage += selectedItem.Value;
                        break;
                    case 1:
                        item[0].Value = selectedItem.Value;
                        player.EnhancedStat.Dex += selectedItem.Value;
                        break;
                    case 2:
                        item[0].Value = selectedItem.Value;
                        player.EnhancedStat.Luk += selectedItem.Value;
                        break;
                    case 3:
                        item[0].Value = selectedItem.Value;
                        player.EnhancedStat.Def += selectedItem.Value;
                        break;
                }
            }
            else if (item[0].isEquip)  // 기존 상의 해제 후 장착
            {
                item[0].isEquip = false;
                selectedItem.isEquip = true;
                item[0].isEquip = selectedItem.isEquip;
                selectedItem.isEquip = false;

                switch (selectedItem.Stat)
                {
                    case 0:
                        player.EnhancedStat.CriticalDamage -= item[0].Value;
                        item[0].Value = selectedItem.Value;
                        player.EnhancedStat.CriticalDamage += selectedItem.Value;
                        break;
                    case 1:
                        player.EnhancedStat.Dex -= item[0].Value;
                        item[0].Value = selectedItem.Value;
                        player.EnhancedStat.Dex += selectedItem.Value;
                        break;
                    case 2:
                        player.EnhancedStat.Luk -= item[0].Value;
                        item[0].Value = selectedItem.Value;
                        player.EnhancedStat.Luk += selectedItem.Value;
                        break;
                    case 3:
                        player.EnhancedStat.Def -= item[0].Value;
                        item[0].Value = selectedItem.Value;
                        player.EnhancedStat.Def += selectedItem.Value;
                        break;
                }
            }
        }


        private static int GetInput(int min, int max)
        {
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">> ");
            if(int.TryParse(Console.ReadLine(), out int input) && input >= min && input <= max) return input;
            else return -1;
        }
    }
}
