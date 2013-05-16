//-----------------------------------------------------------------------
// <copyright file="Engine.cs" company="TelerikAcademy">
//     All rights reserved © Telerik Academy 2012-2013
// </copyright>
//----------------------------------------------------------------------

using System;
using System.Text;

namespace BullsAndCowsGame
{
    /// <summary>
    /// Class Engine stores the main functionality of the game
    /// </summary>
    public class Engine
    {
        /// <summary>
        /// This is the method that each new game strats with. It calls several
        /// other helper methods.
        /// </summary>
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

        /// <summary>
        /// Method handles entered by the player command <paramref name="enteredCommand"/>, 
        /// calling other methods each responsible for different interactions.
        /// </summary>
        /// <param name="enteredCommand">Options that the game offers to a player</param>
        /// <param name="playerInput">Command that player enters</param>
        /// <param name="player">The user that's playing at the moment</param>
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

        /// <summary>
        /// The method checkes if the player's input <paramref name="playerInput"/>
        /// is valid. It checks if the input is empty string, if the input lenght is
        /// correct and if the input contains digits only.
        /// </summary>
        /// <param name="playerInput">Player's input</param>
        /// <returns>Returns <see cref="System.Boolean"/> true or false
        /// according to if the input is valid or not.</returns>
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
