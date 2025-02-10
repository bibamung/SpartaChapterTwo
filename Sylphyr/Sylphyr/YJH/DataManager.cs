using System;
using Guild;
using Newtonsoft.Json;
using Sylphyr.Character;
using Sylphyr.Utils;


namespace Sylphyr.YJH;

public class DataManager:SingleTon<DataManager>
{
    public List<CharacterStat> characterStats;
    public List<Monster> monsters;
    public List<Item.ConsumeItem> consumeItems;
    public List<Item.EquipmentItem> equipmentItems;
    public List<Item.WeaponItem> weaponItems;
    public List<Quest> quests;
  
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
        consumeItems = JsonConvert.DeserializeObject<List<Item.ConsumeItem>>(File.ReadAllText("Data/JSON/Sylphyr.ConsumeItems.json"));
        equipmentItems = JsonConvert.DeserializeObject<List<Item.EquipmentItem>>(File.ReadAllText("Data/JSON/Sylphyr.EquipmentItems.json"));
        weaponItems = JsonConvert.DeserializeObject<List<Item.WeaponItem>>(File.ReadAllText("Data/JSON/Sylphyr.WeaponItem.json"));
        quests = JsonConvert.DeserializeObject<List<Quest>>(File.ReadAllText("Data/JSON/Sylphyr.Quest.json"));
    }
}

