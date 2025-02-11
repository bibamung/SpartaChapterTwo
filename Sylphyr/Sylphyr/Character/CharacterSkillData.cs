namespace Sylphyr.Character;

public class CharacterSkillData
{
    public string SkillName;
    public CharacterClass CharacterClass;
    public float Damage;
    public int SkillType;
    public int UseMp;
    public int AcquisitionLevel;
    public string Desc;
    
    public bool IsLearned { get; private set; }
    
    public void LearnSkill()
    {
        IsLearned = true;
    }
}

public enum SkillType
{
    SingleTarget, // 단일 대상
    AreaTarget,   // 범위 대상
    Penetration,  // 관통
}