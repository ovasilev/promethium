using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BullsAndCowsGame
{
    //TODO : Refactor code, that manages the gameplay from original class "bikove_i_kravi"
    public class Engine
    {
        private const int NUMBER_LENGHT = 4;

        public void Start()
        {
            PlayerCommand enteredCommand;
            do
            {
                UserInterface.PrintWelcomeMessage();
                generateNumber();
                int attempts = 0;
                int cheats = 0;
				
                UserInterface.helpNumber = new StringBuilder("XXXX");
                UserInterface.helpPattern = null;
                do
                {
                    Console.Write("Enter your guess or command: ");
                    string playerInput = Console.ReadLine();
                    enteredCommand = CommandParser.PlayerInputToPlayerCommand(playerInput);

                    if (enteredCommand == PlayerCommand.Top)
                    {
                        UserInterface.PrintScoreboard();
                    }
                    else if (enteredCommand == PlayerCommand.Help)
                    {
                        cheats = UserInterface.PokajiHelp(cheats);
                    }
                    else
                    {
                        if (IsValidInput(playerInput))
                        {
                            attempts++;
                            int bullsCount;
                            int cowsCount;
                            CalculateBullsAndCowsCount(playerInput, UserInterface.generatedNumber, out bullsCount, out cowsCount);
                            if (bullsCount == NUMBER_LENGHT)
                            {
                                UserInterface.PrintCongratulateMessage(attempts, cheats);
                                UserInterface.FinishGame(attempts, cheats);
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Wrong number! Bulls: {0}, Cows: {1}", bullsCount, cowsCount);
                            }
                        }
                        else
                        {
                            if (enteredCommand != PlayerCommand.Restart && enteredCommand != PlayerCommand.Exit)
                            {
                                UserInterface.PrintWrongCommandMessage();
                            }
                        }
                    }
                }
                while (enteredCommand != PlayerCommand.Exit && enteredCommand != PlayerCommand.Restart);
                Console.WriteLine();
            }
            while (enteredCommand != PlayerCommand.Exit);
            Console.WriteLine("Good bye!");
        }

        private void CalculateBullsAndCowsCount(string playerInput, string generatedNumber, out int bullsCount, out int cowsCount)
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

        private bool IsValidInput(string playerInput)
        {
            if (playerInput == String.Empty || playerInput.Length != NUMBER_LENGHT)
            {
                return false;
            }
            for (int i = 0; i < playerInput.Length; i++)
            {
                char currentChar = playerInput[i];
                if (!Char.IsDigit(currentChar))
                {
                    return false;
                }
            }
            return true;
        }

        public static void generateNumber()
        {
            StringBuilder num = new StringBuilder(4);
            Random randomNumberGenerator = new Random(DateTime.Now.Millisecond);

            for (int i = 0; i < NUMBER_LENGHT; i++)
            {
                int randomDigit = randomNumberGenerator.Next(9);
                num.Append(randomDigit);
            }

            UserInterface.generatedNumber = num.ToString();
        }
    }
}
