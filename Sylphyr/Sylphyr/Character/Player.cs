using System.Text;
using Sylphyr.Dungeon;
using Sylphyr.Scene;
using Sylphyr.Utils;
using Sylphyr.YJH;
using static Sylphyr.Character.CharacterStat;

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
    private CharacterSkillData[] Skills;
    public List<CharacterSkillData> learnedSkills { get; } = new();

    // Player Info
    public string Name { get; }
    public int Level { get; private set; }
    public float CurrentHp { get; private set; }
    public float CurrentMp { get; private set; }
    public int Exp { get; private set; }
    public int Gold { get; private set; } = 0;
    
    //Player Best Stage
    public int BestStage { get; private set; }

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
        statusSb.AppendLine($" {Name} ( {Class.GetClassName()} )");
        statusSb.AppendLine($" HP: {CurrentHp}/{TotalStat.MaxHp}");
        statusSb.AppendLine($" MP: {CurrentMp}/{TotalStat.MaxMp}");
        statusSb.AppendLine($" Exp: {Exp}/{LevelData.GetExp(Level)}");
        statusSb.AppendLine($" 골드: {Gold} G");
        statusSb.AppendLine();
        statusSb.AppendLine($" 공격력: {TotalStat.Atk}");
        statusSb.AppendLine($" 방어력: {TotalStat.Def}");
        statusSb.AppendLine($" 속도: {TotalStat.Speed}");
        statusSb.AppendLine($" 민첩: {TotalStat.Dex}");
        statusSb.AppendLine($" 운: {TotalStat.Luk}");
        statusSb.AppendLine();
        statusSb.AppendLine($" 치명타 확률: {TotalStat.CriticalChance * 100}%");
        statusSb.AppendLine($" 치명타 대미지: {TotalStat.CriticalDamage * 10}%");
        statusSb.AppendLine();
        statusSb.AppendLine("[ 보유 스킬 ]");
        foreach (var skill in learnedSkills)
        {
            statusSb.AppendLine($" - {skill.SkillName} : {skill.Desc}");
        }
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

    public void TakeDamage(float damage)
    {
        float finalDamage = damage - (TotalStat.Def / (TotalStat.Def + 50f)) * 100f;
        if (finalDamage <= 0)
            finalDamage = 0;
        CurrentHp -= finalDamage;
    }

    public void AddExp(int exp)
    {
        Exp += exp;
        
        // 레벨업 체크
        int levelUpExp = LevelData.GetExp(Level);
        if (Exp >= levelUpExp)
        {
            LevelUp(Exp - levelUpExp);
        }
    }

    private void LevelUp(int remainExp)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"레벨이 올랐습니다! Lv.{Level + 1}이 되었습니다.");
        Console.ResetColor();
        
        Level++;
        
        switch (Class)
        {
            case CharacterClass.Thief:
                AddStat(9.5f, 5, 1.6f, 0.95f, 1.0f, 0.75f, 3, 0.018f, 0.01f);
                break;
            case CharacterClass.Archer:
                AddStat(9.5f, 5, 1.6f, 0.95f, 1, 0.75f, 3, 0.018f, 0.01f);
                break;
            case CharacterClass.Warrior:
                AddStat(15, 5, 1.5f, 1.2f, 1, 0.75f, 3, 0.01f, 0.01f);
                break;
            case CharacterClass.Paladin:
                AddStat(10.5f, 5, 1.55f, 1.05f, 1, 0.79f, 3, 0.009f, 0.02f);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(Class), Class, null);
        }

        foreach (var skill in Skills)
        {
            if (skill.AcquisitionLevel == Level)
            {
                learnedSkills.Add(skill);
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine($"스킬을 배웠습니다! {skill.SkillName} 습득!");
                Console.ResetColor();
            }
        }
        
        if (remainExp > 0)
        {
            AddExp(remainExp);
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
        GameManager.Instance.GameOver();
        
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("사망하였습니다...");
        Console.ResetColor();
        
        Console.WriteLine("press any key to continue...");
        Console.ReadKey();
        TitleScene.Instance.Run();
    }
    public void SetBestStage(int stage)
    {
        BestStage = stage;
    }
    
    public SaveData ToSaveData() {
        return new SaveData {
            Name = Name,
            CharacterClass = Class.ToString(),
            Level = Level,
            CurrentHp = CurrentHp,
            CurrentMp = CurrentMp,
            Exp = Exp,
            Gold = Gold,
            BaseStat = new CharacterStatData {
                Strength = BaseStat.Atk,
                Dexterity = BaseStat.Dex,
                Intelligence = BaseStat.Luk,
                Vitality = BaseStat.Def
            },
            EnhancedStat = new CharacterStatData {
                Strength = EnhancedStat.Atk,
                Dexterity = EnhancedStat.Dex,
                Intelligence = EnhancedStat.Luk,
                Vitality = EnhancedStat.Def
            }
        };
    }
}