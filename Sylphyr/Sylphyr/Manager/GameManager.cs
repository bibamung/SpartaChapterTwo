using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Sylphyr.Character;
using Sylphyr.Scene;
using Sylphyr.YJH;

namespace Sylphyr
{
    public class GameManager : SingleTon<GameManager>
    {
        public MainScene main { get; private set; }
        public Player player { get; private set; }
        public Item shop { get; private set; }
        public Inventory inventory { get; private set; }
        public HealingHouse healingHouse { get; private set; }
        public Guild.Quest quest { get; private set; }
        public Guild.Guild guild { get; private set; }

        public void Init()
        {
            Console.ResetColor();
            main = new MainScene();
            shop = new Item(10, "qwr", 1, 1, "2", 10, "12", false, false);
            inventory = new Inventory();
            quest = new Guild.Quest(1, "sfds", "sdfsdf", 0, 0, 0, 1, 1, false, false);
            guild = new Guild.Guild();
            healingHouse = new();
        }
        public void SetMain()
        {
            Console.ResetColor();
            main = new MainScene();
            shop = new Item(10, "qwr", 1, 1, "2", 10, "12", false, false);
            inventory = new Inventory();
            quest = new Guild.Quest(1, "sfds", "sdfsdf", 0, 0, 0, 1, 1, false, false);
            guild = new Guild.Guild();
        }
        public void SetPlayer(string name, CharacterClass characterClass)
        {
            player = new Player(name, characterClass);
        }

        public void GameOver()
        {
            main = null;
            player = null;
            shop = null;
            inventory = null;
        }
    }
}