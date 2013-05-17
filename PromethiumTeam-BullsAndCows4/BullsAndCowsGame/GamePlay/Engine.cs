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
        #region Constructors

        public Engine()
        {
            this.ScoreBoard = new ScoreBoard<Player>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Number to be guessed in the game
        /// </summary>
        private GameNumber Number { get; set; }

        /// <summary>
        /// Player profile
        /// </summary>
        private Player Player { get; set; }

        /// <summary>
        /// Keeps highest scores of the game
        /// </summary>
        private ScoreBoard<Player> ScoreBoard { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// This is the method that each new game strats with. It calls several
        /// other helper methods.
        /// </summary>
        public void StartGame()
        {
            PlayerCommand enteredCommand = new PlayerCommand();

            do
            {
                UserInterface.ShowWelcomeGreeting();
                this.Player = new Player("NoName");
                this.Number = new GameNumber();

                do
                {
                    string playerInput = UserInterface.GetPlayerCommand(); 
                    enteredCommand = CommandParser.PlayerInputToPlayerCommand(playerInput);
                    ExecuteCommand(enteredCommand, playerInput);
                }
                while (enteredCommand != PlayerCommand.Exit && enteredCommand != PlayerCommand.Restart);

                UserInterface.EndOfGameMessage();
            }
            while (enteredCommand != PlayerCommand.Exit);

            UserInterface.ShowFairwell();
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
                UserInterface.ShowScoreboard(this.ScoreBoard.ToString());
            }
            else if (enteredCommand == PlayerCommand.Help)
            {
                if (this.Number.RevealDigit(this.Player.Cheats))
                {
                    UserInterface.ShowHelp(this.Number.HelpNumber.ToString());
                    this.Player.Cheats++;
                }
                else 
                {
                    UserInterface.ShowCheatsLimitReached();
                }
            }
            else
            {
                if (IsValidInput(playerInput))
                {
                    this.Player.Attempts++;
                    int bullsCount;
                    int cowsCount;
                    this.Number.GetBullsAndCows(playerInput, this.Number.Digits, out bullsCount, out cowsCount);
                    if (bullsCount == GameNumber.LENGHT)
                    {
                        UserInterface.ShowCongratulations(this.Player.Attempts, this.Player.Cheats);
                        this.FinishGame();
                        return;
                    }
                    else
                    {
                        UserInterface.ShowGuessStatistics(bullsCount, cowsCount);
                    }
                }
                else
                {
                    if (enteredCommand != PlayerCommand.Restart && enteredCommand != PlayerCommand.Exit)
                    {
                        UserInterface.ShowWrongCommand();
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
            if (playerInput == String.Empty || playerInput.Length != GameNumber.LENGHT)
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

        /// <summary>
        /// Adds the player to the scoreboard at the end of the game, if hasn't used cheats
        /// </summary>
        public void AddPlayerToScoreboard()
        {
            if (this.Player.Cheats == 0)
            {
                string playerName = UserInterface.GetPlayerName();
                this.ScoreBoard.Add(Player);
            }
            else
            {
                UserInterface.ShowCheatersMessage();
            }
        }

        /// <summary>
        /// Takes care of scoreboard and user comunication at end of each game
        /// </summary>
        public void FinishGame()
        {
            AddPlayerToScoreboard();

            if (this.Player.Cheats == 0)
            {
                UserInterface.ShowScoreboard(this.ScoreBoard.ToString());
            }
        }

        #endregion
    }
}
