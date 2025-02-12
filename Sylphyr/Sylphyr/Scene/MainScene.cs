using Sylphyr.Character;
using Sylphyr.Dungeon;
using System.Text;
using Sylphyr.Utils;
using Sylphyr.YJH;
using Sylphyr.Character;

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

        if (!GameManager.Instance.shop.isShop)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("상점이 닫혔습니다.");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("상점이 열렸습니다.");
            Console.ResetColor();
        }

        Console.Write("원하시는 행동을 입력해주세요.");

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
                GameSave();
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
        if (GameManager.Instance.shop.isShop)
        {
            GameManager.Instance.shop.shopScene(player, inventory);
        }
        Run();
    }

    private void EnterDungeon()
    {
        dungeonManager.StageSelect();
    }

    private void GameSave()
    {
        try
        {
            // Save 클래스의 인스턴스 생성
            SaveManager saveManagerSystem = new SaveManager();


            // 플레이어 데이터를 SaveData로 변환
            var player = GameManager.Instance.player; // 현재 플레이어 정보
            SaveData data = player.ToSaveData();
            data.CreateSaveItemData();
            data.CreateSaveWeponData();
            data.CreateSavePotionData();
            data.SavepurchaseItem();
            data.SaveEquipItem();

            // 세이브 폴더 없으면 생성
            DirectoryInfo projectDir = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory); // net8.0
            projectDir = projectDir.Parent; // Debug
            projectDir = projectDir.Parent; // bin
            projectDir = projectDir.Parent; // Sylphyr

            string folderPath = Path.Combine(projectDir.FullName, "Data", "Save");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            SaveManager.filePath = Path.Combine(folderPath, "GameData.json"); // 최종 저장 파일 경로 설정


            // 데이터 저장
            saveManagerSystem.SaveGame(data);

            Console.WriteLine("게임이 성공적으로 저장되었습니다!");
          


            // 저장 후 메뉴 출력
            ShowMenu();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"세이브 중 오류가 발생했습니다: {ex.Message}");
        }
    }

    /// <summary>
    /// 저장 후 옵션 메뉴 표시
    /// </summary>
    private void ShowMenu()
    {
        while (true) // 사용자가 명시적으로 종료하기 전까지 반복
        {
            Console.WriteLine("원하는 작업을 선택하세요:");
            Console.WriteLine("1. 계속하기");
            Console.WriteLine("2. 종료");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    // 계속하기 -> 메인 동작으로 돌아가기
                    Run();
                    break;
                case "2":
                    // 프로그램 종료
                    Console.WriteLine("게임을 종료합니다.");
                    Environment.Exit(0); // 명시적으로 프로그램 종료
                    break;
                default:
                    Console.WriteLine("유효하지 않은 입력입니다. 다시 시도하세요.");
                    break;
            }
        }
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