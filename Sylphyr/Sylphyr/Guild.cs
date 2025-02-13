using Sylphyr;
using Sylphyr.Character;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Sylphyr.YJH;
using System.Xml.Serialization;
using Sylphyr.Dungeon;
using System.Reflection.Emit;
using System.Security.Claims;
using System.Xml.Linq;

namespace Guild
{
    public class Quest
    {
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
        public List<Quest> QuestList = DataManager.Instance.quests;


        public void GuildMain(Player player)
        {

            while (true)
            {
                int j = 0;
                Console.Clear();
                Console.WriteLine("========= Guild =========\n");
                Console.WriteLine("길드에 오신것을 환영합니다.");
                Console.WriteLine("퀘스트를 클리어하여 보상을 받아가세요!\n");

                for (int i = 0; i < QuestList.Count; i++)
                {
                    if (QuestList[i].Isclear && CompletedQuests.Count != 0)
                    {
                        foreach (var qqq in CompletedQuests)
                        {
                            if (qqq == QuestList[i]) Console.Write($"{i + 1}. {qqq.Name}");
                        }
                    }
                    else if (!QuestList[i].Isclear)
                    {
                        if (AcceptedQuests.Count != 0)
                        {
                            if (QuestList[i].ID == AcceptedQuests[0].ID) Console.Write($"{i + 1}. {AcceptedQuests[0].Name}");
                            else Console.Write($"{i + 1}. {QuestList[i].Name}");
                        }
                        else
                        {
                            Console.Write($"{i + 1}. {QuestList[i].Name}");
                        }
                    }

                    if (AcceptedQuests.Count > j)
                    {
                        if (AcceptedQuests[0].Request && AcceptedQuests[0].ID == QuestList[i].ID)
                        {
                            // 진행도 출력
                            if (AcceptedQuests[0].ID % 1000 == 8)
                            {
                                Console.Write($"\t 진행도: {DungeonManager.clearCount[6]} / {QuestList[i].RequiredFloors}");
                                if (DungeonManager.clearCount[6] == QuestList[i].RequiredFloors)
                                {
                                    AcceptedQuests[0].Isclear = true;
                                    CompletedQuests.Add(AcceptedQuests[0]);

                                }
                            }
                            else if ((AcceptedQuests[0].ID % 1000 == 5 && player.BestStage == 10) || (AcceptedQuests[0].ID % 1000 == 5 && player.BestStage == 40))
                            {
                                AcceptedQuests[0].Isclear = true;
                                CompletedQuests.Add(AcceptedQuests[0]);  // 퀘스트 완료

                            }
                            else if (AcceptedQuests[0].ID % 1000 == 1 || AcceptedQuests[0].ID % 1000 == 6 || AcceptedQuests[0].ID % 1000 == 7)
                            {
                                if (GameManager.Instance.quest.CurrentBuyItems > QuestList[i].RequiredBuyItems)
                                    GameManager.Instance.quest.CurrentBuyItems = QuestList[i].RequiredBuyItems;
                                Console.Write($"\t 진행도: {GameManager.Instance.quest.CurrentBuyItems} / {QuestList[i].RequiredBuyItems} \t");
                                if (GameManager.Instance.quest.CurrentBuyItems >= QuestList[i].RequiredBuyItems)
                                {
                                    AcceptedQuests[0].Isclear = true;
                                    CompletedQuests.Add(AcceptedQuests[0]);


                                }
                            }
                            else if (AcceptedQuests[0].ID % 1000 == 2)
                            {
                                if (GameManager.Instance.quest.CurrentSellItems > QuestList[i].RequiredBuyItems)
                                    GameManager.Instance.quest.CurrentSellItems = QuestList[i].RequiredBuyItems;
                                Console.Write($"\t 진행도: {GameManager.Instance.quest.CurrentSellItems} / {QuestList[i].RequiredSellItems} \t");
                                if (GameManager.Instance.quest.CurrentSellItems >= QuestList[i].RequiredSellItems)
                                {
                                    AcceptedQuests[0].Isclear = true;
                                    CompletedQuests.Add(AcceptedQuests[0]);


                                }
                            }
                            else if (AcceptedQuests[0].ID % 1000 == 0 || AcceptedQuests[0].ID % 1000 == 3 || AcceptedQuests[0].ID % 1000 == 4)
                            {
                                int sum = 0;
                                for (int n = 0; n < DungeonManager.clearCount.Length; n++)
                                {
                                    sum += DungeonManager.clearCount[n];
                                }
                                Console.Write($"\t 진행도: {sum} / {QuestList[i].RequiredFloors} 층 \t");

                                int questnum = AcceptedQuests[0].ID % 1000;
                                switch (questnum)
                                {
                                    case 0:
                                    case 3:
                                    case 4:
                                        if (sum == QuestList[i].RequiredFloors)
                                        {
                                            AcceptedQuests[0].Isclear = true;
                                            DungeonManager.clearCount = new int[DungeonManager.clearCount.Length];
                                            CompletedQuests.Add(AcceptedQuests[0]);

                                        }
                                        break;
                                }
                            }
                            if (AcceptedQuests.Count != 0 && !AcceptedQuests[0].Isclear)
                            {
                                Console.Write(" (수락 완료) ");
                            }
                            else
                            {
                                Console.Write(" (퀘스트 완료) ");
                            }
                            j++;
                        }
                    }
                    Console.WriteLine();
                }

                Console.WriteLine("\n0. 나가기\n");
                if (AcceptedQuests.Count > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"{AcceptedQuests[0].Name} 진행중");
                    Console.ResetColor();
                }

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
                    selectedQuest.ShowQuest();
                    Console.WriteLine("\n수고하셨습니다. 엔터를 눌러 보상을 받아가세요.");
                    Console.WriteLine($"\n{selectedQuest.RewardGold}G , {selectedQuest.RewardExp} 경험치");
                    player.AddGold(selectedQuest.RewardGold);
                    player.AddExp(selectedQuest.RewardExp);
                    GameManager.Instance.quest.CurrentBuyItems = 0;
                    GameManager.Instance.quest.CurrentSellItems = 0;
                    AcceptedQuests.RemoveAt(0);
                    QuestList.Remove(selectedQuest);
                    Console.ReadLine();
                    continue;
                }

