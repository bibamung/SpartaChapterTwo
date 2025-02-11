using Sylphyr.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

using static System.Formats.Asn1.AsnWriter;

namespace Sylphyr.Dungeon
{
    public class DungeonScene
    {
        public void DisplayPlayerHpBar(Player player)
        {
            int barSize = 20; // 체력바 길이 (20칸)
            float healthPercentage = player.CurrentHp / player.TotalStat.MaxHp;
            int filledBars = (int)(barSize * healthPercentage);
            if (filledBars < 0) filledBars = 0;
            int emptyBars = barSize - filledBars;

            // 색상 적용 (콘솔 전용)
            Console.ForegroundColor = GetHealthColor(healthPercentage);

            // 체력바 출력
            Console.Write($" {player.Name} \n HP [");
            Console.Write(new string('■', filledBars)); // 채워진 부분
            Console.Write(new string('□', emptyBars));  // 빈 부분
            Console.Write($"] {(player.CurrentHp > 0 ? player.CurrentHp.ToString("F2") : 0)}/" +
                $"{player.TotalStat.MaxHp.ToString("F2")}\n");

            healthPercentage = player.CurrentMp / player.TotalStat.MaxMp;
            filledBars = (int)(barSize * healthPercentage);
            if (filledBars < 0) filledBars = 0;
            emptyBars = barSize - filledBars;
            // 마나바 출력
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write($" MP [");
            Console.Write(new string('■', filledBars)); // 채워진 부분
            Console.Write(new string('□', emptyBars));  // 빈 부분
            Console.Write($"] {(player.CurrentMp > 0 ? player.CurrentMp.ToString("F2") : 0)}/" +
                $"{player.TotalStat.MaxMp.ToString("F2")}");

            // 색상 초기화
            Console.ResetColor();
            Console.WriteLine();

        }

        public void DisplayHealthBar(List<Monster> stageMonsters)
        {
            int count = 1;
            Console.WriteLine($"\n====몬스터=====\n");
            foreach (Monster monster in stageMonsters)
            {
                int barSize = 20; // 체력바 길이 (20칸)
                float healthPercentage = monster.CurrentHp / monster.MaxHp;
                int filledBars = (int)(barSize * healthPercentage);
                if (filledBars < 0) filledBars = 0;
                int emptyBars = barSize - filledBars;

                // 색상 적용 (콘솔 전용)
                Console.ForegroundColor = GetHealthColor(healthPercentage);

                // 체력바 출력
                Console.Write($"{count++}. {monster.MonsterName} [");
                Console.Write(new string('■', filledBars)); // 채워진 부분
                Console.Write(new string('□', emptyBars));  // 빈 부분
                Console.Write($"] {(monster.CurrentHp > 0 ? monster.CurrentHp.ToString("F2") : 0)}/" +
                    $"{monster.MaxHp.ToString("F2")}");

                // 색상 초기화
                Console.ResetColor();
                Console.WriteLine();

            }

        }

        public void DisplayHealthBar(Monster monster)
        {
            int barSize = 20; // 체력바 길이 (20칸)
            float healthPercentage = monster.CurrentHp / monster.MaxHp;
            if (healthPercentage < 0) healthPercentage = 0;
            int filledBars = (int)(barSize * healthPercentage);
            int emptyBars = barSize - filledBars;
            if (monster.CurrentHp > 0)
            {
                if (filledBars < 0) filledBars = 0;
                // 색상 적용 (콘솔 전용)
                Console.ForegroundColor = GetHealthColor(healthPercentage);

                // 체력바 출력
                Console.Write($"{monster.MonsterName} [");
                Console.Write(new string('■', filledBars)); // 채워진 부분
                Console.Write(new string('□', emptyBars));  // 빈 부분
                Console.Write($"] {(monster.CurrentHp > 0 ? monster.CurrentHp.ToString("F2") : 0)}/" +
                    $"{monster.MaxHp.ToString("F2")}");
            }
            else
            {
                Console.ForegroundColor = GetHealthColor(healthPercentage);
                Console.Write($"{monster.MonsterName} [□□□□□□□□□□□□□□□□□□□□]{(monster.CurrentHp > 0 ? monster.CurrentHp.ToString("F2") : 0)}/" +
                    $"{monster.MaxHp.ToString("F2")}\n\n");
            }

            // 색상 초기화
            Console.ResetColor();
            Console.WriteLine();

        }

