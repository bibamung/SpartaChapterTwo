using Guild;
using System.Text;

namespace Sylphyr.Character;

public class Player
{
    public string Name { get; }
    public CharacterClass Class { get; }
    public CharacterStat BaseStat { get; }
    public CharacterStat EnhancedStat { get; set; }
    public int Level { get; }
    public float CurrentHp { get; private set; }
    public float CurrentMp { get; private set; }
    public int Exp { get; private set; }
    public int Gold { get; private set; }
    public  StringBuilder statusSb = new StringBuilder();
    // public Dictionary<int, Equipment> Equipped { get; }
    
    private CharacterStat totalStat;
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

    // TODO: 초기 골드를 줄건지?
    public Player(string name, CharacterClass charClass)
    {
        /*
        Name = name;
        // TODO
        // Class = DataManager.Instance.CharacterStatDatas.GetCharacterStat(charClass);
        Level = 1;
        CurrentHp = BaseStat.MaxHp;
        CurrentMp = BaseStat.MaxMp;
        Exp = 0;
        Gold = 0;
        */

        Name = name;
        Class = charClass;

        // ⭐ BaseStat을 반드시 초기화
        BaseStat = new CharacterStat
        {
            MaxHp = 100,   // 초기 HP 값 (적절히 변경)
            MaxMp = 50,    // 초기 MP 값 (적절히 변경)
            Atk = 10,      // 초기 공격력
            Def = 5,       // 초기 방어력
            Luk = 3,       // 초기 럭
            Dex = 4,       // 초기 민첩
            Speed = 5,     // 초기 속도
            CriticalChance = 0.05f,  // 치명타 확률 (5%)
            CriticalDamage = 1.5f    // 치명타 배율 (1.5배)
        };

        EnhancedStat = new CharacterStat(); // ⭐ 필수 초기화

        Level = 1;
        CurrentHp = BaseStat.MaxHp;
        CurrentMp = BaseStat.MaxMp;
        Exp = 0;
        Gold = 4000;
    }

    // 상태창 보기
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

    // 골드 얻기
    // TODO: 보상에 의한 추가 골드를 따로 표시해줄 것인가?
    public void AddGold(int gold, bool isReward)
    {
        Gold += gold;

        // 추가 보상 : 럭에 의한 추가 골드 획득
        if (isReward)
        {
            // 보상 1000G , Luk 3
            // 추가보상 = 1000 * 0.06 = 60G
            
            // Luk에 의한 추가보상
            float addGoldRate = BaseStat.Luk * 2 / 100;
            float addGold = gold * addGoldRate;
            Gold += (int)addGold;
        }
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
        // if (Exp >= DataManager.Instance.CharacterLevelData.LevelUpExp)
        // {
        //     LevelUp();
        // }
    }

    public void LevelUp()
    {
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
        
        return;
        
        // 스탯을 올리는 함수
        void AddStat(float hp, int mp, float atk, float def, float luk, float dex, int speed, float criticalChance, float criticalDamage)
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

    public void Dead()
    {
        // 타이틀로 돌아가기;
    }
    
    // 아이템 사용하기?
    public void UseItem()
    {
        // TODO: 몬스터와 전투 도중 사용할 것인지? 아니면 마을에서 사용할 것인지? 아니면 한 층을 클리어했을 때 사용할 것인지?
    }
    
    // 장비 착용하기?
    public void Equip()
    {
        
    }

    public void UnEquip()
    {
        
    }
}