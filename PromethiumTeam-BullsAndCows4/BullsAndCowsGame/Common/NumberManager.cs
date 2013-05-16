namespace BullsAndCowsGame
{
    using System;
    using System.Text;

    //TODO : Refactor code, that manages number generation, bulls and cows checks and help/cheat functionality from original class "bikove_i_kravi" 
    static class NumberManager
    {
        public const int NUMBER_LENGHT = 4;
        public static string Number { get; private set; }

        public static void GenerateNumber()
        {
            StringBuilder number = new StringBuilder(4);
            Random randomNumberGenerator = new Random(DateTime.Now.Millisecond);

            for (int i = 0; i < NUMBER_LENGHT; i++)
            {
                int randomDigit = randomNumberGenerator.Next(9);
                number.Append(randomDigit);
            }

            Number = number.ToString();
        }

        public static void GetBullsAndCows(string playerInput, string generatedNumber, out int bullsCount, out int cowsCount)
        {
            bullsCount = 0;
            cowsCount = 0;
            StringBuilder playerNumber = new StringBuilder(playerInput);
            StringBuilder number = new StringBuilder(generatedNumber);
            for (int i = 0; i < playerNumber.Length; i++)
            {
                if (playerNumber[i] == number[i])
                {
                    bullsCount++;
                    playerNumber.Remove(i, 1);
                    number.Remove(i, 1);
                    i--;
                }
            }

            for (int i = 0; i < playerNumber.Length; i++)
            {
                for (int j = 0; j < number.Length; j++)
                {
                    if (playerNumber[i] == number[j])
                    {
                        cowsCount++;
                        playerNumber.Remove(i, 1);
                        number.Remove(j, 1);
                        j--;
                        i--;
                        break;
                    }
                }
            }
        }
    }
}
