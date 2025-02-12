using Sylphyr.Character;
using Sylphyr.Dungeon;
using System.Text;
using Sylphyr.Utils;
using Sylphyr.YJH;

namespace Sylphyr.Scene;

public class MainScene
{
    public StringBuilder sb = new();
    DungeonManager dungeonManager = new DungeonManager();

    public MainScene()
    {
        sb.Clear();
        sb.AppendLine(" == 루미에라 ==");
        sb.AppendLine();
        sb.AppendLine("1. 상태 보기");
        sb.AppendLine("2. 인벤토리");
        sb.AppendLine("3. 상점");
        sb.AppendLine("4. 던전 입장");
        sb.AppendLine("5. 저장하기");
        sb.AppendLine("0. 게임 종료");

        sb.AppendLine();
    }

    public void Run()
    {
        Console.Clear();
        Console.Write(sb.ToString());

        Console.WriteLine("원하시는 행동을 입력해주세요.");

        int input = Util.GetInput(0, 5, 1313);
        switch ((Behavior)input)
        {
            case Behavior.PlayerInfo:
                PrintPlayerInfo();
                break;
            case Behavior.Inventory:
                OpenInventory();
                break;
            case Behavior.Store:
                EnterStore();
                break;
            case Behavior.DungeonEnter:
                EnterDungeon();
                break;
            case Behavior.Save:
                
                break;
            case Behavior.Exit:
                TitleScene.Instance.ExitGame();
                break;
            case Behavior.Debug:
                Debug();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void PrintPlayerInfo()
    {
        var player = GameManager.Instance.player;
        Console.Clear();
        Console.WriteLine("플레이어의 정보를 확인합니다.");
        Console.WriteLine();
        player.PrintStatus();
        Console.WriteLine();
        Console.WriteLine("press any key to continue...");
        Console.ReadKey();
        Run();
    }

    private void OpenInventory()
    {
        var player = GameManager.Instance.player;
        GameManager.Instance.inventory.invenDisplay(player);
        Run();
    }

    private void EnterStore()
    {
        var player = GameManager.Instance.player;
        var inventory = GameManager.Instance.inventory;
        GameManager.Instance.shop.shopScene(player, inventory);
        Run();
    }

    private void EnterDungeon()
    {
        dungeonManager.StageSelect();
    }
    
    private void Debug()
    {
        DebugScene.Instance.Run();
    }
}

public enum Behavior
{
    PlayerInfo = 1,
    Inventory = 2,
    Store = 3,
    DungeonEnter = 4,
    Save = 5,
    Debug = 1313,
    Exit = 0
}