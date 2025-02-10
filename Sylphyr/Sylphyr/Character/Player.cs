using Guild;
using System.Text;
using Sylphyr.YJH;

namespace Sylphyr.Character;

public class Player
{
    public StringBuilder statusSb { get; } = new();
    
    // Player Stat
    public CharacterClass Class { get; }
    public CharacterStat BaseStat { get; }
    public CharacterStat EnhancedStat { get; }
    
    // Player Level
    public CharacterLevelData LevelData { get; }
    
    // Player Skill
    public CharacterSkillData[] Skills { get; }
    public List<CharacterSkillData> learnedSkills { get; } = new();

    // Player Info
    public string Name { get; }
    public int Level { get; private set; }
    public float CurrentHp { get; private set; }
    public float CurrentMp { get; private set; }
    public int Exp { get; private set; }
    public int Gold { get; private set; }

    private CharacterStat totalStat = new CharacterStat();
    public CharacterStat TotalStat
    {
        get
        {
            totalStat.MaxHp = BaseStat.MaxHp + EnhancedStat.MaxHp;
            totalStat.MaxMp = BaseStat.MaxMp + EnhancedStat.MaxMp;
            totalStat.Atk = BaseStat.Atk + EnhancedStat.Atk;
            totalStat.Def = BaseStat.Def + EnhancedStat.Def;
            totalStat.Luk = BaseStat.Luk + EnhancedStat.Luk;
            totalStat.Dex = BaseStat.Dex + EnhancedStat.Dex;
            totalStat.Speed = BaseStat.Speed + EnhancedStat.Speed;
            totalStat.CriticalChance = BaseStat.CriticalChance + EnhancedStat.CriticalChance;
            totalStat.CriticalDamage = BaseStat.CriticalDamage + EnhancedStat.CriticalDamage;
            return totalStat;
        }
    }

    public Player(string name, CharacterClass charClass)
    {
        Name = name;
        Class = charClass;
        
        BaseStat = GetCharacterStat(charClass)!;
        EnhancedStat = new CharacterStat();
        LevelData = new CharacterLevelData();
        Skills = GetSkills();

        Level = 1;
        CurrentHp = BaseStat.MaxHp;
        CurrentMp = BaseStat.MaxMp;
        Exp = 0;
        Gold = 2000;
    }

    private CharacterStat? GetCharacterStat(CharacterClass charClass)
    {
        List<CharacterStat> statDatas = DataManager.Instance.characterStats;
        
        return charClass switch
        {
            CharacterClass.Warrior => statDatas.SingleOrDefault(stat => stat.Id == 1001),
            CharacterClass.Thief   => statDatas.SingleOrDefault(stat => stat.Id == 1002),
            CharacterClass.Archer  => statDatas.SingleOrDefault(stat => stat.Id == 1003),
            CharacterClass.Paladin => statDatas.SingleOrDefault(stat => stat.Id == 1004),
            _                      => throw new ArgumentOutOfRangeException(nameof(charClass), charClass, null)
        };
    }

    public void PrintStatus()
    {
        statusSb.Clear();
        statusSb.AppendLine($" Lv.{Level}");
        statusSb.AppendLine($" {Name} ( {Class} )");
        statusSb.AppendLine($"HP: {CurrentHp}/{BaseStat.MaxHp}");
        statusSb.AppendLine($"MP: {CurrentMp}/{BaseStat.MaxMp}");
        statusSb.AppendLine();
        statusSb.AppendLine($"공격력: {BaseStat.Atk}");
        statusSb.AppendLine($"방어력: {BaseStat.Def}");
        statusSb.AppendLine($"속도: {BaseStat.Speed}");
        statusSb.AppendLine($"민첩: {BaseStat.Dex}");
        statusSb.AppendLine($"운: {BaseStat.Luk}");
        statusSb.AppendLine();
        statusSb.AppendLine($"치명타 확률: {BaseStat.CriticalChance}");
        statusSb.AppendLine($"치명타 대미지: {BaseStat.CriticalDamage}");
        Console.Write(statusSb.ToString());
    }

    public void AddGold(int gold)
    {
        Gold += gold;
    }

