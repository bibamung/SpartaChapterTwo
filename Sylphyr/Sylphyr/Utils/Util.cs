namespace Sylphyr.Utils;

public static class Util
{
    public static int GetInput(int min, int max)
    {
        int input = 0;
        while (true)
        {
            Console.Write(">> ");
            if (int.TryParse(Console.ReadLine(), out input))
            {
                if (input >= min && input <= max)
                {
                    return input;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                }
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }
        }
    }
}