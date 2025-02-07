namespace Sylphyr.YJH;

public class Start
{
    static void Main(string[] args)
    {
        DataManager dataManager = new DataManager();
        dataManager.ConvertAllCsv(); 

        Console.WriteLine("모든 작업이 완료되었습니다.");

        dataManager.DeserializeJson();
        
        
    }
}