    public void AddRewardGold(int gold, out int totalGold)
    {
        totalGold = gold;

        // 보상 1000G , Luk 3
        // 추가보상 = 1000 * 0.06 = 60G
        if (Class == CharacterClass.Thief)
        {
            float addGoldRate = BaseStat.Luk * 2 / 100;
            float addGold = gold * addGoldRate;
            totalGold = (int)(totalGold + addGold);
        }

        Gold += totalGold;
    }

    public void RemoveGold(int gold)
    {
        Gold -= gold;

        if (Gold < 0)
            Gold = 0;
    }

    public void Attack() //(Monster monster)
    {
        // 크리티컬 확률 계산
        Random rand = new Random();
        bool isCritical = rand.NextDouble() < TotalStat.CriticalChance;
        // float damageReduce = 100f *  (monster.Def / (monster.Def + 50f));
        float finalDamage = TotalStat.Atk; // * (1 - damageReduce / 100f);

        if (isCritical)
        {
            finalDamage *= TotalStat.CriticalDamage;
        }

        // 몬스터에게 데미지 주기
        if (finalDamage <= 0)
            finalDamage = 1;

        // monster.TakeDamage(damage);
    }

    public void TakeDamage(float damage)
    {
        Random rand = new Random();
        float evasionRate = 100f * (TotalStat.Dex / (TotalStat.Dex + 50f));

        if (rand.NextDouble() < evasionRate)
        {
            // TODO: 회피 성공
            return;
        }

        float finalDamage = damage - (TotalStat.Def / TotalStat.Def + 50f);
        if (finalDamage <= 0)
            finalDamage = 1;

        CurrentHp -= finalDamage;
    }

    public void AddExp(int exp)
    {
        Exp += exp;
        
        // 레벨업 체크
        int levelUpExp = LevelData.GetExp(Level);
        if (Exp >= levelUpExp)
        {
            Exp -= levelUpExp;
            LevelUp();
        }
    }

    // TODO: 머 올림?
    private void LevelUp()
    {
        Level++;
        
        switch (Class)
        {
            case CharacterClass.Thief:
                AddStat(10, 5, 2, 1, 1, 1, 1, 0.01f, 0.1f);
                break;
            case CharacterClass.Archer:
                break;
            case CharacterClass.Warrior:
                break;
            case CharacterClass.Paladin:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(Class), Class, null);
        }

        foreach (var skill in Skills)
        {
            if (skill.AcquisitionLevel == Level)
            {
                learnedSkills.Add(skill);
            }
        }

        return;

        void AddStat(float hp, int mp, float atk, float def, float luk, float dex, int speed, float criticalChance,
                     float criticalDamage)
        {
            EnhancedStat.MaxHp += hp;
            EnhancedStat.MaxMp += mp;
            EnhancedStat.Atk += atk;
            EnhancedStat.Def += def;
            EnhancedStat.Luk += luk;
            EnhancedStat.Dex += dex;
            EnhancedStat.Speed += speed;
            EnhancedStat.CriticalChance += criticalChance;
            EnhancedStat.CriticalDamage += criticalDamage;
        }
    }

    public CharacterSkillData[] GetSkills()
    {
        var skillDatas = DataManager.Instance.characterSkills;
        var skills = new CharacterSkillData[4];
        int index = 0;
        for (int i = 0; i < skillDatas.Count; i++)
        {
            if (skillDatas[i].CharacterClass == Class)
            {
                skills[index++] = skillDatas[i];
            }
        }

        return skills;
    }

    public void UseMp(int useMp)
    {
        if (CurrentMp > useMp)
        {
            CurrentMp -= useMp;
        }
        else
        {
            Console.WriteLine("마나가 없습니다.");
        }
    }


    public void Dead()
    {
        // 타이틀로 돌아가기;
    }

    public void UseItem(bool isHealth, float value)
    {
        if (isHealth) // hp
        {
            CurrentHp += value;
            if (CurrentHp > TotalStat.MaxHp)
                CurrentHp = TotalStat.MaxHp;
            
        }
        else // mp
        {
            CurrentMp += value;
            if (CurrentMp > TotalStat.MaxMp)
                CurrentMp = TotalStat.MaxMp;
        }
    }

    // 장비 착용하기?
    public void Equip() { }

    public void UnEquip() { }
}