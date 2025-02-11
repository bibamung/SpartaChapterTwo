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

        int input = Util.GetInput(0, 5);
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
        Console.WriteLine("any key to continue");
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
    
    /*
    private void SaveGameData()
    {
        try
        {
            // Save 클래스의 인스턴스 생성
            Save saveSystem = new Save();

            // 세이브 폴더 없으면 생성
            saveSystem.CreateSaveFolder();

            // SaveData 객체 생성 및 데이터 준비
            SaveData data = new SaveData
            {
                // CharacterStats = GameManger.Instance.player.CharacterStats, // 유저 캐릭터 스탯 리스트
                // Inventories = GameManger.Instance.inventory.Items,     // 인벤토리 아이템들
                Players = new List<Player> { GameManager.Instance.player } // 플레이어 정보
            };

            // 세이브 파일 경로 지정
            Save.filePath = "Data/Save/GameData.json"; // 상대 경로에 저장

            // 데이터 저장
            saveSystem.SaveGame(data);

            Console.WriteLine("게임이 성공적으로 저장되었습니다!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"세이브 중 오류가 발생했습니다: {ex.Message}");
        }  
    }
    */
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