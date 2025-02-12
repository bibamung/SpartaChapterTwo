using Newtonsoft.Json;
using SaveData = Sylphyr.YJH.SaveData;
using GameManager = Sylphyr.GameManager;
using System.Collections.Generic;
using System.IO;
using System;
using System.Diagnostics;
using Guild;


namespace Sylphyr.YJH;

public class LoadManager : SingleTon<LoadManager>
{
    public GameData gameDatas;
    
    public readonly string baseDirectory = AppDomain.CurrentDomain.BaseDirectory; //.exe파일위치 상대경로 지정을 위해사용

    public void loadgame()
    {
        try
        {
            DirectoryInfo projectDir = Directory.GetParent(baseDirectory);      // net8.0
            projectDir = projectDir.Parent;                                     // Debug
            projectDir = projectDir.Parent;                                     // bin
            projectDir = projectDir.Parent;                                     // Sylphyr

            string dataPath = Path.Combine(projectDir.FullName, "Data/Save/GameData.json");

            // 파일 존재 여부 확인
            if (!File.Exists(dataPath))
            {
                Console.WriteLine($"파일을 찾을 수 없습니다: {dataPath}");
                return;
            }

            string jsonString = File.ReadAllText(dataPath);

            // JSON 데이터 출력 (디버깅용 선택 사항)
            Console.WriteLine("JSON 파일 내용:\n" + jsonString);

            // JSON 역직렬화 시도
            gameDatas = JsonConvert.DeserializeObject<GameData>(jsonString);
            gameDatas = JsonConvert.DeserializeObject<GameData>(File.ReadAllText(dataPath));

            
            // 로드 성공 메시지
            Console.WriteLine("데이터 로드 성공!");
        }
        catch (JsonSerializationException ex)
        {
            // 역직렬화 오류 발생 시 처리
            Console.WriteLine($"JSON 직렬화 오류 발생: {ex.Message}");
            Console.WriteLine(ex.StackTrace);
        }
        catch (JsonReaderException ex)
        {
            // JSON 데이터 문제가 있을 경우
            Console.WriteLine($"JSON 읽기 오류 발생: {ex.Message}");
            Console.WriteLine(ex.StackTrace);
        }
        catch (Exception ex)
        {
            // 그 외의 일반 오류 처리
            Console.WriteLine($"알 수 없는 오류 발생: {ex.Message}");
            Console.WriteLine(ex.StackTrace);
        }
        

    }
    
}

public class GameData
{
    public string Name { get; set; } // 플레이어 이름
    public string CharacterClass { get; set; } // 캐릭터 클래스
    public int Level { get; set; } // 플레이어 레벨
    public int CurrentHp { get; set; } // 현재 HP (JSON과 일치)
    public int CurrentMp { get; set; } // 현재 MP (JSON과 일치)
    public int Exp { get; set; } // 경험치
    public int Gold { get; set; } // 소지금


    public float Atk {  get; set; }   
    public float Dex {  get; set; }   
    public float Def {  get; set; }   
    public float Luk {  get; set; }   

    public CharacterStatData BaseStat { get; set; } // 기본 스탯
    public CharacterStatData EnhancedStat { get; set; } // 강화된 스탯
    public List<ItemData> Items { get; set; } = new List<ItemData>(); // 빈 리스트 초기화
    public List<WeaponData> Weapons { get; set; } = new List<WeaponData>(); // 빈 리스트 초기화
    public List<PotionData> Potions { get; set; } = new List<PotionData>(); // 빈 리스트 초기화
    public List<int> purChaseweaponItem { get; set; } = new List<int>(); // 구매한 무기 정보
    public List<int> purChaseEquipmentItem { get; set; } = new List<int>(); // 구매한 방어구 정보
    public List<int> purChasePotion { get; set; } = new List<int>(); // 구매한 방어구 정보
    public List<int> weaponEquip { get; set; } = new List<int>(); // 장비중인 무기
    public List<int> itemsEquip { get; set; } = new List<int>(); // 장비중인 방어구
}
