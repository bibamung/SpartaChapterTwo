using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Sylphyr
{
    public class Quest
    {
        List<Quest> questlists = new List<Quest>()
        {
            
        };
    }

    public class Guild
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Reward { get; set; }
        private List<Guild> inventory = new List<Guild>();

        public Guild(string name, string description, string reward)
        {
            Name = name;
            Description = description;
            Reward = reward;
        }

        public void ShowGuild()
        {
            Console.Clear();
            Console.WriteLine("길드에 오신것을 환영합니다.");
            Console.WriteLine("원하시는 길드로 가서 퀘스트를 수행해주세요!\n");
            Console.WriteLine("===== [ 길드 목록 ] =====");
            Console.WriteLine("1. 전투 길드");
            Console.WriteLine("2. 상인 길드");
            Console.WriteLine("3. 마법 길드");

            switch (Console.ReadLine())
            {
                case "1":
                    Player.BattleGuild(); break;
                case "2":
                    Player.MerchantGuild(); player.ManageEquipment(); break;
                case "0":
                    Console.WriteLine("\n0.나가기"); break;
                default:
                    Console.WriteLine($"잘못된 입력입니다.엔터를 눌러주세요.");
                    Console.ReadLine();
                    ShowGuild();
                    break;
            }

            if (inventory.Count == 0)
            {
                Console.WriteLine("현재 보유 중인 아이템이 없습니다.\n");
            }

            
        }
    }
}