                selectedQuest.ShowQuest();

                Console.WriteLine("1. 수락");
                Console.WriteLine("2. 거절");

                string action = Console.ReadLine();

                bool isint = int.TryParse(action, out int choosenum);
                if (choosenum != 1 && choosenum != 2) Console.WriteLine("1 또는 2를 입력해주세요.");

                if (choosenum == 1)
                {
                    if (AcceptedQuests.Count == 0)
                    {
                        AcceptedQuests.Add(selectedQuest);
                        selectedQuest.Request = true;
                        Console.WriteLine($"\n {selectedQuest.Name} 퀘스트를 수락했습니다!");
                    }
                    else
                    {
                        Console.WriteLine("\n 이미 수령한 퀘스트가 있습니다.");
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
            }
        }

        public List<QuestData> ToQuestList()
        {
            List<QuestData> questDatas = new List<QuestData>();

            foreach (var item in QuestList)
            {
                questDatas.Add(new QuestData(item.ID, item.Name, item.Desc, item.RewardExp, item.RewardGold, item.RequiredFloors,item.CurrentFloors, item.MaxFloors, item.RequiredBuyItems, item.CurrentBuyItems, item.RequiredSellItems, item.CurrentSellItems, item.Isclear, item.Request));
            }
            return questDatas;
        }

        public void InitializeQuset(GameData gameData)
        {
            Console.WriteLine("InitializePlayer 진입");
            if (gameData == null)
            {
                Console.WriteLine("GameData가 없습니다. 초기화에 실패했습니다.");
                return;
            }
            Console.WriteLine("if 스킵 성공");
            // GameData 데이터를 Player에 적용

            foreach (var item in gameData.quests)
            {
                QuestList.Add(new Quest(item.ID, item.Name, item.Desc, item.RewardExp, item.RewardGold, item.RequiredFloors, item.RequiredBuyItems, item.RequiredSellItems, item.Isclear, item.Request));

            }

            Console.WriteLine("Player가 GameData를 사용하여 성공적으로 초기화되었습니다.");
        }

    }
}