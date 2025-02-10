namespace Sylphyr.Character;

public class CharacterLevelData
{
    // 첫 exp
    private const int baseExp = 300;
    private const float exponent = 0.956f;

    private int[] expTable = new int[30];

    public CharacterLevelData()
    {
        expTable[0] = baseExp;
        for (int i = 1; i < expTable.Length; i++)
        {
            int baseValue = expTable[i - 1];
            expTable[i] = (int)Math.Pow(baseValue, exponent);

            if (i > 10)
            {
                expTable[i] = (int)((baseValue + 0.34f) * Math.Pow(expTable[i - 1], exponent));
            }
        }
    }
    
    public void printExpTable()
    {
        for (int i = 0; i < expTable.Length; i++)
        {
            Console.WriteLine($"Level {i + 1} : {expTable[i]}");
        }
    }
}