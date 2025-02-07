using System.Globalization;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using Newtonsoft.Json;

namespace TextRPG.Utils;

// CSV 파일을 Json 파일로 변환하는 유틸리티 클래스
public static class CsvToJsonConverter
{
    private const string folderPath = "Data/CSV";
    private const string outputFolderPath = "Data/JSON";
 
    public static void ConvertCsvToJson<T, TMap>(string filePath, string outputFolderPath) where TMap : ClassMap<T>, new()
    {
        using (var reader = new StreamReader(filePath))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            csv.Context.RegisterClassMap<TMap>();  // 매핑 클래스 적용
            var records = csv.GetRecords<T>().ToList();
            
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(filePath);
            string json =  JsonConvert.SerializeObject(records, Formatting.Indented);
            string jsonOutputPath = Path.Combine(outputFolderPath, $"{fileNameWithoutExt}.json");
            
            File.WriteAllText(jsonOutputPath, json);
            Console.WriteLine($"CSV -> Json 변환 완료! 결과파일 : {jsonOutputPath}");
        }
    }
    
    private static void ConvertCsvToJson(string csvPath, string jsonOutputPath)
    {
        if (!File.Exists(csvPath))
        {
            Console.WriteLine($"CSV 파일을 찾을 수 없습니다. : {csvPath}");
            return;
        }

        using (var reader = new StreamReader(csvPath, Encoding.UTF8))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            var records = csv.GetRecords<dynamic>(); // CSV 데이터를 동적 객체로 변환
            string json = JsonConvert.SerializeObject(records, Formatting.Indented);

            File.WriteAllText(jsonOutputPath, json);
            Console.WriteLine($"CSV -> Json 변환 완료! 결과파일 : {jsonOutputPath}");
        }
    }

    public static void ConvertAllCsvInFolder()
    {
        if (!Directory.Exists(outputFolderPath))
        {
            Directory.CreateDirectory(outputFolderPath);
        }
        
        string[] csvFiles = Directory.GetFiles(folderPath, "*.csv");
        
        if (csvFiles.Length == 0)
        {
            Console.WriteLine("CSV 파일을 찾을 수 없습니다.");
            return;
        }

        foreach (var csvFile in csvFiles)
        {
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(csvFile);
            string jsonOutputPath = Path.Combine(outputFolderPath, $"{fileNameWithoutExt}.json");

            ConvertCsvToJson(csvFile, jsonOutputPath);
        }
        
        Console.WriteLine("모든 CSV 파일을 Json으로 변환 완료!");
    }
}