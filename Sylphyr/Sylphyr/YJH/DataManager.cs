using System;
using Guild;
using Newtonsoft.Json;
using Sylphyr.Character;
using Sylphyr.Utils;
using System.Collections.Generic;
using System.IO;

namespace Sylphyr.YJH;

public class DataManager:SingleTon<DataManager>
{
    private readonly string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
    
    public List<CharacterStat> characterStats;
    public Dictionary<int, CharacterStat> characterStatDict;
    public List<Monster> monsters;
    public List<Item.ConsumeItem> consumeItems;
    public List<Item.EquipmentItem> equipmentItems;
    public List<Item.WeaponItem> weaponItems;
    public List<CharacterSkillData> characterSkills;
    public List<Quest> quests;
  
    
    public void ConvertAllCsv()
    {
        CsvToJsonConverter. SetFolderPath();
        Console.WriteLine("CSV -> JSON 변환을 시작합니다...");
        CsvToJsonConverter.ConvertAllCsvInFolder();
        Console.WriteLine("변환이 완료되었습니다!");
    }
    
    // Json파일을 여기서 리스트화.
    public void DeserializeJson()
    {
        //파일 위치지정
        DirectoryInfo projectDir = Directory.GetParent(baseDirectory);      // net8.0
        projectDir = projectDir.Parent;                                     // Debug
        projectDir = projectDir.Parent;                                     // bin
        projectDir = projectDir.Parent;                                     // Sylphyr
        
        string dataPath = Path.Combine(projectDir.FullName, "Data/JSON");
        string dataPathCharacter = Path.Combine(dataPath, "Sylphyr.Character.json");
        string dataPathMonster = Path.Combine(dataPath, "Sylphyr.Monster.json");
        string dataPathConsumeItem = Path.Combine(dataPath, "Sylphyr.ConsumeItem.json");
        string dataPathEquipmentItem = Path.Combine(dataPath, "Sylphyr.EquipmentItem.json");
        string dataPathWeaponItem = Path.Combine(dataPath, "Sylphyr.WeaponItem.json");
        string dataPathQuest = Path.Combine(dataPath, "Sylphyr.Quest.json");
        string dataPathCharacterSkill = Path.Combine(dataPath, "Sylphyr.CharacterSkill.json");
        
        characterStats = JsonConvert.DeserializeObject<List<CharacterStat>>(File.ReadAllText(dataPathCharacter));
        monsters = JsonConvert.DeserializeObject<List<Monster>>(File.ReadAllText(dataPathMonster));
        consumeItems = JsonConvert.DeserializeObject<List<Item.ConsumeItem>>(File.ReadAllText(dataPathConsumeItem));
        equipmentItems = JsonConvert.DeserializeObject<List<Item.EquipmentItem>>(File.ReadAllText(dataPathEquipmentItem));
        weaponItems = JsonConvert.DeserializeObject<List<Item.WeaponItem>>(File.ReadAllText(dataPathWeaponItem));
        quests = JsonConvert.DeserializeObject<List<Quest>>(File.ReadAllText(dataPathQuest));
        characterSkills = JsonConvert.DeserializeObject<List<CharacterSkillData>>(File.ReadAllText(dataPathCharacterSkill));
    }
}

