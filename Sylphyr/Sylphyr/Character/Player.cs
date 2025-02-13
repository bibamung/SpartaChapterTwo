using System.Text;
using Sylphyr.Scene;
using Sylphyr.Utils;
using Sylphyr.YJH;

namespace Sylphyr.Character;

public class Player
{
    private StringBuilder statusSb { get; } = new();
    
    // Player Stat
    public CharacterClass Class { get; set; }
    public CharacterStat BaseStat { get; }
    public CharacterStat EnhancedStat { get; }

    // Player Level
    public CharacterLevelData LevelData { get; }
    
    // Player Skill
    private CharacterSkillData[] Skills;
    public List<CharacterSkillData> learnedSkills { get; } = new();

    // Player Info
    public string Name { get; set; }
    public int Level { get; private set; }
    public float CurrentHp { get; private set; }
    public float CurrentMp { get; private set; }
    public int Exp { get; private set; }
    public int Gold { get; private set; } = 0;
    
    // Dungeon Clear Info
    public int BestStage { get; set; }

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

        Gold = 5000;

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
        
        statusSb.AppendLine($" HP: {CurrentHp:N2} / {TotalStat.MaxHp:N2}");
        statusSb.AppendLine($" MP: {CurrentMp:N2} / {TotalStat.MaxMp:N2}");
        
        statusSb.AppendLine($" Exp: {Exp} / {LevelData.GetExp(Level)}");
        statusSb.AppendLine($" 골드: {Gold} G");
        statusSb.AppendLine();
        
        statusSb.Append($" 공격력: {TotalStat.Atk}");
        if (EnhancedStat.Atk > 0) statusSb.Append($" (+{EnhancedStat.Atk})");
        statusSb.AppendLine();
        statusSb.Append($" 방어력: {TotalStat.Def}");
        if (EnhancedStat.Def > 0) statusSb.Append($" (+{EnhancedStat.Def})");
        statusSb.AppendLine();
        statusSb.Append($" 속도: {TotalStat.Speed}");
        if (EnhancedStat.Speed > 0) statusSb.Append($" (+{EnhancedStat.Speed})");
        statusSb.AppendLine();
        statusSb.Append($" 민첩: {TotalStat.Dex}");
        if (EnhancedStat.Dex > 0) statusSb.Append($" (+{EnhancedStat.Dex})");
        statusSb.AppendLine();
        statusSb.Append($" 행운: {TotalStat.Luk}");
        if (EnhancedStat.Luk > 0) statusSb.Append($" (+{EnhancedStat.Luk})");
        statusSb.AppendLine();
        
        statusSb.AppendLine($" 치명타 확률: {TotalStat.CriticalChance * 100:N1}%");
        statusSb.Append($" 치명타 대미지: {TotalStat.CriticalDamage * 100:N1}%");
        if (EnhancedStat.CriticalDamage > 0) statusSb.Append($" (+{EnhancedStat.CriticalDamage * 100:N1}%)");
        statusSb.AppendLine();
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

    // 던전에서 사용 할 보상용 골드 추가 메서드
    public void AddRewardGold(int gold, out int totalGold)
    {
        totalGold = gold;

        // 도적의 경우 Luk 수치에 따라 추가 골드 획득
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
        
        // 레벨업 가능한 경험치가 아니라면 리턴
        int levelUpExp = LevelData.GetExp(Level);
        if (Exp < levelUpExp) return;
        
        int remainExp = Exp - levelUpExp;
        Exp = 0;
        LevelUp(remainExp);
    }

    private void LevelUp(int remainExp)
    {
        Level++;
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"레벨이 올랐습니다! Lv.{Level}이 되었습니다.");
        Console.ResetColor();
        
        // 직업별로 다른 스탯 증가
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
        
        CurrentHp = TotalStat.MaxHp;
        CurrentMp = TotalStat.MaxMp;

