using Sylphyr.Scene;

namespace Sylphyr.YJH;
public class Start
{
    static void Main(string[] args)
    {
        DataManager.Instance.ConvertAllCsv(); 
        Console.WriteLine("모든 작업이 완료되었습니다.");
        DataManager.Instance.DeserializeJson();

        TitleScene titleScene = new TitleScene();
        titleScene.Run();
    }
}