using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sylphyr.Character;

namespace Sylphyr
{
    public class GameManger : SingleTon<GameManger>
    {
        public Player player { get; } = new Player("어둠의다크 한빈정령", CharacterClass.Thief);
    }
}