namespace Sylphyr.YJH;
using Player = Sylphyr.Character.Player;
using CharacterStat = Sylphyr.Character.CharacterStat;
using Saving = Sylphyr.YJH.Save;

public class SaveData
{
    public List<CharacterStat> CharacterStats { get; set; } = new List<CharacterStat>();
    public List<Inventory> Inventories { get; set; } = new List<Inventory>();
    public List<Player> Players { get; set; } = new List<Player>();
}