using Sylphyr;
using Sylphyr.Character;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Sylphyr.YJH;
using System.Xml.Serialization;
using Sylphyr.Dungeon;

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
        //List<Quest> Quests = new List<Quest>();
        List<Quest> AcceptedQuests = new List<Quest>();
        List<Quest> CompletedQuests = new List<Quest>();
        DungeonManager dungeonManager = new DungeonManager();
        List<Quest> QuestList = DataManager.Instance.quests;

        /*
        public void GuildMain(Player player)
        {

            while (true)
            {
                int j = 0;
                Console.Clear();
                Console.WriteLine("========= Quest =========\n");
                Console.WriteLine("퀘스트를 선택할 수 있습니다.");

                for (int i = 0; i < QuestList.Count; i++)
                {
                    if (QuestList[i].Isclear && CompletedQuests.Count != 0)
                    {
                        Console.Write($"{i + 1}. {CompletedQuests[i].Name}");
                    }
                    else if (!QuestList[i].Isclear)
                    {
                        if (AcceptedQuests.Count != 0)
                        {
                            foreach (var accetQuest in AcceptedQuests)
                            {
                                if (QuestList[i].ID == accetQuest.ID) Console.Write($"{i + 1}. {accetQuest.Name}");
                            }
                            Console.Write($"{i + 1}. {QuestList[i].Name}");
                        }
                        else
                        {
                            Console.Write($"{i + 1}. {QuestList[i].Name}");
                        }
                    }
                    
                    if (AcceptedQuests.Count > j)
                    {
                        if (AcceptedQuests[j].Request && AcceptedQuests[j].ID == QuestList[i].ID)
                        {
                            // 진행도 출력
                            if (AcceptedQuests[j].ID % 1000 == 8)
                            {
                                Console.Write($"진행도: {DungeonManager.clearCount[6]} / {QuestList[j].RequiredFloors}");
                                if (DungeonManager.clearCount[6] == QuestList[j].RequiredFloors)
                                {
                                    AcceptedQuests[j].Isclear = true;
                                    CompletedQuests.Add(AcceptedQuests[j]);
                                    AcceptedQuests.RemoveAt(j);
                                }
                            }
                            else if ((AcceptedQuests[j].ID % 1000 == 5 && player.BestStage == 10) || (AcceptedQuests[j].ID % 1000 == 5 && player.BestStage == 40))
                            {
                                AcceptedQuests[j].Isclear = true;
                                CompletedQuests.Add(AcceptedQuests[j]);  // 퀘스트 완료
                                AcceptedQuests.RemoveAt(j);
                            }
                            else if (AcceptedQuests[j].ID % 1000 == 0 || AcceptedQuests[j].ID % 1000 == 3 || AcceptedQuests[j].ID % 1000 == 4)
                            {
                                int sum = 0;
                                for (int n = 0; n < DungeonManager.clearCount.Length; n++)
                                {
                                    sum += DungeonManager.clearCount[n];
                                }
                                Console.Write($" 진행도: {sum} / {QuestList[j].RequiredFloors}\t");

                                int questnum = AcceptedQuests[j].ID % 1000;
                                switch (questnum)
                                {
                                    case 0:
                                    case 3:
                                    case 4:
                                        if (sum == QuestList[j].RequiredFloors)
                                        {
                                            AcceptedQuests[j].Isclear = true;
                                            CompletedQuests.Add(AcceptedQuests[j]);
                                            AcceptedQuests.RemoveAt(j);
                                        }
                                        break;
                                }
                            }
                            if (AcceptedQuests.Count != 0 && !AcceptedQuests[j].Isclear)
                            {
                                Console.Write(" (수락 완료) ");
                            }
                            else
                            {
                                Console.Write(" (퀘스트 완료)");
                            }
                            j++;
                        }
                    }
                    Console.WriteLine();
                }

                Console.WriteLine("\n0. 나가기");
                Console.Write("\n원하시는 퀘스트를 선택해주세요.\n>> ");

                if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 0 || choice > QuestList.Count)
                {
                    Console.WriteLine("잘못된 입력입니다.엔터를 눌러주세요.");
                    Console.ReadLine();
                    continue;
                }

                if (choice == 0) return;

                Quest selectedQuest = QuestList[choice - 1];

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
                
                bool isint = int.TryParse(action, out int choosenum);
                if (!isint) Console.WriteLine("숫자를 입력해주세요.");
                    
                if (choosenum == 1)
                {
                    if (AcceptedQuests.Count == 0)
                    {
                        Console.Clear();
                        AcceptedQuests.Add(selectedQuest);
                        selectedQuest.Request = true;
                        Console.WriteLine($"\n {selectedQuest.Name} 퀘스트를 수락했습니다!");
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("이미 수령한 퀘스트가 있습니다.");
                    }
                }
                else if (choosenum == 2)
                {
                    selectedQuest.Request = false;
                    AcceptedQuests.Remove(selectedQuest);
                    Console.WriteLine("\n 퀘스트를 거절했습니다.");
                }

                Console.WriteLine("\n엔터를 눌러주세요.");
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
                        CompleteQuest();
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
        */

        public void DungeonCountClearQuest()
        {
            foreach (var select in QuestList)
            {
                switch (select.ID % 1000)
                {
                    case 0:
                    case 3:
                    case 4:

                        break;
                    default:
                        break;
                }
            }
        }


        public void BestStageQuest()  // 층 도달 미션
        {
            foreach (var select in QuestList)
            {
                switch (select.ID % 1000)
                {
                    case 5:
                    case 9:

                        break;
                    default:
                        break;
                }
            }
        }



        public void ShopPurchaseQuest()
        {
            foreach (var select in QuestList)
            {
                switch (select.ID % 1000)
                {
                    case 1:
                    case 6:
                    case 7:

                        break;
                    default:
                        break;
                }
            }
        }


        public void SelectQuest(int selectQuest)
        {
            if (selectQuest < 0 || selectQuest > QuestList.Count)
            {
                Console.WriteLine("잘못된 입력입니다.엔터를 눌러주세요.");
                Console.ReadLine();
            }

            if (selectQuest == 0) return;

            Quest selectedQuest = QuestList[selectQuest - 1];

            if (CompletedQuests.Contains(selectedQuest))
            {
                Console.WriteLine("\n이미 완료한 퀘스트입니다.엔터를 눌러주세요.");
                Console.ReadLine();
            }

            selectedQuest.ShowQuest();

            Console.WriteLine("1. 수락");
            Console.WriteLine("2. 거절");

            int accept;
            bool isint = int.TryParse(Console.ReadLine(), out accept);
            if (!isint) Console.WriteLine("숫자를 입력해주세요.");

            else
            {
                if (accept == 1)
                {
                    if (AcceptedQuests.Count == 0)
                    {
                        Console.Clear();
                        AcceptedQuests.Add(selectedQuest);
                        selectedQuest.Request = true;
                        Console.WriteLine($"\n {selectedQuest.Name} 퀘스트를 수락했습니다!");
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("이미 수령한 퀘스트가 있습니다.");
                    }
                }
                else if (accept == 2)
                {
                    selectedQuest.Request = false;
                    AcceptedQuests.Remove(selectedQuest);
                    Console.WriteLine("\n 퀘스트를 거절했습니다.");
                }
            }
            
            Console.WriteLine("\n엔터를 눌러주세요.");
            Console.ReadLine();
        }


        public void GuildMain(Player player)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("========= Quest =========\n");
                Console.WriteLine("퀘스트를 선택할 수 있습니다.");
                int count = 0;
                foreach (var select in QuestList)
                {
                    Console.Write($"{count + 1}. {select.Name}");
                    if (select.Isclear)
                    {
                        Console.WriteLine("\t(완료된 퀘스트)");
                    }
                    else if (AcceptedQuests.Contains(select))
                    {
                        Console.WriteLine("\t(수락한 퀘스트)");
                    }
                    else
                    {
                        Console.WriteLine();
                    }

                    count++;
                }
                Console.WriteLine("\n0. 나가기");
                Console.Write("\n원하시는 퀘스트를 선택해주세요.\n>> ");

                //퀘스트 수락, 거절
                int selectQuest;
                bool isVaildNum = int.TryParse(Console.ReadLine(), out selectQuest);
                if (isVaildNum)
                {
                    if (selectQuest == 0)
                    {
                        break;
                    }
                    else
                    {
                        SelectQuest(selectQuest);
                    }
                }
                else Console.WriteLine("Please press any key...");

            }
        }
    }
}