namespace Sylphyr.Character;

public class CharacterStatDatas
{
    public Dictionary<int, CharacterStat> Dict { get; }
    
    public CharacterStat GetCharacterStat(CharacterClass characterClass)
    {
        switch (characterClass)
        {
            case CharacterClass.Warrior:
                return Dict[0];
            case CharacterClass.Thief:
                return Dict[1];
            case CharacterClass.Archer:
                return Dict[2];
            case CharacterClass.Paladin:
                return Dict[3];
            default:
                throw new ArgumentOutOfRangeException(nameof(characterClass), characterClass, null);
        }
    }
}

public class CharacterStat
{
    public int Id;
    public string Name;
    public float MaxHp;
    public int MaxMp;
    public float Atk;
    public float Def;
    public float Luk;
    public float Dex;
    public int Speed;
    public float CriticalChance;
    public float CriticalDamage;
}

public enum CharacterClass
{
    Warrior = 1,
    Thief,
    Archer,
    Paladin,
}