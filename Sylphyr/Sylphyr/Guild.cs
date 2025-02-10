using Sylphyr;
using Sylphyr.Character;
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
        //인벤.카운트
        public int ID;
        public string Name;
        public string Description;
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

        public Quest(int id, string name, string description, int rewardExp, int rewardGold, int requiredFloors, int requiredBuyItems,
                     int requiredSellItems)
        {
            ID = id;
            Name = name;
            Description = description;
            RewardExp = rewardExp;
            RewardGold = rewardGold;
            RequiredFloors = requiredFloors;
            CurrentFloors = 0;
            RequiredBuyItems = requiredBuyItems;
            CurrentBuyItems = 0;
            RequiredSellItems = requiredSellItems;
            CurrentSellItems = 0;
        }

        public void ShowQuest() //퀘스트내용
        {
            Console.Clear();
            Console.WriteLine("===== Quest!! =====\n");
            Console.WriteLine(Name);
            Console.WriteLine(Description);
            Console.WriteLine("\n- 보상 -");
            Console.WriteLine($"  Gold: {RewardGold}");
            Console.WriteLine($"  Exp: {RewardExp}\n");
        }
    }

    class Guild
    {
        List<Quest> Quests = new List<Quest>();
        public Quest ActiveQuest { get; set; } = null; // 현재 진행 중인 퀘스트

        public void CompleteQuest()
        {
            if (ActiveQuest == null) return;

            if (ActiveQuest.IsFloorsCompleted || ActiveQuest.IsBuyItemsCompleted || ActiveQuest.IsSellItemsCompleted)
            {
                Console.Clear();
                Console.WriteLine($" {ActiveQuest.Name} 완료!");
                Console.WriteLine($" {ActiveQuest.RewardGold} Gold & {ActiveQuest.RewardExp} Exp 획득!\n");

                GameManager.Instance.player.AddGold(ActiveQuest.RewardGold, false);
                GameManager.Instance.player.AddExp(ActiveQuest.RewardExp);
                ActiveQuest = null;
            }

            else
            {
                Console.WriteLine("퀘스트 완료 조건을 충족하지 않았습니다.");
            }
        }

        public void ProgressQuest()
        {
            if (ActiveQuest == null)
            {
                Console.WriteLine("진행중인 퀘스트가 없습니다.");
            }

            else if (ActiveQuest.IsFloorsCompleted || ActiveQuest.IsBuyItemsCompleted || ActiveQuest.IsSellItemsCompleted)
            {
                Console.WriteLine("완료할 퀘스트가 있습니다.");
            }
        }

        public void Start()
        {
            Quests = DataManager.Instance.quests;
        }

        public void ShowFloorsProgress()
        {
            Console.WriteLine($"현재 클리어한 횟수 {ActiveQuest.CurrentFloors} / {ActiveQuest.RequiredFloors}");
            if (ActiveQuest.CurrentFloors > ActiveQuest.RequiredFloors)
            {
                ActiveQuest.CurrentFloors = ActiveQuest.RequiredFloors;
            }
        }
        public void ShowBuyItemsProgress()
        {
            Console.WriteLine($"현재 구매한 횟수 {ActiveQuest.CurrentBuyItems} / {ActiveQuest.RequiredBuyItems}");
            if (ActiveQuest.CurrentBuyItems > ActiveQuest.RequiredBuyItems)
            {
                ActiveQuest.CurrentBuyItems = ActiveQuest.RequiredBuyItems;
            }
        }
        public void ShowSellItemsProgress()
        {
            Console.WriteLine($"현재 판매한 횟수 {ActiveQuest.CurrentSellItems} / {ActiveQuest.RequiredSellItems}");
            if (ActiveQuest.CurrentSellItems > ActiveQuest.RequiredSellItems)
            {
                ActiveQuest.CurrentSellItems = ActiveQuest.RequiredSellItems;
            }
        }

        public void ShowMaxFloors()
        {
            Console.WriteLine($"현재 최대 오른 층 {ActiveQuest.MaxFloors}");
        }

        void GuildMain(Player player)
        {
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

                ProgressQuest();
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
                selectedQuest.ShowQuest();

                Console.WriteLine("1. 수락");
                Console.WriteLine("2. 거절");
                Console.Write("\n퀘스트를 수락하시겠습니까?\n>> ");
                string action = Console.ReadLine();

                if (action == "1")
                {
                    Console.Clear();
                    ActiveQuest = selectedQuest;
                    Console.WriteLine($"\n {selectedQuest.Name} 퀘스트를 수락했습니다!");
                }
                else if (action == "2")
                {
                    Console.WriteLine("\n 퀘스트를 거절했습니다.");
                }

                Console.WriteLine("\nEnter 키를 눌러 계속");
                Console.ReadLine();

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
                        if (ActiveQuest.IsFloorsCompleted)
                        {
                            CompleteQuest();
                        }
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