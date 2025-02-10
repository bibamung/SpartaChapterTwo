using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Sylphyr.YJH;

namespace Guild
{
    public class Quest
    {
        //길드창-퀘스트목록 퀘스트진행상태
        //퀘스트이름 내용 보상 1수락 2거절
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int RewardExp { get; set; }
        public int RewardGold { get; set; }
        public int RequiredFloors { get; set; } //목표
        public int CurrentFloors { get; set; }  //현재
        public int MaxFloors { get; set; }
        public int RequiredBuyItems { get; set; }
        public int CurrentBuyItems { get; set; }
        public int RequiredSellItems { get; set; }
        public int CurrentSellItems { get; set; }
        public bool IsCompleted => CurrentFloors >= RequiredFloors;

        public Quest(int id, string name, string description, int rewardExp, int rewardGold, int requiredFloors, int buyItems,
                     int sellItems)
        {
            ID = id;
            Name = name;
            Description = description;
            RewardExp = rewardExp;
            RewardGold = rewardGold;
            RequiredFloors = requiredFloors;
            CurrentFloors = 0;
            RequiredBuyItems = buyItems;
            CurrentBuyItems = 0;
            RequiredSellItems = sellItems;
            CurrentSellItems = 0;
        }

        public void ShowFloorQuest() //퀘스트내용
        {
            Console.Clear();
            Console.WriteLine("===== Quest!! =====\n");
            Console.WriteLine($"{Name}\n");
            Console.WriteLine(Description);
            Console.WriteLine("\n- 보상 -");
            Console.WriteLine($"  Gold: {RewardGold}");
            Console.WriteLine($"  Exp: {RewardExp}\n");
        }
    }

    public class Player
    {
        public int Gold { get; set; } = 1000;
        public int Exp { get; set; } = 0;
        public Quest ActiveQuest { get; set; } = null; // 현재 진행 중인 퀘스트

        public void ShowStatus()
        {
            Console.WriteLine("\n===== [ 플레이어 상태 ] =====");
            Console.WriteLine($"Gold: {Gold}");
            Console.WriteLine($"Exp: {Exp}\n");
        }

        public void CompleteQuest()
        {
            if (!ActiveQuest.IsCompleted)
            {
                Console.WriteLine("퀘스트 완료 조건을 충족하지 않았습니다.");
                return;
            }

            Console.Clear();
            Console.WriteLine($" {ActiveQuest.Name} 완료!");
            Console.WriteLine($" {ActiveQuest.RewardGold} Gold & {ActiveQuest.RewardExp} Exp 획득!\n");

            Gold += ActiveQuest.RewardGold;
            Exp += ActiveQuest.RewardExp;
            ActiveQuest = null;
        }

        public void ProgressQuest()
        {
            if (ActiveQuest == null)
            {
                Console.WriteLine("진행중인 퀘스트가 없습니다.");
            }
            else if (ActiveQuest.IsCompleted)
            {
                Console.WriteLine("완료할 퀘스트가 있습니다.");
            }
        }
    }

    class Program
    {
        static void GuildMain()
        {
            Player player = new Player();
            bool nextPage = true;
            List<Quest> questList = DataManager.Instance.quests;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("========= Quest =========\n");
                Console.WriteLine("퀘스트를 선택할 수 있습니다.");

                for (int i = 0; i < questList.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {questList[i].Name}");
                }

                player.ProgressQuest();
                Console.WriteLine("\n0. 나가기");

                Console.Write("\n원하시는 퀘스트를 선택해주세요.\n>> ");
                if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 0 || choice > questList.Count)
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.ReadLine();
                    continue;
                }

                if (choice == 0) break;

                Quest selectedQuest = questList[choice - 1];
                selectedQuest.ShowFloorQuest();

                Console.WriteLine("1. 수락");
                Console.WriteLine("2. 거절");
                Console.Write("\n퀘스트를 수락하시겠습니까?\n>> ");
                string action = Console.ReadLine();

                if (action == "1")
                {
                    Console.Clear();
                    player.ActiveQuest = selectedQuest;
                    Console.WriteLine($"\n {selectedQuest.Name} 퀘스트를 수락했습니다!");
                }
                else if (action == "2")
                {
                    Console.WriteLine("\n 퀘스트를 거절했습니다.");
                }

                Console.WriteLine("\nEnter 키를 눌러 계속");
                Console.ReadLine();

                while (player.ActiveQuest != null)
                {
                    Console.Clear();

                    Console.WriteLine("1. 1층 오르기");
                    Console.WriteLine("2. 퀘스트 진행 확인");
                    Console.WriteLine("0. 나가기");

                    string questAction = Console.ReadLine();

                    if (questAction == "1")
                    {
                        player.ActiveQuest.CurrentFloors++;
                    }
                    else if (questAction == "2")
                    {
                        if (player.ActiveQuest.IsCompleted)
                        {
                            player.CompleteQuest();
                        }
                        else { }
                    }
                    else if (questAction == "0")
                    {
                        break;
                    }

                    Console.WriteLine("\nEnter 키를 눌러 계속");
                    Console.ReadLine();
                }
            }
        }
    }
}