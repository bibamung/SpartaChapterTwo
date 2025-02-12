using Sylphyr.Character;
using Sylphyr.Utils;
using Sylphyr.YJH;

namespace Sylphyr.Scene;

public class DebugScene : SingleTon<DebugScene>
{
    public void Run()
    {
        Console.Clear();
        Console.WriteLine("1. 플레이어 정보");
        Console.WriteLine("2. 경험치 획득");
        Console.WriteLine("3. 레벨업");
        Console.WriteLine("4. 골드 추가");
        Console.WriteLine("5. 아이템 추가");
        Console.WriteLine("0. 뒤로가기");

        int input = Util.GetInput(0, 5);
        switch (input)
        {
            case 1:
                GameManager.Instance.player.PrintStatus();
                Console.WriteLine("press any key to continue...");
                Console.ReadKey();
                Run();
                break;
            case 2:
                ExpUp();
                break;
            case 3:
                LevelUp();
                break;
            case 4:
                AddGold();
                break;
            case 5:
                AddItem();
                break;
            case 0:
                GameManager.Instance.main.Run();
                break;
        }
    }

    private void ExpUp()
    {
        Console.Clear();
        Console.WriteLine("1. 1,000 획득");
        Console.WriteLine("2. 10,000 획득");
        Console.WriteLine("3. 100,000 획득");

        int input = Util.GetInput(1, 3);
        switch (input)
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

        var player = GameManager.Instance.player;
        Console.WriteLine("경험치를 획득하였습니다.");
        Console.WriteLine($"현재 레벨 : Lv.{player.Level}");
        Console.WriteLine($"현재 경험치 : {player.Exp}/{player.LevelData.GetExp(GameManager.Instance.player.Level)}");
        Console.WriteLine("press any key to continue...");
        Console.ReadKey();

        Run();
    }

    private void LevelUp()
    {
        var player = GameManager.Instance.player;
        int levelUpExp = player.LevelData.GetExp(player.Level);
        int exp = levelUpExp - player.Exp;
        player.AddExp(exp);

        Console.WriteLine($"현재 경험치 : {player.Exp}/{player.LevelData.GetExp(GameManager.Instance.player.Level)}");
        Console.WriteLine("press any key to continue...");
        Console.ReadKey();
        Run();
    }

    private void AddGold()
    {
        Console.Clear();
        Console.WriteLine("1. 1,000G 획득");
        Console.WriteLine("2. 10,000G 획득");
        Console.WriteLine("3. 100,000G 획득");
        Console.WriteLine("4. 1,000,000G 획득");

        int input = Util.GetInput(1, 4);
        switch (input)
        {
            case 1:
                GameManager.Instance.player.AddGold(1000);
                break;
            case 2:
                GameManager.Instance.player.AddGold(10000);
                break;
            case 3:
                GameManager.Instance.player.AddGold(100000);
                break;
        }

        Console.WriteLine("골드를 획득하였습니다.");
        Console.WriteLine($"현재 보유 골드 : {GameManager.Instance.player.Gold}G");
        Console.WriteLine("press any key to continue...");
        Console.ReadKey();
        Run();
    }

    private void AddItem()
    {
        Console.Clear();
        Console.WriteLine("1. 무기");
        Console.WriteLine("2. 장비");
        Console.WriteLine("3. 소모품");

        int input = Util.GetInput(1, 3);
        switch (input)
        {
            case 1:
                AddWeapon();
                break;
            case 2:
                AddEquipment();
                break;
            case 3:
                AddConsumable();
                break;
        }
    }

    private int GetInputId()
    {
        Console.WriteLine("아이템 ID를 입력해주세요.");

        while (true)
        {
            string input = Console.ReadLine();
            if (int.TryParse(input, out int id))
            {
                return id;
            }
            else
            {
                Console.WriteLine("숫자만 입력해 예외처리 귀찮잖아");
            }
        }
    }

    private void AddWeapon()
    {
        var list = DataManager.Instance.weaponItem;
        Console.WriteLine("추가할 아이템의 ID를 입력해주세요.");
        while (true)
        {
            int id = GetInputId();
            if (list.Exists(x => x.Id == id))
            {
                var item = list.Find(x => x.Id == id);
                GameManager.Instance.inventory.AddWeapon(item);
                Console.WriteLine("아이템을 추가하였습니다.");
                Console.WriteLine("press any key to continue...");
                Console.ReadKey();
                break;
            }
            else
            {
                Console.WriteLine("해당 아이템이 존재하지 않습니다.");
            }
        }
        
        Run();
    }

    private void AddEquipment()
    {
        var list = DataManager.Instance.equipmentItems;
        Console.WriteLine("추가할 아이템의 ID를 입력해주세요.");
        while (true)
        {
            int id = GetInputId();
            if (list.Exists(x => x.ID == id))
            {
                var item = list.Find(x => x.ID == id);
                GameManager.Instance.inventory.AddItem(item);
                Console.WriteLine("아이템을 추가하였습니다.");
                Console.WriteLine("press any key to continue...");
                Console.ReadKey();
                break;
            }
            else
            {
                Console.WriteLine("해당 아이템이 존재하지 않습니다.");
            }
        }
        
        Run();
    }

    private void AddConsumable()
    {
        var list = DataManager.Instance.consumeItems;
        Console.WriteLine("추가할 아이템의 ID를 입력해주세요.");
        while (true)
        {
            int id = GetInputId();
            if (list.Exists(x => x.Id == id))
            {
                var item = list.Find(x => x.Id == id);
                GameManager.Instance.inventory.AddPotion(item);
                Console.WriteLine("아이템을 추가하였습니다.");
                Console.WriteLine("press any key to continue...");
                Console.ReadKey();
                break;
            }
            else
            {
                Console.WriteLine("해당 아이템이 존재하지 않습니다.");
            }
        }

        Run();
    }
}