using System;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;

namespace Sylphyr.YJH;

// public class Save
// {
//     public static string filePath;
//     
//     //세이브 폴더 만들어주기
//     public void AddSaveFile()
//     {
//         string folderPath = "Sylphyr/Data"; // ./ 실행한 폴더 경로
//         DirectoryInfo SaveFolder = new DirectoryInfo(folderPath);
//
//         if (SaveFolder.Exists == false) //경로에 Save 폴더가 없다면 생성
//         {
//             SaveFolder.Create();
//         }
//     }
//
//     public static Game LoadSaveData(string SavePath)
//     {
//         if (!File.Exists(SavePath))
//         {
//             Console.WriteLine("File not found");
//             return null;
//         }
//         
//         string jsonString = File.ReadAllText(SavePath);
//         return JsonSerializer.Deserialize<Save>(jsonString);
//     }
//
//     public static void Save GameSaveData()
//     {
//       
//     }
// }




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