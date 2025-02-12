using System.Text;
using System.Xml.Linq;
using Sylphyr.Character;
using Sylphyr.Utils;
using Sylphyr.YJH;

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
        TitleSb.AppendLine("");
        TitleSb.AppendLine("");
    }

    public void Run()
    {
        Sb.Clear();
        Sb.AppendLine("  \t\t1. 게임 시작");
        Sb.AppendLine("  \t\t2. 이어 하기");
        Sb.AppendLine("  \t\t3. 게임 종료");
        Sb.AppendLine();
        
        Console.Clear();
        Console.Write(TitleSb.ToString());
        Console.Write(Sb.ToString());
        
        int input = Util.GetInput(1, 4);
        switch (input)
        {
            case 1: StartNewGame();
                break;
            case 2: LoadGame();
                break;
            case 3: ExitGame();
                break;
        }
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
        int input = Util.GetInput(1, 4);
        
        GameManager.Instance.SetPlayer(name, (CharacterClass)input);
        GameManager.Instance.Init();
        var player = GameManager.Instance.player;
        
        newGameSb.Clear();
        newGameSb.AppendLine();
        newGameSb.AppendLine($"{player.BaseStat.Name} {name}, 모험을 시작합니다.");
        newGameSb.AppendLine();
        newGameSb.AppendLine("press any key to continue...");
        Console.Write(newGameSb.ToString());
        Console.ReadKey();
        GameManager.Instance.main.Run();
    }

    private void LoadGame()
    {
        LoadManager.Instance.loadgame();

        if (LoadManager.Instance.gameDatas == null)
        {
            Console.WriteLine("로드된 게임 데이터가 없습니다. 게임을 시작할 수 없습니다.");
            return;
        }
        Console.WriteLine(LoadManager.Instance.gameDatas);
        Console.WriteLine(LoadManager.Instance.gameDatas.CharacterClass);

        GameManager.Instance.SetPlayer("", CharacterClass.Paladin);

        GameManager.Instance.player.InitializePlayer(LoadManager.Instance.gameDatas);

        GameManager.Instance.Init();
        GameManager.Instance.main.Run();    
    }

    public GameData GameData { get; }


    public void ExitGame()
    {
        Environment.Exit(0);
    }
}