using System;
using Newtonsoft.Json;
using Sylphyr.KJE;
using TextRPG.Utils;


namespace Sylphyr.YJH;

public class DataManager
{
    private List<CharacterStat> characterStats;
    private List<Monster> monsters;
    private List<ConsumeItem> consumeItems;
    private List<EquipmentItem> equipmentItems;
        
  
    public void ConvertAllCsv()
    {
        Console.WriteLine("CSV -> JSON 변환을 시작합니다...");
        CsvToJsonConverter.ConvertAllCsvInFolder();
        Console.WriteLine("변환이 완료되었습니다!");
    }
    
    // Json파일을 여기서 리스트화.
    public void DeserializeJson()
    {
        characterStats = JsonConvert.DeserializeObject<List<CharacterStat>>(File.ReadAllText("Data/JSON/Sylphyr.Character.json"));
        monsters = JsonConvert.DeserializeObject<List<Monster>>(File.ReadAllText("Data/JSON/Sylphyr.Monster.json"));
        consumeItems = JsonConvert.DeserializeObject<List<ConsumeItems>>(File.ReadAllText("Data/JSON/Sylphyr.ConsumeItems.json"));
        
        
        
        CharacterStat testStat = characterStats[0];
        
        Console.WriteLine($"Name:{testStat.Name}");
    }
}

