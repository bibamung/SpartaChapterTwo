namespace Sylphyr.YJH;
using Player = Sylphyr.Character.Player;
using CharacterStat = Sylphyr.Character.CharacterStat;
using Saving = Sylphyr.YJH.Save;

public class SaveData
{
    public string Name;
    public string CharacterClass;
    public int Level;
    public float CurrentHp;
    public float CurrentMp;
    public int Exp;
    public int Gold;
    public CharacterStatData BaseStat; // BaseStat 저장
    public CharacterStatData EnhancedStat; // EnhancedStat 저장
    
    //인벤토리정보
    public List<ItemData> Items; // 인벤토리에서 아이템 저장
    public List<ItemData> Weapons; // 무기 저장
    public List<ItemData> Potions; // 포션 저장

}

// CharacterStat 데이터를 변환하기 위한 데이터 구조
public class CharacterStatData
{
    public float Strength;
    public float Dexterity;
    public float Intelligence;
    public float Vitality;
}

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



