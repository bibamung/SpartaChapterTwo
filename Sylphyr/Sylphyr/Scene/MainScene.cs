using System.Text;

namespace Sylphyr.Scene;

public class MainScene
{
    public StringBuilder sb = new();

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
    }
}