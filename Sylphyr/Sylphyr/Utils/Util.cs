using Sylphyr.Character;

namespace Sylphyr.Utils;

public static class Util
{
    public static int GetInput(int min, int max, int HiddenInput = -1)
    {
        int input = 0;
        while (true)
        {
            Console.Write(">> ");
            if (int.TryParse(Console.ReadLine(), out input))
            {
                if (HiddenInput != -1 && input == HiddenInput)
                {
                    return input;
                }

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
    
    public static string GetClassName(this CharacterClass characterClass)
    {
        return characterClass switch
        {
            CharacterClass.Warrior => "전사",
            CharacterClass.Thief   => "도적",
            CharacterClass.Archer  => "궁수",
            CharacterClass.Paladin => "팔라딘",
            _ => throw new ArgumentOutOfRangeException(nameof(characterClass), characterClass, null)
        };
    }
}