using Sylphyr;
using Sylphyr.Character;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Sylphyr.YJH;
using System.Xml.Serialization;

namespace Guild
{
    public class Quest
    {
        //길드창-퀘스트목록 퀘스트진행상태
        //퀘스트이름 내용 보상 1수락 2거절
        public int ID;
        public string Name;
        public string Desc;
        public int RewardExp;
        public int RewardGold;
        public int RequiredFloors;//목표
        public int CurrentFloors;  //현재
        public int MaxFloors;
        public int RequiredBuyItems;
        public int CurrentBuyItems;
        public int RequiredSellItems;
        public int CurrentSellItems;
        public bool IsFloorsCompleted => CurrentFloors >= RequiredFloors;
        public bool IsBuyItemsCompleted => CurrentBuyItems >= RequiredBuyItems;
        public bool IsSellItemsCompleted => CurrentSellItems >= RequiredSellItems;
        public bool Isclear { get; set; }
        public bool Request { get; set; }

        public Quest(int id, string name, string desc, int rewardExp, int rewardGold, int requiredFloors, int requiredBuyItems,
                     int requiredSellItems, bool isclear, bool request)
        {
            ID = id;
            Name = name;
            Desc = desc;
            RewardExp = rewardExp;
            RewardGold = rewardGold;
            RequiredFloors = requiredFloors;
            CurrentFloors = 0;
            RequiredBuyItems = requiredBuyItems;
            CurrentBuyItems = 0;
            RequiredSellItems = requiredSellItems;
            CurrentSellItems = 0;
            Isclear = isclear;
            Request = request;
        }

        public void ShowQuest() //퀘스트내용
        {
            Console.Clear();
            Console.WriteLine("===== Quest!! =====\n");
            Console.WriteLine(Name);
            Console.WriteLine();
            Console.WriteLine(Desc);
            Console.WriteLine("\n- 보상 -");
            Console.WriteLine($"  Gold: {RewardGold}");
            Console.WriteLine($"  Exp: {RewardExp}\n");
        }
    }

    public class Guild
    {
        List<Quest> Quests = new List<Quest>();
        List<Quest> AcceptedQuests = new List<Quest>();
        List<Quest> CompletedQuests = new List<Quest>();
        public Quest ActiveQuest { get; private set; }


        public void CompleteQuest()
        {
            if (ActiveQuest.IsFloorsCompleted || ActiveQuest.IsBuyItemsCompleted || ActiveQuest.IsSellItemsCompleted)
            {
                Console.WriteLine($"{ActiveQuest.Name} 완료!");
                Console.WriteLine($"{ActiveQuest.RewardGold} 골드 & {ActiveQuest.RewardExp} 경험치를 획득!\n");

                GameManager.Instance.player.AddGold(ActiveQuest.RewardGold);
                GameManager.Instance.player.AddExp(ActiveQuest.RewardExp);
                CompletedQuests.Add(ActiveQuest);
                ActiveQuest = null;
            }
            else
            {
                Console.WriteLine("진행 중인 퀘스트의 조건이 충족되지 않았습니다.");
            }
        }




        public void ProgressQuest()
        {
            if (ActiveQuest == null)
            {
                Console.WriteLine("\n진행중인 퀘스트가 없습니다.");
            }

            else if (ActiveQuest.IsFloorsCompleted || ActiveQuest.IsBuyItemsCompleted || ActiveQuest.IsSellItemsCompleted)
            {
                Console.WriteLine("\n완료할 퀘스트가 있습니다.");
            }
            else 
            { 
                Console.WriteLine("\n진행중인 퀘스트가 있습니다.");
                return;
            }
        }

        public void Start()
        {
            Quests = DataManager.Instance.quests;
        }

        public void ShowFloorsProgress()
        {
            if (ActiveQuest == null) return;
            Console.WriteLine($"현재 클리어한 횟수 {ActiveQuest.CurrentFloors} / {ActiveQuest.RequiredFloors - 1}");
            if (ActiveQuest.CurrentFloors >= ActiveQuest.RequiredFloors - 1)
            {
                ActiveQuest.CurrentFloors = ActiveQuest.RequiredFloors;
            }
        }
        public void ShowBuyItemsProgress()
        {
            if (ActiveQuest == null) return;
            Console.WriteLine($"현재 구매한 횟수 {ActiveQuest.CurrentBuyItems} / {ActiveQuest.RequiredBuyItems - 1}");
            if (ActiveQuest.CurrentBuyItems >= ActiveQuest.RequiredBuyItems - 1)
            {
                ActiveQuest.CurrentBuyItems = ActiveQuest.RequiredBuyItems;
            }
        }
        public void ShowSellItemsProgress()
        {
            if (ActiveQuest == null) return;
            Console.WriteLine($"현재 판매한 횟수 {ActiveQuest.CurrentSellItems} / {ActiveQuest.RequiredSellItems - 1}");
            if (ActiveQuest.CurrentSellItems >= ActiveQuest.RequiredSellItems - 1)
            {
                ActiveQuest.CurrentSellItems = ActiveQuest.RequiredSellItems;
            }
        }

