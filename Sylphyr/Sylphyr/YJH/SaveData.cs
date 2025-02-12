using Newtonsoft.Json;
using Sylphyr.Character;

namespace Sylphyr.YJH;

using System.Linq;
using System.Collections.Generic;
using CharacterStat = Sylphyr.Character.CharacterStat;

public class SaveData
{
    public string Name { get; set; } // 플레이어 이름
    public string CharacterClass { get; set; } // 캐릭터 클래스
    public int Level { get; set; } // 플레이어 레벨
    public float CurrentHp { get; set; } // 현재 HP
    public float CurrentMp { get; set; } // 현재 MP
    public int Exp { get; set; } // 경험치
    public int Gold { get; set; } // 소지금
    public CharacterStatData BaseStat { get; set; } // 기본 스탯
    public CharacterStatData EnhancedStat { get; set; } // 강화된 스탯
    public List<ItemData> Items { get; set; } // 인벤토리에 저장된 방어구
    public List<WeaponData> Weapons { get; set; } // 저장된 무기
    public List<PotionData> Potions { get; set; } // 저장된 포션

    public List<int> purChaseweaponItem { get; set; } // 구매한 무기 정보

    public List<int> purChaseEquipmentItem { get; set; } // 구매한 방어구 정보

    public List<int> weaponEquip { get; set; } // 장비중인 무기

    public List<int> itemsEquip { get; set; } // 장비중인 방어구

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
            BaseStat = new CharacterStatData (10, 10, 10, 10),
            EnhancedStat = new CharacterStatData (10, 10, 10, 10),
            Items = new List<ItemData>(),
            Weapons = new List<WeaponData>(),
            Potions = new List<PotionData>(),
            purChaseweaponItem = new List<int>(),
            purChaseEquipmentItem = new List<int>(),
            weaponEquip = new List<int>(),
            itemsEquip = new List<int>()
        };
    }

    public void CreateSaveItemData()
    {
        Items = GameManager.Instance.inventory.ToItemData();
    }

    public void CreateSaveWeponData()
    {
        Weapons = GameManager.Instance.inventory.ToWeaponData();
    }

    public void CreateSavePotionData()
    {
        Potions = GameManager.Instance.inventory.ToPotionData();
    }


    // Debug 용 데이터 출력
    public void PrintData()
    {
        Console.WriteLine($"[Name: {Name}, Class: {CharacterClass}, Level: {Level}]");
        Console.WriteLine($"[HP: {CurrentHp}, MP: {CurrentMp}, Exp: {Exp}, Gold: {Gold}]");
        Console.WriteLine(
            $"[Base Stats] ATK: {BaseStat.Attack}, DEX: {BaseStat.Dex}, DEF: {BaseStat.Def}, LUK: {BaseStat.Luk}");
        Console.WriteLine(
            $"[Enhanced Stats] ATK: {EnhancedStat.Attack}, DEX: {EnhancedStat.Dex}, DEF: {EnhancedStat.Def}, LUK: {EnhancedStat.Luk}");
    }

    public void SavepurchaseItem()
    {
        purChaseweaponItem = GameManager.Instance.shop.ToPurChaseWeaponItem();
        purChaseEquipmentItem = GameManager.Instance.shop.ToPurChaseEquipmentItem();
    }

    public void SaveEquipItem()
    {
        weaponEquip = GameManager.Instance.inventory.ToEquipWeapon();
        itemsEquip = GameManager.Instance.inventory.ToEquipItem();
    }
}

// CharacterStat 데이터를 변환하기 위한 데이터 구조
public class CharacterStatData
{
    [JsonProperty] public float Attack { get; set; }
    [JsonProperty]public float Dex{ get; set; }
    [JsonProperty]public float Def{ get; set; }
    [JsonProperty]public float Luk{ get; set; }

    public CharacterStatData(float attack, float dex, float def, float luk )
    {
        Attack = attack;
        Dex = dex;
        Def = def;
        Luk = luk;   
    }
}

// Item 데이터를 저장하기 위한 별도 데이터 구조
public class ItemData
{
    [JsonProperty] public int Id{ get; set;}
    [JsonProperty] public string Name { get; set;}
    [JsonProperty] public int Stat { get; set;}
    [JsonProperty] public int Value { get; set;}
    [JsonProperty] public string Slot { get; set;}
    [JsonProperty] public int Price { get; set;}
    [JsonProperty] public string Desc { get; set;}
    [JsonProperty] public bool IsEquip { get; set;}
    [JsonProperty] public bool PurChase { get; set;}

    public ItemData(int id, string name, int stat, int value, string slot, int price, string desc, bool isEquip, bool purChase)
    {
        Id = id;
        Name = name;
        Stat = stat;
        Value = value;
        Slot = slot;
        Price = price;
        Desc = desc;
        IsEquip = isEquip;
        PurChase = purChase;
    }
}

public class WeaponData
{
    [JsonProperty] public int Id{ get; set;}
    [JsonProperty] public string Name { get; set;}
    [JsonProperty] public int Stat { get; set;}
    [JsonProperty] public int Value { get; set;}
    [JsonProperty] public string Slot { get; set;}
    [JsonProperty] public int Price { get; set;}
    [JsonProperty] public string Desc { get; set;}
    [JsonProperty] public bool wIsEquip { get; set;}
    [JsonProperty] public bool wPurChase { get; set;}

    public WeaponData(int id, string name, int stat, int value, string slot, int price, string desc, bool wisEquip,
        bool wpurChase)
    {
        Id = id;
        Name = name;
        Stat = stat;
        Value = value;
        Slot = slot;
        Price = price;
        Desc = desc;
        wIsEquip = wisEquip;
        wPurChase = wpurChase;
    }
}

public class PotionData
{
    [JsonProperty] public int Id{ get; set;}
    [JsonProperty] public string Name { get; set;}
    [JsonProperty] public int Stat { get; set;}
    [JsonProperty] public int Value { get; set;}
    [JsonProperty] public int Price { get; set;}
    [JsonProperty] public string Desc { get; set;}

    public PotionData(int id, string name, int stat, int value, int price, string desc)
    {
        Id = id;
        Name = name;
        Stat = stat;
        Value = value;
        Price = price;
        Desc = desc;
    }
}