        //플레이어가 맞았을 때
        //DisplayHit(때린사람, 맞은 대상, 크리티컬 여부, 최종데미지)
        public void DisplayHit(Monster monster, Player player, bool isCritical, float finalDamage)
        {
            float playerDef = (player.TotalStat.Def / (player.TotalStat.Def + 50.0f)) * 100;
            if (isCritical) //크리티컬이 터졌습니다.
            {
                Console.WriteLine($"{monster.MonsterName}이/가 {player.Name}을 공격했다.");
                Console.WriteLine($"효과는 굉장했다.");
                Console.WriteLine($"{player.Name}에게 {(finalDamage - playerDef > 0 ? (finalDamage - playerDef).ToString("F2") : 0)}만큼 피해를 입혔다.");
                DisplayPlayerHpBar(player);

                Console.WriteLine("press any key to continue...");
                Console.ReadKey();
            }
            else            //크리티컬이 안 터졌습니다.
            {
                Console.WriteLine($"{monster.MonsterName}이/가 {player.Name}을 공격했다.");
                Console.WriteLine($"{player.Name}에게 {(finalDamage - playerDef > 0 ? (finalDamage - playerDef).ToString("F2") : 0)}만큼 피해를 입혔다.");
                DisplayPlayerHpBar(player);

                Console.WriteLine("press any key to continue...");
                Console.ReadKey();
            }
        }

        //플레이어가 때렸을 때 (기본공격)
        public void DisplayHit(Player player, Monster monster, bool isCritical, float finalDamage)
        {
            if (isCritical) //크리티컬이 터졌습니다.
            {
                Console.WriteLine($"{monster.MonsterName}를 공격했다.");
                Console.WriteLine($"효과는 굉장했다.");
                Console.WriteLine($"{monster.MonsterName}에게 {(finalDamage > 0 ? finalDamage.ToString("F2") : 0)}만큼 피해를 입혔다.");
                monster.CurrentHp -= finalDamage;
                DisplayHealthBar(monster);

                Console.WriteLine("press any key to continue...");
                Console.ReadKey();
            }
            else            //크리티컬이 안 터졌습니다.
            {
                Console.WriteLine($"{monster.MonsterName}를 공격했다.");
                Console.WriteLine($"{monster.MonsterName}에게 {(finalDamage > 0 ? finalDamage.ToString("F2") : 0)}만큼 피해를 입혔다.");
                monster.CurrentHp -= finalDamage;
                DisplayHealthBar(monster);

                Console.WriteLine("press any key to continue...");
                Console.ReadKey();
            }
        }
        //플레이어가 때렸을 때 (스킬공격)
        public void DisplaySkillHit(Player player, Monster monster, bool isCritical, float finalDamage, int useSkill)
        {
            if (isCritical) //크리티컬이 터졌습니다.
            {
                Console.WriteLine($"{monster.MonsterName}를 {player.learnedSkills[useSkill - 1].SkillName}으/로 공격했다.");
                Console.WriteLine($"효과는 굉장했다.");
                Console.WriteLine($"{monster.MonsterName}에게 {finalDamage.ToString("F2")}만큼 피해를 입혔다.");
                monster.CurrentHp -= finalDamage;
                DisplayHealthBar(monster);

                Console.WriteLine("press any key to continue...");
                Console.ReadKey();
            }
            else            //크리티컬이 안 터졌습니다.
            {
                Console.WriteLine($"{monster.MonsterName}를 {player.learnedSkills[useSkill - 1].SkillName}으/로 공격했다.");
                Console.WriteLine($"{monster.MonsterName}에게 {finalDamage.ToString("F2")}만큼 피해를 입혔다.");
                monster.CurrentHp -= finalDamage;
                DisplayHealthBar(monster);

                Console.WriteLine("press any key to continue...");
                Console.ReadKey();
            }
        }

