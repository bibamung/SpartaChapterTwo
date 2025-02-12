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
        if (data != null)
        {
            // JSON 데이터 변환 및 저장
            string jsonString = JsonSerializer.Serialize(data);
            File.WriteAllText(filePath, jsonString);
            Console.WriteLine("생성된 JSON 데이터:\n" + jsonString);
        }
        else
        {
            Console.WriteLine("저장할 데이터가 없습니다.");
        }

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
    
    // JSON 저장 파일 삭제 (Debug용)
    public static void DeleteFile()
    {
        try
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                Console.WriteLine("저장 파일이 삭제되었습니다.");
            }
            else
            {
                Console.WriteLine("저장 파일이 존재하지 않습니다.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"파일 삭제 중 오류 발생: {ex.Message}");
        }
    }
}
