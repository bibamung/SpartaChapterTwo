using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sylphyr.Character;
using Sylphyr.Scene;

namespace Sylphyr
{
    public class GameManger : SingleTon<GameManger>
    {
        public MainScene Main { get; private set; }
        public Player player { get; private set; }

        public void Init()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Main = new MainScene();
        }
        
        public void SetPlayer(string name, CharacterClass characterClass)
        {
            player = new Player(name, characterClass);
        }
    }
}