        //플레이어가 때린걸 몬스터가 회피했을때
        //DisplayEvasion(때린 대상)
        public void DisplayEvasion(Player player)
        {
            Random rand = new Random(DateTime.Now.Millisecond);
            int text = rand.Next(0, 4);
            switch (text)
            {
                case 1:
                    Console.WriteLine($"{player.Name}의 공격이 빗나갔다.");
                    break;
                case 2:
                    Console.WriteLine($"{player.Name}가 멍 때리고 있어 공격을 하지 않았다.");
                    break;
                case 3:
                    Console.WriteLine($"{player.Name}가 공격을 하다가 돌부리에 걸려 넘어져 공격에 실패했다.");
                    break;
            }

            Console.WriteLine("press any key to continue...");
            Console.ReadKey();
        }

        public void DisplayEvasion(Monster monster)
        {
            Random rand = new Random(DateTime.Now.Millisecond);
            int text = rand.Next(0, 4);
            switch (text)
            {
                case 1:
                    Console.WriteLine($"{monster.MonsterName}의 공격이 빗나갔다.");
                    break;
                case 2:
                    Console.WriteLine($"{monster.MonsterName}가 멍 때리고 있어 공격을 하지 않았다.");
                    break;
                case 3:
                    Console.WriteLine($"{monster.MonsterName}가 자고 있다.");
                    break;
            }

            Console.WriteLine("press any key to continue...");
            Console.ReadKey();
        }

        // 체력 상태에 따라 색상 변경 (콘솔용)
        private ConsoleColor GetHealthColor(float percentage)
        {
            if (percentage > 0.6f) return ConsoleColor.Green;  // 60% 이상 - 초록
            if (percentage > 0.3f) return ConsoleColor.Yellow; // 30~60% - 노랑
            return ConsoleColor.Red;                           // 30% 이하 - 빨강
        }

        //플레이어 스킬 리스트 출력
        public void DisplayPlayerSkill(Player player)
        {
            int count = 1;
            for (int i = 0; i < player.learnedSkills.Count(); i++)
            {
                Console.WriteLine($"{count++}. {player.learnedSkills[i].SkillName} | 사용 마나: {player.learnedSkills[i].UseMp} | 스킬 설명: {player.learnedSkills[i].Desc}\n");
            }
        }


        //기본 공격
        public void BasicAttack(Player player, Monster monster)
        {
            Random rand = new Random(DateTime.Now.Millisecond);
            bool isCritical = false;
            float evasionRate = (monster.Dex / (monster.Dex + 50.0f));
            float monsterDef = (monster.Def / (monster.Def + 50.0f)) * 100.0f;
            if (rand.NextSingle() > evasionRate)
            {
                if (rand.NextSingle() < player.TotalStat.CriticalChance)
                {
                    float finalDamage = player.TotalStat.Atk * player.TotalStat.CriticalDamage - monsterDef;

                    DisplayHit(player, monster, isCritical, finalDamage);
                }
                else
                {
                    float finalDamage = player.TotalStat.Atk - monsterDef;

                    DisplayHit(player, monster, isCritical, finalDamage);
                }
            }
            else
            {
                DisplayEvasion(player);
            }
        }
        //스킬
        // 적 방어력을 계산하는 함수
        public void SkillAttack(Player player, Monster monster, int useSkill)
        {
            bool isCritical = false;
            Random rand = new Random(DateTime.Now.Millisecond);
            float evasionRate = (monster.Dex / (monster.Dex + 50.0f));
            float monsterDef = (monster.Def / (monster.Def + 50.0f)) * 100.0f;
            if (rand.NextSingle() > evasionRate)
            {
                if (rand.NextSingle() < player.TotalStat.CriticalChance)
                {
                    float finalDamage = player.TotalStat.Atk * player.TotalStat.CriticalDamage * player.learnedSkills[useSkill - 1].Damage
                        - monsterDef;

                    DisplaySkillHit(player, monster, isCritical, finalDamage, useSkill);
                }
                else
                {
                    float finalDamage = player.TotalStat.Atk * player.learnedSkills[useSkill - 1].Damage
                        - monsterDef;
                    Console.WriteLine("스킬 데미지: {0}", player.learnedSkills[useSkill - 1].Damage);
                    DisplaySkillHit(player, monster, isCritical, finalDamage, useSkill);
                }
            }
            else
            {
                DisplayEvasion(player);
            }
        }
        // 방어력 무시하는 함수
        public void DefIgnoreSkillAttack(Player player, Monster monster, int useSkill)
        {
            bool isCritical = false;
            Random rand = new Random(DateTime.Now.Millisecond);
            float evasionRate = (monster.Dex / (monster.Dex + 50.0f));
            if (rand.NextSingle() > evasionRate)
            {
                if (rand.NextSingle() < player.TotalStat.CriticalChance)
                {
                    float finalDamage = player.TotalStat.Atk * player.TotalStat.CriticalDamage * player.TotalStat.CriticalDamage * player.learnedSkills[useSkill - 1].Damage;

                    DisplayHit(player, monster, isCritical, finalDamage);
                }
                else
                {
                    float finalDamage = player.TotalStat.Atk * player.TotalStat.CriticalDamage * player.learnedSkills[useSkill - 1].Damage;

                    DisplayHit(player, monster, isCritical, finalDamage);
                }
            }
            else
            {
                DisplayEvasion(player);
            }
        }



