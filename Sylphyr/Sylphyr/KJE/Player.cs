using System.Text;

namespace Sylphyr.KJE;

public class Player
{
    public string Name { get; }
    public CharacterClass Class { get; }
    public CharacterStat BaseStat { get; }
    public CharacterStat EnhancedStat { get; }
    public CharacterStat TotalStat { get; }
    public int Level { get; }
    public float CurrentHp { get; private set; }
    public float CurrentMp { get; private set; }
    public int Exp { get; private set; }
    public int Gold { get; private set; }
    
    public  StringBuilder statusSb = new StringBuilder();

    // TODO: 초기 골드를 줄건지? 
    public Player(string name, CharacterClass charClass)
    {
        Name = name;
        // TODO
        // Class = DataManager.Instance.CharacterStatDatas.GetCharacterStat(charClass);
        Level = 1;
        CurrentHp = BaseStat.MaxHp;
        CurrentMp = BaseStat.MaxMp;
        Exp = 0;
        Gold = 0;
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
    public void AddGold(int gold, bool isReward)
    {
        Gold += gold;

        if (isReward)
        {
            // Luk에 의한 추가보상
            
            
        }

    }

    public void RemoveGold(int gold)
    {
        Gold -= gold;
        
        if (Gold < 0)
            Gold = 0;
    }
    
    // 대미지 받기
    // 죽기
    // 아이템 사용하기?
    // 장비 착용하기?
    // 경험치 얻기
    // 레벨업
    
}