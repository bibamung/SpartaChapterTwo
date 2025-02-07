using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sylphyr.KJE;

namespace Sylphyr
{
    public class GameManger : SingleTon<GameManger>
    {
        public Player player { get; } = new Player("성원",CharacterClass.Thief);
    }
}
