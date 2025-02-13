using System.Text;
using Sylphyr.Utils;

namespace Sylphyr.Scene;

public class HealingHouse
{
    private StringBuilder sb = new();
    public bool isOpen { get; private set; } = true;

    public HealingHouse()
    {
        sb.AppendLine(" 치유의 정령이 다친 곳을 치료해드릴게요!");
        sb.AppendLine();
        sb.AppendLine(" 1. 치료하기");
        sb.AppendLine(" 0. 나가기");
        sb.AppendLine();
    }

    public void Run()
    {
        Console.Clear();
        Console.Write(sb.ToString());

        int input = Util.GetInput(0, 1);
        switch (input)
        {
            case 1:
                Heal();
                break;
            case 0:
                GameManager.Instance.main.Run();
                break;
        }
    }
    
    private void Heal()
    {
        Console.Clear();

        var player = GameManager.Instance.player;
        var hpValue = player.TotalStat.MaxHp * 0.3f;
        var mpValue = player.TotalStat.MaxMp * 0.2f;
        player.Healing(hpValue, mpValue);
        
        Console.WriteLine("치료가 완료되었습니다!");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($" HP: {player.CurrentHp:N2} / {player.TotalStat.MaxHp:N2}");
        Console.WriteLine($" MP: {player.CurrentMp:N2} / {player.TotalStat.MaxMp:N2}");
        Console.ResetColor();
        Console.WriteLine();
        
        Console.WriteLine("탑에 갈 때는 조심하세요.");
        Console.WriteLine("press any key to continue...");
        Console.ReadKey();
        Close();
        
        GameManager.Instance.main.Run();
    }
    
    public void Open()
    {
        isOpen = true;
    }
    
    public void Close()
    {
        isOpen = false;
    }
}