        public void ShowMaxFloors()
        {
            if (ActiveQuest == null) return;
            Console.WriteLine($"현재 최대 오른 층 {ActiveQuest.MaxFloors}");
        }

        public void GuildMain(Player player)
        {
            List<Quest> questList = DataManager.Instance.quests;

            while (true)
            {
                int j = 0;
                Console.Clear();
                Console.WriteLine("========= Quest =========\n");
                Console.WriteLine("퀘스트를 선택할 수 있습니다.");

                for (int i = 0; i < questList.Count; i++)
                {
                    Console.Write($"{i + 1}. {questList[i].Name}");
                    if (AcceptedQuests.Count > j)
                    {
                        if (AcceptedQuests[j].Request && AcceptedQuests[j].ID == questList[i].ID)
                        {
                            Console.Write(" (수락 완료)");
                            j++;
                        }
                    }
                    if (i < Quests.Count && CompletedQuests.Contains(Quests[i]))
                    {
                        Console.Write(" (퀘스트 완료)");
                    }
                    Console.WriteLine();
                }

                ProgressQuest();

                Console.WriteLine("\n0. 나가기");
                Console.Write("\n원하시는 퀘스트를 선택해주세요.\n>> ");

                if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 0 || choice > questList.Count)
                {
                    Console.WriteLine("잘못된 입력입니다.엔터를 눌러주세요.");
                    Console.ReadLine();
                    continue;
                }

                if (choice == 0) return;

                Quest selectedQuest = questList[choice - 1];

                if (CompletedQuests.Contains(selectedQuest))
                {
                    Console.WriteLine("\n이미 완료한 퀘스트입니다.엔터를 눌러주세요.");
                    Console.ReadLine();
                    continue;
                }

                selectedQuest.ShowQuest();

                Console.WriteLine("1. 수락");
                Console.WriteLine("2. 거절");

                string action = Console.ReadLine();
                int choosenum = int.Parse(action);

                if (choosenum == 1)
                {
                    Console.Clear();
                    AcceptedQuests.Add(selectedQuest);
                    selectedQuest.Request = true;
                    Console.WriteLine($"\n {selectedQuest.Name} 퀘스트를 수락했습니다!");
                }
                else if (choosenum == 2)
                {
                    selectedQuest.Request = false;
                    AcceptedQuests.Remove(selectedQuest);
                    Console.WriteLine("\n 퀘스트를 거절했습니다.");
                }

                Console.WriteLine("\n엔터를 눌러주세요.");
                Console.ReadLine();

                /*
                while (ActiveQuest != null)
                {
                    Console.Clear();
                    Console.WriteLine("1. 1층 오르기");
                    Console.WriteLine("2. 1개 사기");
                    Console.WriteLine("3. 1개 팔기");
                    Console.WriteLine("4. 퀘스트 진행 확인");
                    Console.WriteLine("0. 나가기");

                    if (choice == 1 || choice == 4 || choice == 5)
                    {
                        ShowFloorsProgress();
                    }
                    else if (choice == 2 || choice == 7 || choice == 8)
                    {
                        ShowBuyItemsProgress();
                    }
                    else if (choice == 3)
                    {
                        ShowSellItemsProgress();
                    }
                    else if (choice == 6 || choice == 9 || choice == 10)
                    {
                        ShowMaxFloors();
                    }

                    string questAction = Console.ReadLine();

                    if (questAction == "1")
                    {
                        ActiveQuest.CurrentFloors++;
                    }
                    else if (questAction == "2")
                    {
                        ActiveQuest.CurrentBuyItems++;
                    }
                    else if (questAction == "3")
                    {
                        ActiveQuest.CurrentSellItems++;
                    }
                    else if (questAction == "4")
                    {
                        CompleteQuest();
                    }
                    else if (questAction == "0")
                    {
                        break;
                    }

                    Console.WriteLine("\nEnter 키를 눌러 계속");
                    Console.ReadLine();
                }
                */
            }
        }
    }
}