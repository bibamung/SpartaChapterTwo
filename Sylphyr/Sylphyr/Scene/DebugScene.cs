using Sylphyr.Character;

namespace Sylphyr.Scene;

public class DebugScene : SingleTon<DebugScene>
{
    public void Init()
    {
        string name = "테스트";
        GameManager.Instance.SetPlayer(name, CharacterClass.Thief);
    }
    
    public void Run()
    {
        Console.Clear();
        Console.WriteLine("1. 플레이어 정보");
        Console.WriteLine("2. 경험치 획득");
        Console.WriteLine("3. 레벨업");

        int select = int.Parse(Console.ReadLine());
        switch (select)
        {
            case 1:
                GameManager.Instance.player.PrintStatus();
                Console.WriteLine("any key to continue");
                Console.ReadKey();
                Run();
                break;
            case 2:
                ExpUp();
                break;
            case 3:
                LevelUp();
                break;
        }
    }

    public void ExpUp()
    {
        Console.WriteLine();
        Console.WriteLine("1. 1,000 획득");
        Console.WriteLine("2. 10,000 획득");
        Console.WriteLine("3. 100,000 획득");

        int select = int.Parse(Console.ReadLine());
        switch (select)
        {
            case 1:
                GameManager.Instance.player.AddExp(1000);
                break;
            case 2:
                GameManager.Instance.player.AddExp(10000);
                break;
            case 3:
                GameManager.Instance.player.AddExp(100000);
                break;
        }

        Console.WriteLine("경험치가 획득되었습니다.");
        Console.WriteLine("any key to continue");
        Console.ReadKey();
        
        Run();
    }

    public void LevelUp()
    {
        var player = GameManager.Instance.player;
        int levelUpExp = player.LevelData.GetExp(player.Level);
        int exp = levelUpExp - player.Exp;
        player.AddExp(exp);
        
        Console.WriteLine("any key to continue");
        Console.ReadKey();
        Run();
    }
}