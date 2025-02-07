using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Sylphyr.Dungeon
{
    public class MonsterList
    {
        Random random = new Random(DateTime.Now.Millisecond); //DateTime.Now.Millisecond
        public List<Monster> MonsterLists { get; set; }

        public MonsterList()
        {
            //Monster(int monsterId, string monsterName, float MaxHp, float hp, float atk, float def, float luk, float critical, float dex, float criticalChance, float critcalDamage, int speed, int dropGold, float dropExp)
            //슬라임, 고블린, 홉고블린 오우거 오크 리자드 드래곤 스켈레톤 좀비
            MonsterLists = new List<Monster>()
            {
                new Monster(1000,"슬라임",100,100,5,5,5,0.1f,5,5,5,5,5,5),
                new Monster(1001,"고블린",100,100,5,5,5,0.1f,5,5,5,5,5,5),
                new Monster(1002,"홉고블린",100,100,5,5,5,0.1f,5,5,5,5,5,5),
                new Monster(1003,"오우거",100,100,5,5,5,0.1f,5,5,5,5,5,5),
                new Monster(1004,"오크",100,100,5,5,5,0.1f,5,5,5,5,5,5),
                new Monster(1005,"리자드",100,100,5,5,5,0.1f,5,5,5,5,5,5),
                new Monster(1006,"스켈레톤",100,100,5,5,5,0.1f,5,5,5,5,5,5),
                new Monster(1007,"좀비",100,100,5,5,5,0.1f,5,5,5,5,5,5),
                new Monster(1008,"드래곤",100,100,5,5,5,0.1f,5,5,5,5,5,5)
            };

        }
        public List<Monster> GetRandomMonsters(int stage)
        {
            int monsterCount = random.Next(1, 5); // 1~4개 등장
            List<Monster> selectedMonsters = new List<Monster>();

            // 몬스터를 랜덤하게 선택하면서 중복을 최대 2번까지만 허용
            while (selectedMonsters.Count < monsterCount)
            {
                Monster randomMonster = MonsterLists[random.Next(MonsterLists.Count)];
                int existingCount = selectedMonsters.Count(m => m.MonsterId == randomMonster.MonsterId);

                if (existingCount < 2) // 같은 몬스터가 2번 미만일 때 추가
                {
                    selectedMonsters.Add(randomMonster);
                }
            }

            return selectedMonsters;
        }
    }
}
