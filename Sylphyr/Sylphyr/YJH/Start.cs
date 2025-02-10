namespace Sylphyr.YJH;

public class Start
{
    static void Main(string[] args)
    {
        /*DataManager dataManager = new DataManager();
        dataManager.ConvertAllCsv(); 

        Console.WriteLine("모든 작업이 완료되었습니다.");

        dataManager.DeserializeJson();*/

        var player = GameManger.Instance.player;
        var inventory = new Inventory();
        var potion = new Potion(10, "테스트", 10, 10, 10, "테스트", false, false);
        var weapon = new Weapon(10, "테스", 10, 10, 10, 4, "테스", false, false);
        Item item = new Item(1000, "테스트 아이템", 5, 10, "상의", 500, "테스트 아이템입니다.", false, false);

        item.addTestItems();  // 상점에 아이템 추가
        potion.addpotion();
        weapon.addWeapon();
        

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
                    item.shopScene(player, inventory, potion, weapon);  // 상점 메뉴로 이동
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