        public void MonsterAttack(Monster monster, Player player)
        {
            bool isCritical = false;
            Random rand = new Random(DateTime.Now.Millisecond);
            float evasionRate = (player.TotalStat.Dex / (player.TotalStat.Dex + 50.0f));
            if (rand.NextSingle() > evasionRate)
            {
                if (rand.NextSingle() < monster.CriticalChance)        //크리티컬이 터졌을 경우
                {
                    isCritical = true;
                    float finalDamage = monster.Atk * monster.CriticalDamage;
                    player.TakeDamage(finalDamage);
                    DisplayHit(monster, player, isCritical, finalDamage);

                }
                else
                {
                    isCritical = false;
                    float finalDamage = monster.Atk;
                    player.TakeDamage(finalDamage);
                    DisplayHit(monster, player, isCritical, finalDamage);
                }
            }
            else
            {
                DisplayEvasion(monster);
            }

        }

        public void DisplayReward(Player player, int rewardGold, int rewardExp)
        {
            int totalGold = 0;
            Console.Clear();
            Console.WriteLine("******************************************************************************************");
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("*");
                Console.SetCursorPosition(90, i);
                Console.WriteLine("*");
                Console.SetCursorPosition(0, i);
            }
            Console.WriteLine("******************************************************************************************");

            Console.SetCursorPosition(2, 0);
            Console.WriteLine("\r\n*   _____                                   _           _         _    _               \r\n*  /  __ \\                                 | |         | |       | |  (_)              \r\n* | /  \\/  ___   _ __    __ _  _ __   __ _ | |_  _   _ | |  __ _ | |_  _   ___   _ __  \r\n* | |     / _ \\ | '_ \\  / _` || '__| / _` || __|| | | || | / _` || __|| | / _ \\ | '_ \\ \r\n* | \\__/\\| (_) || | | || (_| || |   | (_| || |_ | |_| || || (_| || |_ | || (_) || | | |\r\n*  \\____/ \\___/ |_| |_| \\__, ||_|    \\__,_| \\__| \\__,_||_| \\__,_| \\__||_| \\___/ |_| |_|\r\n*                        __/ |                                                         \r\n*                       |___/                                                          \r\n");

            Console.WriteLine("\n===========던전 클리어!!!!===========\n");

            Console.WriteLine($"  획득한 경험치 => {rewardExp}");
            Console.WriteLine($"  획득한 골드 => {totalGold}");

            Console.WriteLine("\n====================================\n");

            Console.WriteLine($"  현재 보유 골드 => {player.Gold}");
            Console.WriteLine($"  현재 플레이어 레벨 => {player.Level}");
            Console.WriteLine($"  현재 플레이어 현재 경험치 => {player.Exp}");

            Console.WriteLine("\n====================================\n");
            GameManager.Instance.shop.isShop = true;
        }

    }

}
