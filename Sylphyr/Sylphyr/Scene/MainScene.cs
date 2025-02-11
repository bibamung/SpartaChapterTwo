using Sylphyr.Character;
using Sylphyr.Dungeon;
using System.Text;

namespace Sylphyr.Scene;


public class MainScene
{
    public StringBuilder sb = new();
    DungeonManager dungeonManager = new DungeonManager();

    public MainScene()
    {
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

        Console.Write("원하시는 행동을 입력해주세요.\n>> ");

        int select;
        bool isVaildNum = int.TryParse(Console.ReadLine(), out select);
        if (isVaildNum)
        {
            switch ((Behavior)select)
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
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        else
        {
            Console.WriteLine("숫자를 입력해 주세요.");
        }
    }

    private void PrintPlayerInfo()
    {
        var player = GameManger.Instance.player;
        Console.Clear();
        Console.WriteLine("플레이어의 정보를 확인합니다.");
        Console.WriteLine();
        player.PrintStatus();
        Console.WriteLine();
        Console.WriteLine("any key to continue");
        Console.ReadKey();
        Run();
    }
    
    private void OpenInventory()
    {
        var player = GameManger.Instance.player;
        GameManger.Instance.inventory.invenDisplay(player);
    }

    private void EnterStore()
    {
        var player = GameManger.Instance.player;
        var inventory = GameManger.Instance.inventory;
        GameManger.Instance.shop.shopScene(player, inventory);
    }
    
    private void EnterDungeon()
    {
        dungeonManager.StageSelect();
    }
    
    private void Save()
    {
        
    }
    
    private void EnterCasino()
    {
        
    }
}

public enum Behavior
{
    PlayerInfo = 1, 
    Inventory = 2,
    Store = 3,
    DungeonEnter = 4,
    Save = 5, 
    Exit = 0
}