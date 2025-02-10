namespace Sylphyr.Character;

public class CharacterLevelData
{
    // 첫 exp
    private const double baseExp = 300d;
    private const double multiplier = 0.34;
    private const double exponent = 0.956d;

    private double[] expTable = new double[29];

    public CharacterLevelData()
    {
        double baseValue = baseExp + Math.Pow(baseExp, exponent);
        expTable[0] = baseValue;
        for (int i = 1; i < expTable.Length; i++)
        {
            double prevLevelExp = expTable[i - 1];
            if (i < 10)
            {
                expTable[i] = prevLevelExp + Math.Pow(prevLevelExp, exponent);
            }
            else
            {
                expTable[i] = prevLevelExp + multiplier * Math.Pow(prevLevelExp, exponent);
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

    public int GetExp(int level)
    {
        return (int)expTable[level - 1];
    }
}