//-----------------------------------------------------------------------
// <copyright file="Engine.cs" company="TelerikAcademy">
//     All rights reserved © Telerik Academy 2012-2013
// </copyright>
//----------------------------------------------------------------------

namespace BullsAndCowsGame.GamePlay
{
    using System;
    using System.Text;
    using BullsAndCowsGame.GameObjects;
    using BullsAndCowsGame.UI;
    using BullsAndCowsGame.Intefaces;

    /// <summary>
    /// Class Engine stores the main functionality of the game
    /// </summary>
    public class Engine
    {
        private Number number;
        private Player player;
        private ScoreBoard<Player> scoreBoard;

        public Engine()
        {
            this.scoreBoard = new ScoreBoard<Player>();
            this.player = new Player("NoName");
            this.number = new Number();
        }
        
        /// <summary>
        /// This is the method that each new game strats with. It calls several
        /// other helper methods.
        /// </summary>
        public void StartGame()
        {
            PlayerCommand enteredCommand = new PlayerCommand();

            do
            {
                UserInterface.PrintWelcomeMessage();

                do
                {
                    Console.Write("Enter your guess or command: ");
                    string playerInput = Console.ReadLine();
                    enteredCommand = CommandParser.PlayerInputToPlayerCommand(playerInput);

                    ExecuteCommand(enteredCommand, playerInput);
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
        private void ExecuteCommand(PlayerCommand enteredCommand, string playerInput)
        {
            if (enteredCommand == PlayerCommand.Top)
            {
                UserInterface.PrintScoreboard(this.scoreBoard as IScoreBoard<IPlayer>);
            }
            else if (enteredCommand == PlayerCommand.Help)
            {
                if (number.RevealDigit(player.Cheats))
                {
                    UserInterface.ShowHelp(number.HelpNumber.ToString());
                    player.Cheats++;
                }
                else 
                {
                    UserInterface.PrintCheatsLimitReached();
                }
            }
            else
            {
                if (IsValidInput(playerInput))
                {
                    this.player.Attempts++;
                    int bullsCount;
                    int cowsCount;
                    number.GetBullsAndCows(playerInput, number.Digits, out bullsCount, out cowsCount);
                    if (bullsCount == Number.LENGHT)
                    {
                        UserInterface.PrintCongratulateMessage(player.Attempts, player.Cheats);
                        this.FinishGame(player.Attempts, player.Cheats);
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
            if (playerInput == String.Empty || playerInput.Length != Number.LENGHT)
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

        public void AddPlayerToScoreboard(string playerName, int attempts)
        {
            Player player = new Player(playerName, attempts);
           this.scoreBoard.Add(player);
        }

        public void FinishGame(int attempts, int cheats)
        {
            if (cheats == 0)
            {
                Console.Write("Please enter your name for the top scoreboard: ");
                string playerName = Console.ReadLine();
                AddPlayerToScoreboard(playerName, attempts);
                UserInterface.PrintScoreboard(this.scoreBoard as IScoreBoard<IPlayer>);
            }
            else
            {
                Console.WriteLine("You are not allowed to enter the top scoreboard.");
            }
        }
    }
}
