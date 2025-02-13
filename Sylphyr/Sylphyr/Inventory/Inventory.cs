using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Reflection.Emit;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Guild;
using Sylphyr.Character;
using Sylphyr.YJH;

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

        public Inventory() { }
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

                    if (weaponEquip.Count > 0 && weaponEquip[0].WisEquip)
                    {
                        if (weaponItem.Desc == weaponEquip[0].Desc)
                        {
                            equippedText1 = "E";
                            iswee = true;
                        }
                        else
                        {
                            weaponItem.WisEquip = false;
                        }
                    }
                    
                    if (weaponItem.WisEquip && iswee)
                    {
                        // 정렬된 텍스트 출력
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"- [{AlignText(equippedText1, 1)}] " + AlignText(weaponItem.Name, 24) + " | " +
                                          AlignText(statname, 10) + "   " +
                                          AlignText("+" + weaponItem.Value, 5) + " | " +
                                          AlignText(weaponslot, 8) + " | " +
                                          weaponItem.Desc);
                        Console.ResetColor();
                        iswee = false;
                    }
                    else
                    {
                        // 정렬된 텍스트 출력
                        Console.WriteLine("- " + AlignText(weaponItem.Name, 28) + " | " +
                                          AlignText(statname, 10) + "   " +
                                          AlignText("+" + weaponItem.Value, 5) + " | " +
                                          AlignText(weaponslot, 8) + " | " +
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
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"- [{AlignText(equippedText, 1)}] " + AlignText(item.Name, 24) + " | " +
                                              AlignText(statname, 10) + "   " +
                                              AlignText("+" + (statname == "크리데미지" ? ((float)item.Value / 10) + "%" : item.Value), 5) + " | " +
                                              AlignText(slotname, 8) + " | " +
                                              item.Desc);
                        Console.ResetColor();
                        iseee = false;
                    }
                    else
                    {
                        Console.WriteLine($"- " + AlignText(item.Name, 28) + " | " +
                                              AlignText(statname, 10) + "   " +
                                              AlignText("+" + (statname == "크리데미지" ? ((float)item.Value / 10) + "%" : item.Value), 5) + " | " +
                                              AlignText(slotname, 8) + " | " +
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

                   
                    // 정렬된 텍스트 출력
                    Console.WriteLine("- " + AlignText(potionItem.Name, 28) + " | " +
                                        AlignText(hpmp, 10) + "   " +
                                        AlignText("+" + potionItem.Value, 5) + " | " +
                                        potionItem.Desc);
                    
                }


                Console.WriteLine();
                Console.WriteLine("1. 장착 관리");
                Console.WriteLine("2. 포션 사용");
                Console.WriteLine("0. 나가기");
                Console.WriteLine();

                if (isfail)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("다시 입력해주세요.");
                    Console.ResetColor();
                    isfail = false;
                }

                int input = GetInput(0, 2);

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
                    case 2:
                        ConsumeDisplay(player);
                        break;
                }

            }
        }


        // 장비 착용 해제 메뉴
        public void EquipDisplay(Player player, bool isfail = false, bool noneweapon = false)   
        {
            while (true)
            {
                int i = 1;  // 아이템 장착 해제 번호
                int index = 0;
                bool iseee = false;  // 방어구 장착 표시를 정함
                bool iswee = false;  // 무기 장착 표시를 정함
                bool choosepotion;
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
                    Console.WriteLine("아이템이 비어 있습니다.");
                }


                foreach (var weaponItem in invenweapons)
                {
                    string statname = "공격력";
                    string weaponslot = "무기";


                    if (weaponEquip.Count > 0 && weaponEquip[0].WisEquip)
                    {
                        if (weaponItem.Id == weaponEquip[0].Id)
                        {
                            equippedText1 = "E";
                            iswee = true;
                        }
                        else
                        {
                            weaponItem.WisEquip = false;
                            iswee = false;
                        }
                    }



                    if (iswee)
                    {
                        // 정렬된 텍스트 출력
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"- {i}. [{AlignText(equippedText1, 1)}] " + AlignText(weaponItem.Name, 25) + " | " +
                                          AlignText(statname, 10) + "   " +
                                          AlignText("+" + weaponItem.Value, 5) + " | " +
                                          AlignText(weaponslot, 7) + " | " +
                                          weaponItem.Desc);
                        Console.ResetColor();
                    }
                    else
                    {
                        // 정렬된 텍스트 출력
                        Console.WriteLine($"- {i}. " + AlignText(weaponItem.Name, 29) + " | " +
                                          AlignText(statname, 10) + "   " +
                                          AlignText("+" + weaponItem.Value, 5) + " | " +
                                          AlignText(weaponslot, 7) + " | " +
                                          weaponItem.Desc);
                    }
                    i++;
                }

                Console.WriteLine();
                Console.WriteLine(new string('-', 110));
                Console.WriteLine("방어구");

                if (invenitems.Count == 0)
                {
                    Console.WriteLine("아이템이 비어 있습니다.");
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
                    
                    if (itemsEquip.Count > 0 && item.isEquip)
                    {
                        equippedText = "E";
                        iseee = true;
                    }
                    else
                    {
                        item.isEquip = false;
                        iseee = false;
                    }
                    

                    if (iseee)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"- {i}. [{AlignText(equippedText, 1)}] " + AlignText(item.Name, 25) + " | " +
                                              AlignText(statname, 10) + "   " +
                                              AlignText("+" + (statname == "크리데미지" ? ((float)item.Value / 10) + "%" : item.Value), 5) + " | " +
                                              AlignText(slotname, 7) + " | " +
                                              item.Desc);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine($"- {i}. " + AlignText(item.Name, 29) + " | " +
                                              AlignText(statname, 10) + "   " +
                                              AlignText("+" + (statname == "크리데미지" ? ((float)item.Value / 10) + "%" : item.Value), 5) + " | " +
                                              AlignText(slotname, 7) + " | " +
                                              item.Desc);
                    }
                    i++;
                    index++;
                }


                Console.WriteLine();
                Console.WriteLine("0. 나가기");

                if (isfail)
                {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("다시 입력해주세요.");
                    Console.ResetColor();
                    isfail = false;
                }
                

                int input = GetInput(0, invenitems.Count + invenpotions.Count + invenweapons.Count);

                Weapon selectedWeapon = null;
                Item selectedItem = null;

                if (input >= 1 && input <= invenweapons.Count && invenweapons.Count != 0)
                {
                    selectedWeapon = invenweapons[input - 1];

                }
                else if (input > invenweapons.Count && input <= invenweapons.Count + invenitems.Count && invenitems.Count != 0)
                {
                    selectedItem = invenitems[input - invenweapons.Count - 1]; // 유효한 범위 내에서 선택
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
                            if (!selectedWeapon.WisEquip)
                            {
                                /*
                                if (!noneweapon)
                                {
                                    selectedWeapon.wisEquip = true;
                                    weaponEquip.Add(selectedWeapon);
                                    player.EnhancedStat.Atk += selectedWeapon.Value;
                                    noneweapon = true;
                                }
                                else
                                {*/
                                selectedWeapon.WisEquip = true;
                                if (weaponEquip.Count != 0) player.EnhancedStat.Atk -= weaponEquip[0].Value;
                                player.EnhancedStat.Atk += selectedWeapon.Value;

                                weaponEquip.Clear();
                                weaponEquip.Add(selectedWeapon);
                                noneweapon = true;

                            }
                            else
                            {
                                if (selectedWeapon.Id != weaponEquip[0].Id)
                                {
                                    selectedWeapon.WisEquip = true;
                                    player.EnhancedStat.Atk -= weaponEquip[0].Value;
                                    player.EnhancedStat.Atk += selectedWeapon.Value;

                                    weaponEquip.Clear();
                                    weaponEquip.Add(selectedWeapon);
                                }
                                else
                                {
                                    selectedWeapon.WisEquip = false;
                                    player.EnhancedStat.Atk -= weaponEquip[0].Value;
                                    weaponEquip.Clear();
                                }
                            }
                            
                        }

                        else if (selectedItem != null)
                        {
                            // 기존 방어구 부위와 새로 선택한 아이템 부위 비교
                            bool isReplaced = false;
                            bool isskip = false;

                            if (itemsEquip.Count > 0)
                            {
                                foreach (var equippedItem in itemsEquip)
                                {
                                    if (selectedItem.ID != equippedItem.ID)
                                    {
                                        if (equippedItem.Slot == selectedItem.Slot)  // 동일 부위 비교
                                        {
                                            selectedItem.isEquip = true;

                                            // 기존 아이템 스텟 감소
                                            if (equippedItem.Stat == 0) player.EnhancedStat.CriticalDamage -= (equippedItem.Value / 1000f);
                                            else if (equippedItem.Stat == 1) player.EnhancedStat.Dex -= (equippedItem.Value / 10f);
                                            else if (equippedItem.Stat == 2) player.EnhancedStat.Luk -= equippedItem.Value;
                                            else if (equippedItem.Stat == 3) player.EnhancedStat.Def -= equippedItem.Value;

                                            // 새 아이템으로 교체
                                            equippedItem.isEquip = false;  // 기존 아이템 장착 해제
                                            selectedItem.isEquip = true;   // 새 아이템 장착

                                            // 새 아이템 스텟 증가
                                            if (selectedItem.Stat == 0) player.EnhancedStat.CriticalDamage += (selectedItem.Value / 1000f);
                                            else if (selectedItem.Stat == 1) player.EnhancedStat.Dex += (selectedItem.Value / 10f);
                                            else if (selectedItem.Stat == 2) player.EnhancedStat.Luk += selectedItem.Value;
                                            else if (selectedItem.Stat == 3) player.EnhancedStat.Def += selectedItem.Value;

                                            // 교체된 아이템을 장착 목록에 추가
                                            itemsEquip.Remove(equippedItem);
                                            itemsEquip.Add(selectedItem);

                                            isReplaced = true;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        selectedItem.isEquip = false;
                                        if (equippedItem.Stat == 0) player.EnhancedStat.CriticalDamage -= (selectedItem.Value / 1000f);
                                        else if (equippedItem.Stat == 1) player.EnhancedStat.Dex -= (selectedItem.Value / 10f);
                                        else if (equippedItem.Stat == 2) player.EnhancedStat.Luk -= selectedItem.Value;
                                        else if (equippedItem.Stat == 3) player.EnhancedStat.Def -= selectedItem.Value;
                                        itemsEquip.Remove(equippedItem);
                                        isskip = true;
                                        break;
                                    }
                                }
                            }


                            if (!isReplaced && !isskip)
                            {
                                selectedItem.isEquip = true;
                                // 해당 부위에 아무것도 없으면 그냥 장착
                                if (selectedItem.Stat == 0)
                                {
                                    itemsEquip.Add(selectedItem);
                                    player.EnhancedStat.CriticalDamage += (selectedItem.Value / 1000f);
                                }
                                else if (selectedItem.Stat == 1)
                                {
                                    itemsEquip.Add(selectedItem);
                                    player.EnhancedStat.Dex += (selectedItem.Value / 10f);
                                }
                                else if (selectedItem.Stat == 2)
                                {
                                    itemsEquip.Add(selectedItem);
                                    player.EnhancedStat.Luk += selectedItem.Value;
                                }
                                else if (selectedItem.Stat == 3)
                                {
                                    itemsEquip.Add(selectedItem);
                                    player.EnhancedStat.Def += selectedItem.Value;
                                }
                            }
                        }
                        break;
                }
            }
        }


        public void ConsumeDisplay(Player player, bool isfail = false)
        {
            while (true)
            {
                int i = 1;
                bool choosepotion;
                Console.Clear();
                Console.WriteLine("인벤토리 - 포션");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine(new string('-', 110));
                Console.WriteLine("포션");

                if (invenpotions.Count == 0)
                {
                    Console.WriteLine("아이템이 비어 있습니다.");
                }

                foreach (var potionItem in invenpotions)
                {
                    string hpmp = "";  // HPMP 구분
                    if (potionItem.Stat == 0) hpmp = "HP";
                    else hpmp = "MP";

                    if (!potionItem.IsBuy)
                    {
                        // 정렬된 텍스트 출력
                        Console.WriteLine($"- {i}. " + AlignText(potionItem.Name, 15) + " | " +
                                          AlignText(hpmp, 10) + "   " +
                                          AlignText("+" + potionItem.Value, 5) + " | " +
                                          potionItem.Desc);
                    }
                    else
                    {
                        // 정렬된 텍스트 출력
                        Console.WriteLine($"- {i}. " + AlignText(potionItem.Name, 15) + " | " +
                                          AlignText(hpmp, 10) + "   " +
                                          AlignText("+" + potionItem.Value, 5) + " | " +
                                          potionItem.Desc);
                    }
                    i++;
                }


                Console.WriteLine();
                Console.WriteLine("0. 나가기");


                if (isfail)
                {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("다시 입력해주세요.");
                    Console.ResetColor();
                    isfail = false;
                }

                int input = GetInput(0, invenitems.Count + invenpotions.Count + invenweapons.Count);

                Potion selectedPotion = null;

                if (input >= 1 && input <= invenpotions.Count) selectedPotion = invenpotions[input - 1];
                else isfail = true;

                switch (input)
                {
                    case -1:
                        isfail = true;
                        break;
                    case 0:
                        return;
                    default:
                        var isUsed = false;
                        if (selectedPotion.Stat == 0) player.UseItem(true, selectedPotion.Value, out isUsed);
                        else if (selectedPotion.Stat == 1) player.UseItem(false, selectedPotion.Value, out isUsed);
                        
                        if (!isUsed)
                        {
                            Thread.Sleep(500);
                            break;
                        }
                        
                        invenpotions[input].count--;
                        invenpotions.Remove(selectedPotion);
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



        public void EquipItem(Item item)
        {
            if (item.Stat == 0) GameManager.Instance.player.EnhancedStat.CriticalDamage += (item.Value / 1000f);
            else if (item.Stat == 1) GameManager.Instance.player.EnhancedStat.Dex += (item.Value / 10f);
            else if (item.Stat == 2) GameManager.Instance.player.EnhancedStat.Luk += item.Value;
            else if (item.Stat == 3) GameManager.Instance.player.EnhancedStat.Def += item.Value;
        }
        public void EquipWeapon(Weapon item)
        {
            GameManager.Instance.player.EnhancedStat.Atk += item.Value;
        }

        public List<WeaponData> ToWeaponData()
        {
            List<WeaponData> w = new List<WeaponData>();
            foreach (var item in invenweapons)
            {
                w.Add(new WeaponData(item.Id, item.Name, item.Stat, item.Value, item.Slot, item.Price, item.Desc, item.WisEquip, item.WpurChase));
            }
            return w;//invenweapons.Select(item => new WeaponData(item.Id, item.Name, item.Stat,item.Value, item.Slot, item.Price, item.Desc, item.WisEquip, item.WpurChase)).ToList();
        }
        
        public List<ItemData> ToItemData()
        {
            List<ItemData> i = new List<ItemData>();
            foreach (var item in invenitems)
            {
                i.Add(new ItemData(item.ID, item.Name, item.Stat, item.Value, item.Slot, item.Price, item.Desc,item.isEquip, item.purChase));
            }
            return i;//invenitems.Select(item => new ItemData(item.ID, item.Name, item.Stat,item.Value, item.Slot, item.Price, item.Desc, item.isEquip, item.purChase)).ToList();
        }
        
        public List<PotionData> ToPotionData()
        {
            List<PotionData> i = new List<PotionData>();
            foreach (var item in invenpotions)
            {
                i.Add(new PotionData(item.Id, item.Name, item.Stat, item.Value, item.Price, item.Desc, item.IsBuy, item.IsUse));
            }
            return i;
            //return invenpotions.Select(item => new PotionData(item.Id, item.Name, item.Stat, item.Value, item.Price, item.Desc, item.IsBuy, item.IsUse)).ToList();
        }
        
        public List<int> ToEquipWeapon()
        {
            List<int>equippedWeapon = new List<int>();
            
            foreach (var waepon in weaponEquip )
            {
                equippedWeapon.Add(waepon.Id);
            }

            return equippedWeapon;
        }

        public List<int> ToEquipItem()
        {
            List<int>equippedItem = new List<int>();

            foreach (var item in itemsEquip)
            {
                equippedItem.Add(item.ID);
            }
            return equippedItem;
        }



        
        List<Item> Equipmentlist = DataManager.Instance.equipmentItems;
        List<Potion> Potionlist = DataManager.Instance.consumeItems;
        List<Weapon> Weaponlist = DataManager.Instance.weaponItem;

        public Item GetEquipmentItems(int id)
        {
            Item item;
            item = Equipmentlist.SingleOrDefault(i => i.ID == id)!;
            Item i = new Item(item.ID, item.Name, item.Stat, item.Value, item.Slot, item.Price, item.Desc, item.purChase, item.isEquip);
            return i;
        }
        public Potion GetPotion(int id)
        {
            Potion potion;
            potion = Potionlist.SingleOrDefault(i => i.Id == id)!;
            Potion p = new Potion(potion.Id, potion.Name, potion.Stat, potion.Value, potion.Price, potion.Desc, potion.IsBuy, potion.IsUse);
            return p;
        }
        public Weapon GetWeapon(int id)
        {
            Weapon weapon;
            weapon = Weaponlist.SingleOrDefault(i => i.Id == id)!;
            Weapon w = new Weapon(weapon.Id, weapon.Name, weapon.Stat, weapon.Value, weapon.Slot, weapon.Price, weapon.Desc, weapon.WpurChase, weapon.WisEquip);
            return w;
        }

        public void InitializeInventory(GameData gameData)
        {
            if (gameData == null)
            {
                Console.WriteLine("GameData가 없습니다. 초기화에 실패했습니다.");
                return;
            }

            foreach (var item in gameData.Items)
            {
                invenitems.Add(new Item(item.Id, item.Name, item.Stat, item.Value, item.Slot, item.Price,item.Desc,item.PurChase,item.IsEquip));
                
            }
            foreach (var item in gameData.Weapons)
            {
                invenweapons.Add(new Weapon(item.Id, item.Name, item.Stat, item.Value, item.Slot, item.Price, item.Desc, item.WPurChase, item.WIsEquip));
                
            }
            foreach (var item in gameData.Potions)
            {
                invenpotions.Add(new Potion(item.Id, item.Name, item.Stat, item.Value, item.Price, item.Desc, item.IsBuy, item.IsUse));
            }
            foreach (var item in invenitems)
            {
                if (item.isEquip)
                {
                    GameManager.Instance.inventory.itemsEquip.Add(item);
                    EquipItem(item);
                }
            }
            foreach (var item in invenweapons)
            {
                if (item.WisEquip)
                {
                    GameManager.Instance.inventory.weaponEquip.Add(item);
                    EquipWeapon(item);
                }
            }

            /*BaseStat = new CharacterStat();
            EnhancedStat = new CharacterStat();*/

            Console.WriteLine("Player가 GameData를 사용하여 성공적으로 초기화되었습니다.");
        }

    }
}
