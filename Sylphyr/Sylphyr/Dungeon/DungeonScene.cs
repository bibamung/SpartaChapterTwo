using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    }
    
}
