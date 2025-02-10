namespace Sylphyr.YJH;

public class SaveSystem
{
    public static void SaveData()
    {
        // Data 객체를 JSON 문자열로 변환
        //string jsonString = JsonSerializer.Serialize(data);
        
        // JSON 문자열을 파일에 저장
        //File.WriteAllText(filePath, jsonString);
        Console.WriteLine("DATA saved successfully!");
    }
}