        foreach (var skill in Skills)
        {
            if (skill.AcquisitionLevel != Level) continue;
            
            learnedSkills.Add(skill);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"스킬을 배웠습니다! {skill.SkillName} 습득!");
            Console.ResetColor();
        }
        
        // 남은 경험치가 있다면 재귀적으로 레벨업
        if (remainExp > 0)
            AddExp(remainExp);

        return;

        void AddStat(float hp, int mp, float atk, float def, float luk, float dex, int speed, float criticalChance, float criticalDamage)
        {
            BaseStat.MaxHp += hp;
            BaseStat.MaxMp += mp;
            BaseStat.Atk += atk;
            BaseStat.Def += def;
            BaseStat.Luk += luk;
            BaseStat.Dex += dex;
            BaseStat.Speed += speed;
            BaseStat.CriticalChance += criticalChance;
            BaseStat.CriticalDamage += criticalDamage;
        }
    }

    // 데이터 매니저에서 직업에 맞는 스킬을 가져옴
    private CharacterSkillData[] GetSkills()
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
    
    // 소비아이템(포션) 사용
    public void UseItem(bool isHpPotion, float value, out bool isUsed)
    {
        isUsed = true;
        
        if (isHpPotion) // hp
        {
            if (CurrentHp == TotalStat.MaxHp)
            {
                isUsed = false;
                Console.WriteLine("이미 최대 체력입니다.");
                return;
            }
            
            CurrentHp += value;
            if (CurrentHp > TotalStat.MaxHp)
                CurrentHp = TotalStat.MaxHp;
            
        }
        else // mp
        {
            if (CurrentMp == TotalStat.MaxMp)
            {
                isUsed = false;
                Console.WriteLine("이미 최대 마나입니다.");
                return;
            }
            
            CurrentMp += value;
            if (CurrentMp > TotalStat.MaxMp)
                CurrentMp = TotalStat.MaxMp;
        }
    }

    public void Healing(float hp, float mp)
    {
        CurrentHp += hp;
        if (CurrentHp > TotalStat.MaxHp)
            CurrentHp = TotalStat.MaxHp;
                
        CurrentMp += mp;
        if (CurrentMp > TotalStat.MaxMp)
            CurrentMp = TotalStat.MaxMp;
    }

    public void UseMp(int useMp)
    {
        if (CurrentMp > useMp)
            CurrentMp -= useMp;
        else
            Console.WriteLine("마나가 없습니다.");
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


    public void InitializePlayer(GameData gameData)
    {
        Console.WriteLine("InitializePlayer 진입");
        if (gameData == null)
        {
            Console.WriteLine("GameData가 없습니다. 초기화에 실패했습니다.");
            return;
        }
        Console.WriteLine("if 스킵 성공");
        // GameData 데이터를 Player에 적용
        
        Name = gameData.Name;
        Class = Enum.Parse<CharacterClass>(gameData.CharacterClass);
        Level = gameData.Level;

        foreach (var skill in Skills)
        {
            if (skill.AcquisitionLevel <= Level)
            {
                if (!learnedSkills.Contains(skill))
                {
                    learnedSkills.Add(skill);
                }
            }
        }

        CurrentHp = gameData.CurrentHp;
        CurrentMp = gameData.CurrentMp;
        Exp = gameData.Exp;
        Gold = gameData.Gold;
        
        BestStage = gameData.BestStage;
        
        BaseStat.Atk = gameData.Atk;
        BaseStat.Dex = gameData.Dex;
        BaseStat.Def = gameData.Def;
        BaseStat.Luk = gameData.Luk;
        BaseStat.MaxHp = gameData.MaxHp;
        BaseStat.MaxMp = gameData.MaxMp;

        Console.WriteLine("Player가 GameData를 사용하여 성공적으로 초기화되었습니다.");
    }




    public SaveData ToSaveData() {
        return new SaveData
        {
            Name = Name,
            CharacterClass = Class.ToString(),
            Level = Level,
            CurrentHp = CurrentHp,
            CurrentMp = CurrentMp,
            Exp = Exp,
            Gold = Gold,
            Atk = BaseStat.Atk,
            Dex = BaseStat.Dex,
            Def = BaseStat.Def,
            Luk = BaseStat.Luk,
            MaxHp = BaseStat.MaxHp,
            MaxMp = BaseStat.MaxMp,
            BestStage = (BestStage == 0 ? 0 : BestStage)
        };
    }
}

