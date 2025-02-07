using System.Data;

namespace Sylphyr
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var player = GameManger.Instance.player;
            var inventory = new Inventory();
            Item item = new Item(1000, "테스트 아이템", 5, 10, "상의", 500, "테스트 아이템입니다.", false);
            item.addTestItems();  // 상점에 아이템 추가

            while (true)
            {
                Console.Clear();
                Console.WriteLine("메인 메뉴\n");
                Console.WriteLine("1. 아이템 상점");
                Console.WriteLine("2. 인벤토리\n");
                Console.WriteLine("0. 종료\n");
                Console.Write("원하는 메뉴를 선택하세요: ");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        item.shopScene(player, inventory);  // 상점 메뉴로 이동
                        break;
                    case "2":
                        inventory.invenDisplay(player);  // 인벤토리 메뉴로 이동
                        break;
                    case "0":
                        Console.WriteLine("게임을 종료합니다.");
                        return;
                    default:
                        Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.");
                        break;
                }
            }
        }
    }
}
