using System;
using System.Linq;
using System.Text;

namespace BullsAndCowsGame
{
    //TODO : Refactor code, that manages the gameplay from original class "bikove_i_kravi"
    public class Engine
    {
        //private const int NumberLenght = 4;

        public void Start()
        {
            PlayerCommand enteredCommand = new PlayerCommand();

            do
            {
                UserInterface.PrintWelcomeMessage();
                Player currentPlayer = new Player("NoName");
                NumberManager.GenerateNumber();
				
                UserInterface.HelpNumber = new StringBuilder("XXXX");
                UserInterface.HelpPattern = null;
                do
                {
                    Console.Write("Enter your guess or command: ");
                    string playerInput = Console.ReadLine();
                    enteredCommand = CommandParser.PlayerInputToPlayerCommand(playerInput);

                    ExecuteCommand(enteredCommand, playerInput, currentPlayer);
                }
                while (enteredCommand != PlayerCommand.Exit && enteredCommand != PlayerCommand.Restart);
                
                Console.WriteLine();
            }
            while (enteredCommand != PlayerCommand.Exit);
            Console.WriteLine("Good bye!");
        }

        private void ExecuteCommand(PlayerCommand enteredCommand, string playerInput, Player player)
        {
            if (enteredCommand == PlayerCommand.Top)
            {
                UserInterface.PrintScoreboard();
            }
            else if (enteredCommand == PlayerCommand.Help)
            {
                player.Cheats = UserInterface.ShowHelp(player.Cheats);
            }
            else
            {
                if (IsValidInput(playerInput))
                {
                    player.Attempts++;
                    int bullsCount;
                    int cowsCount;
                    NumberManager.GetBullsAndCows(playerInput, NumberManager.Number, out bullsCount, out cowsCount);
                    if (bullsCount == NumberManager.NUMBER_LENGHT)
                    {
                        UserInterface.PrintCongratulateMessage(player.Attempts, player.Cheats);
                        UserInterface.FinishGame(player.Attempts, player.Cheats);
                        return;
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

        private bool IsValidInput(string playerInput)
        {
            if (playerInput == String.Empty || playerInput.Length != NumberManager.NUMBER_LENGHT)
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
    }
}
