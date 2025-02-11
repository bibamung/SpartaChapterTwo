using System.Text;
using Sylphyr.Character;

namespace Sylphyr.Scene;

public class TitleScene : SingleTon<TitleScene>
{
    public StringBuilder TitleSb = new();
    public StringBuilder Sb = new();

    public TitleScene()
    {
        TitleSb.AppendLine("   _____         _         _                  ");
        TitleSb.AppendLine("  /  ___|       | |       | |                 ");
        TitleSb.AppendLine("  \\ `--.  _   _ | | _ __  | |__   _   _  _ __ ");
        TitleSb.AppendLine("   `--. \\| | | || || '_ \\ | '_ \\ | | | || '__|");
        TitleSb.AppendLine("  /\\__/ /| |_| || || |_) || | | || |_| || |   ");
        TitleSb.AppendLine("  \\____/  \\\\__,||_|| .__/ |_| |_| \\\\__,||_|   ");
        TitleSb.AppendLine("           __/ |   | |             __/ |      ");
        TitleSb.AppendLine("          |___/    |_|            |___/       ");
    }

    public void Run()
    {
        Sb.AppendLine("");
        Sb.AppendLine("");
        Sb.AppendLine("  \t\t1. 게임 시작");
        Sb.AppendLine("  \t\t2. 이어 하기");
        Sb.AppendLine("  \t\t3. 게임 종료");
        Sb.AppendLine("  \t\t4. 디버그");
        Sb.AppendLine();
        
        Console.Clear();
        Console.Write(TitleSb.ToString());
        Console.Write(Sb.ToString());
        
        int input = GetInput(1, 4);
        switch (input)
        {
            case 1: StartNewGame();
                break;
            case 2: LoadGame();
                break;
            case 3: ExitGame();
                break;
            case 4: DebugScene();
                break;
        }
    }

    private void DebugScene()
    {
        Scene.DebugScene.Instance.Init();
        Scene.DebugScene.Instance.Run();
    }

    private void StartNewGame()
    {
        Console.Clear();
        Console.Write(TitleSb.ToString());
        
        StringBuilder newGameSb = new();
        newGameSb.AppendLine();
        newGameSb.AppendLine();
        newGameSb.AppendLine("바람의 정령들이 사는 신비한 대지 실피아에 어서오세요!");
        newGameSb.AppendLine("이 곳은 빛과 바람이 조화를 이루는 마을 루미에라입니다.");
        newGameSb.AppendLine("당신은 이 마을에 등장한 어둠의 탑 다크헤이븐을 올라");
        newGameSb.AppendLine("고난과 역경을 극복하고 고위정령이 되어 구름 위의 세계 누비아에 도달해야 합니다.");
        newGameSb.AppendLine("당신의 이름을 가르쳐 주세요.");
        
        Console.Write(newGameSb.ToString());
        string name = Console.ReadLine();
        
        newGameSb.Clear();
        newGameSb.AppendLine();
        newGameSb.AppendLine($"당신의 이름은 {name}이군요.");
        newGameSb.AppendLine("당신의 직업을 선택해 주세요.");
        newGameSb.AppendLine("1. 전사");
        newGameSb.AppendLine("2. 도적");
        newGameSb.AppendLine("3. 궁수");
        newGameSb.AppendLine("4. 팔라딘");
        Console.Write(newGameSb.ToString());
        int input = GetInput(1, 4);
        
        GameManger.Instance.SetPlayer(name, (CharacterClass)input);
        GameManger.Instance.Init();
        var player = GameManger.Instance.player;
        
        newGameSb.Clear();
        newGameSb.AppendLine();
        newGameSb.AppendLine($"{player.BaseStat.Name} {name}, 모험을 시작합니다.");
        newGameSb.AppendLine();
        newGameSb.AppendLine("press any key to continue...");
        Console.Write(newGameSb.ToString());
        Console.ReadKey();
        GameManger.Instance.Main.Run();
    }

    private void LoadGame()
    {
        // TODO: Implement LoadGame
    }
    
    private void ExitGame()
    {
        Environment.Exit(0);
    }

    private int GetInput(int min, int max)
    {
        int input = 0;
        while (true)
        {
            Console.Write(">> ");
            if (int.TryParse(Console.ReadLine(), out input))
            {
                if (input >= min && input <= max)
                {
                    return input;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                }
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }
        }
    }
}