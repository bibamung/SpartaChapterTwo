namespace Sylphyr.Character;

public class CharacterSkillData
{
    public int Id;
    public string SkillName;
    public CharacterClass SharacterClass;
    public float Damage;
    public int SkillType;
    public int UseMp;
    public int AcquisitionLevel;
    public string Desc;
}

public enum SkillType
{
    Active,
    Passive
}