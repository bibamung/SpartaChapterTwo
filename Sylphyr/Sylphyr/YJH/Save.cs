using System.Text.Json;
using Player = Sylphyr.Character.Player;
using CharacterStat = Sylphyr.Character.CharacterStat;
using Saving = Sylphyr.YJH.Save;


namespace Sylphyr.YJH;

public class Save
{
    private readonly string baseDirectory = AppDomain.CurrentDomain.BaseDirectory; //.exe파일위치 상대경로 지정을 위해사용
    public static string filePath;


    //세이브 폴더 만들어주기
    public void CreateSaveFolder()
    {
        //파일 위치지정
        DirectoryInfo projectDir = Directory.GetParent(baseDirectory); // net8.0
        projectDir = projectDir.Parent; // Debug
        projectDir = projectDir.Parent; // bin
        projectDir = projectDir.Parent; // Sylphyr

        string folderPath = "Data/Save"; // ./ 실행한 폴더 경로
        DirectoryInfo SaveFolder = new DirectoryInfo(folderPath);

        if (SaveFolder.Exists == false) //경로에 Save 폴더가 없다면 생성
        {
            SaveFolder.Create();
        }
    }

    //게임 세이브
    public class GameSave
    {
        private readonly string baseDirectory = AppDomain.CurrentDomain.BaseDirectory; //.exe파일위치 상대경로 지정을 위해사용
        public static string filePath;
    }


    //게임 세이브
    public void SaveGame(SaveData data)
    {
        string jsonString = JsonSerializer.Serialize(data);
        File.WriteAllText(filePath, jsonString);
    }

    //세이브 파일 불러오기
    public SaveData LoadGame()
    {
        if (File.Exists(filePath))
        {
            string jsonString = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<SaveData>(jsonString);
        }
        else
        {
            Console.WriteLine("저장된 파일이 없습니다.");
            return null;
        }
    }
}

/*
// Data 객체를 JSON 문자열로 변환
string jsonString = JsonSerializer.Serialize(data);

// JSON 문자열을 파일에 저장
File.WriteAllText(filePath, jsonString);

//저장 하기
GameData loadedData = SaveSystem.LoadGame(saveFilePath);

//불러오기
SaveSystem.SaveData(Data, saveFilePath);


//Save 폴더 안에 json 파일 만들기
File.WriteAllText(filePath, Save.ToString());
*/