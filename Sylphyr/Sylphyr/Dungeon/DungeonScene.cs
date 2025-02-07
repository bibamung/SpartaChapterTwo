using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Sylphyr.Dungeon
{
    public class DungeonScene
    {
        public int StageSelect()
        {
            int stage;
            bool isValidNum = int.TryParse(Console.ReadLine(), out stage);
            if (isValidNum)
            {
                switch (stage)
                {
                    case 1:

                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        break;
                    case 7:
                        break;
                    case 8:
                        break;
                    case 9:
                        break;
                    case 10:
                        break;
                    case 11:
                        break;
                    case 12:
                        break;
                    case 13:
                        break;
                    case 14:
                        break;
                    case 15:
                        break;
                    default:
                        break;
                }
            }
            else
            {
                Console.WriteLine("잘못 입력하셨습니다.");
            }


            return stage;
        }

        public void DisplayHealthBar(List<Monster> stageMonsters)
        {
            foreach (Monster monster in stageMonsters)
            {
                int count = 1;
                int barSize = 20; // 체력바 길이 (20칸)
                float healthPercentage = monster.Hp / monster.MaxHp;
                int filledBars = (int)(barSize * healthPercentage);
                int emptyBars = barSize - filledBars;

                // 색상 적용 (콘솔 전용)
                Console.ForegroundColor = GetHealthColor(healthPercentage);

                // 체력바 출력
                Console.Write($"{count}. {monster.MonsterName} [");
                Console.Write(new string('■', filledBars)); // 채워진 부분
                Console.Write(new string('-', emptyBars));  // 빈 부분
                Console.Write($"] {monster.Hp}/{monster.MaxHp}");

                // 색상 초기화
                Console.ResetColor();
                Console.WriteLine();
            }
            
        }

        // 체력 상태에 따라 색상 변경 (콘솔용)
        private ConsoleColor GetHealthColor(float percentage)
        {
            if (percentage > 0.6f) return ConsoleColor.Green;  // 60% 이상 - 초록
            if (percentage > 0.3f) return ConsoleColor.Yellow; // 30~60% - 노랑
            return ConsoleColor.Red;                           // 30% 이하 - 빨강
        }

    }
    
}
