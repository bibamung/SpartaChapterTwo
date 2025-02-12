using Sylphyr.Character;

namespace Sylphyr.YJH;

using System.Linq; 
using System.Collections.Generic;
using CharacterStat = Sylphyr.Character.CharacterStat; 

public class SaveData
{
    public string Name { get; set; }                         // 플레이어 이름
    public string CharacterClass{ get; set; }               // 캐릭터 클래스
    public int Level{ get; set; }                           // 플레이어 레벨
    public float CurrentHp{ get; set; }                     // 현재 HP
    public float CurrentMp{ get; set; }                     // 현재 MP
    public int Exp { get; set; }                            // 경험치
    public int Gold { get; set; }                            // 소지금
    public CharacterStatData BaseStat { get; set; }         // 기본 스탯
    public CharacterStatData EnhancedStat { get; set; }      // 강화된 스탯
    public List<ItemData> Items { get; set; }                // 인벤토리에 저장된 아이템
    public List<ItemData> Weapons { get; set; }              // 저장된 무기
    public List<ItemData> Potions { get; set; }              // 저장된 포션
    
    public List<int> purChaseweaponItem { get; set; }        // 구매한 무기 정보
    
    public List<int> purChaseEquipmentItem { get; set; }     // 구매한 방어구 정보

    // 기본 저장 데이터 초기화
    public static SaveData CreateDefault()
    {
        return new SaveData
        {
            Name = "Default_Name",
            CharacterClass = "Warrior",
            Level = 1,
            CurrentHp = 100.0f,
            CurrentMp = 50.0f,
            Exp = 0,
            Gold = 0,
            BaseStat = new CharacterStatData { Strength = 10, Dexterity = 5, Intelligence = 5, Vitality = 20 },
            EnhancedStat = new CharacterStatData { Strength = 12, Dexterity = 6, Intelligence = 6, Vitality = 25 },
            Items = new List<ItemData>(),
            Weapons = new List<ItemData>(),
            Potions = new List<ItemData>(),
            purChaseweaponItem = new List<int>(),
            purChaseEquipmentItem = new List<int>()
        };
    }
    
    // Debug 용 데이터 출력
    public void PrintData()
    {
        Console.WriteLine($"[Name: {Name}, Class: {CharacterClass}, Level: {Level}]");
        Console.WriteLine($"[HP: {CurrentHp}, MP: {CurrentMp}, Exp: {Exp}, Gold: {Gold}]");
        Console.WriteLine($"[Base Stats] STR: {BaseStat.Strength}, DEX: {BaseStat.Dexterity}, INT: {BaseStat.Intelligence}, VIT: {BaseStat.Vitality}");
        Console.WriteLine($"[Enhanced Stats] STR: {EnhancedStat.Strength}, DEX: {EnhancedStat.Dexterity}, INT: {EnhancedStat.Intelligence}, VIT: {EnhancedStat.Vitality}");
    }

    public void SavepurchaseItem()
    {
        purChaseweaponItem = GameManager.Instance.shop.ToPurChaseWeaponItem();
        purChaseEquipmentItem = GameManager.Instance.shop.ToPurChaseEquipmentItem();
    }
}

// CharacterStat 데이터를 변환하기 위한 데이터 구조
public class CharacterStatData
{
    public float Strength;
    public float Dexterity;
    public float Intelligence;
    public float Vitality;
}

// Item 데이터를 저장하기 위한 별도 데이터 구조
public class ItemData
{
    public int Id;
    public string Name;
    public int Stat;
    public string Slot;
    public int Price;
    public string Desc;
    public bool isEquip;
    public bool purChase;
}

