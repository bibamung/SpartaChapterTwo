using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sylphyr.Character;
using Sylphyr.Scene;

namespace Sylphyr
{
    public class GameManager : SingleTon<GameManager>
    {
        public MainScene main { get; private set; }
        public Player player { get; private set; }
        public Item shop { get; private set; }
        public Inventory inventory { get; private set; }

        public void Init()
        {
            Console.ResetColor();
            main = new MainScene();
            shop = new Item(10, "qwr", 1, 1, "2", 10, "12", false, false);
            inventory = new Inventory();
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