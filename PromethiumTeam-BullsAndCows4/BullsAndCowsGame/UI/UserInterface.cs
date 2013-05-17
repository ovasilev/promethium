//-----------------------------------------------------------------------
// <copyright file="UserInterface.cs" company="TelerikAcademy">
//     All rights reserved © Telerik Academy 2012-2013
// </copyright>
//-----------------------------------------------------------------------

namespace BullsAndCowsGame
{
    using System;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Public Static Class that manages the user interface
    /// </summary>
    //TODO : Refactor code, that manages user comunication from original class "bikove_i_kravi"
    public static class UserInterface
    {
        /// <summary>
        /// Constant representing the welcome message, when starting the game
        /// </summary>
        private const string WELCOME_MESSAGE = "Welcome to “Bulls and Cows” game. " +
            "Please try to guess my secret 4-digit number." +
            "Use 'top' to view the top scoreboard, 'restart' " +
            "to start a new game and 'help' to cheat and 'exit' to quit the game.";

        /// <summary>
        /// Constant representing the warning message, when something goes wrong
        /// </summary>
        private const string WRONG_COMMAND_MESSAGE = "Incorrect guess or command!";

        /// <summary>
        /// Prints welcome message from constant WELCOME_MESSAGE on the console
        /// </summary>        
        public static void PrintWelcomeMessage()
        {
            Console.WriteLine(WELCOME_MESSAGE);
        }

        /// <summary>
        /// Prints wrong command message from constant WRONG_COMMAND_MESSAGE on the console
        /// </summary>        
        public static void PrintWrongCommandMessage()
        {
            Console.WriteLine(WRONG_COMMAND_MESSAGE);
        }

        /// <summary>
        /// Prints greetings message, including summery of the game 
        /// </summary>
        /// <param name="attempts">Number of guess attempts during the game</param>
        /// <param name="attempts">Number of cheats used during the game</param>
        public static void PrintCongratulateMessage(int attempts, int cheats)
        {
            Console.Write("Congratulations! You guessed the secret number in {0} attempts", attempts);
            if (cheats == 0)
            {
                Console.WriteLine(".");
            }
            else
            {
                Console.WriteLine(" and {0} cheats.", cheats);
            }
        }

        /// <summary>
        /// Prints the scoreboard on the console
        /// </summary>
        /// <param name="scoreBoard">Given scoreboard to be printed</param>
        public static void PrintScoreboard(ScoreBoard<Player> scoreBoard)
        {
            if (scoreBoard.Count == 0)
            {
                Console.WriteLine("Top scoreboard is empty.");
            }
            else
            {
                Console.WriteLine("Scoreboard:");
                int i = 1;
                foreach (Player player in scoreBoard)
                {
                    Console.WriteLine("{0}. {1} --> {2} guess" + ((player.Attempts == 1) ? "" : "es"), i++, player.Name, player.Attempts);
                }
            }
        }

        public static void ShowHelp(string helpNumber)
        {
            Console.WriteLine("The number looks like {0}.", helpNumber);
        }

        public static void PrintCheatsLimitReached()
        {

            Console.WriteLine("You are not allowed to ask for more help!");
        }
    }
}
