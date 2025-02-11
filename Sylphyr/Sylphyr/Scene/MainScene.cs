using Sylphyr.Character;
using Sylphyr.Dungeon;
using System.Text;

namespace Sylphyr.Scene;
public enum Behavior{
    PlayerInfo = 1, Inventory = 2, Store = 3, DungeonEnter = 4, Storage = 5, Exit = 0
}

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
            switch (select)
            {
                case (int)Behavior.DungeonEnter:
                    dungeonManager.StageSelect();
                    break;
                case (int)Behavior.Inventory:
                    GameManger.Instance.inventory.invenDisplay(GameManger.Instance.player);
                    break;
                case (int)Behavior.Store:
                    GameManger.Instance.shop.shopScene(GameManger.Instance.player, GameManger.Instance.inventory);
                    break;
                default:
                    break;
            }
        }
        else
        {
            Console.WriteLine("숫자를 입력해 주세요.");
        }
    }
}