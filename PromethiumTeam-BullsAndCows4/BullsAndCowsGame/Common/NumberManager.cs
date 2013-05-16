//-----------------------------------------------------------------------
// <copyright file="NumberManager.cs" company="TelerikAcademy">
//     All rights reserved © Telerik Academy 2012-2013
// </copyright>
//-----------------------------------------------------------------------

namespace BullsAndCowsGame
{
    using System;
    using System.Text;

    /// <summary>
    /// Static Class for that manages number generation
    /// </summary>
    static class NumberManager
    {
        /// <summary>
        /// Constant representing the number of digits <see cref="System.Int32"/>
        /// for the generated number
        /// </summary>
        public const int NUMBER_LENGHT = 4;

        /// <summary>
        /// A <see cref="System.String"/> which is the random generated four digit number
        /// converted to string
        /// </summary>
        public static string Number { get; private set; }

        /// <summary>
        /// Method for generating a random 4 digit number
        /// </summary>
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

        /// <summary>
        /// Method that compares <paramref name="playerInput"/> with
        /// <paramref name="generatedNumber"/> and takes out the coincidental
        /// <paramref name="bullsCount"/> and <paramref name="cowsCount"/>
        /// </summary>
        /// <param name="playerInput">Four digit number represented as a string</param>
        /// <param name="generatedNumber">Random generated four digit number represented as a string</param>
        /// <param name="bullsCount">Correctly guessed by the user number of Bulls</param>
        /// <param name="cowsCount">Correctly guessed by the user number of Cows</param>
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
