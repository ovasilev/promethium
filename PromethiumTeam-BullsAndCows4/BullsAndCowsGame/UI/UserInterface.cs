//-----------------------------------------------------------------------
// <copyright file="NumberManager.cs" company="TelerikAcademy">
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
        private const string WelcomeMessage = "Welcome to “Bulls and Cows” game. " +
            "Please try to guess my secret 4-digit number." +
            "Use 'top' to view the top scoreboard, 'restart' " +
            "to start a new game and 'help' to cheat and 'exit' to quit the game.";

        /// <summary>
        /// Constant representing the warning message, when something goes wrong
        /// </summary>
        private const string WrongCommandMessage = "Incorrect guess or command!";

        public static StringBuilder HelpNumber;
        public static string HelpPattern;
        //public static string GeneratedNumber;

        private static readonly ScoreBoard<Player> scoreBoard = new ScoreBoard<Player>();
        
        //private void PrintWelcomeMessage()
        public static void PrintWelcomeMessage()
        {
            Console.WriteLine(WelcomeMessage);
        }

        //private void PrintWrongCommandMessage()
        public static void PrintWrongCommandMessage()
        {
            Console.WriteLine(WrongCommandMessage);
        }

        //private void PrintCongratulateMessage(int attempts, int cheats)
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

        public static int ShowHelp(int cheats)
        {
            if (cheats < 4)
            {
                RevealDigit(cheats);
                cheats++;
                Console.WriteLine("The number looks like {0}.", HelpNumber);
            }
            else
            {
                Console.WriteLine("You are not allowed to ask for more help!");
            }
            return cheats;
        }

        public static void FinishGame(int attempts, int cheats)
        {
            if (cheats == 0)
            {
                Console.Write("Please enter your name for the top scoreboard: ");
                string playerName = Console.ReadLine();
                AddPlayerToScoreboard(playerName, attempts);
                PrintScoreboard();
            }
            else
            {
                Console.WriteLine("You are not allowed to enter the top scoreboard.");
            }
        }

        public static void AddPlayerToScoreboard(string playerName, int attempts)
        {
            Player player = new Player(playerName, attempts);
            scoreBoard.Add(player);
        }

        public static void PrintScoreboard()
        {
            if (scoreBoard.Count == 0)
            {
                Console.WriteLine("Top scoreboard is empty.");
            }
            else
            {
                Console.WriteLine("Scoreboard:");
                int i = 1;
                foreach (Player p in scoreBoard)
                {
                    Console.WriteLine("{0}. {1} --> {2} guess" + ((p.Attempts == 1) ? "" : "es"), i++, p.Name, p.Attempts);
                }
            }
        }

        public static void RevealDigit(int cheats)
        {
            if (HelpPattern == null)
            {
                GenerateHelpPattern();
            }
            int digitToReveal = HelpPattern[cheats] - '0';
            HelpNumber[digitToReveal - 1] = NumberManager.Number[digitToReveal - 1];
        }

        public static void GenerateHelpPattern()
        {
            string[] helpPaterns = {"1234", "1243", "1324", "1342", "1432", "1423",
                "2134", "2143", "2314", "2341", "2431", "2413",
                "3214", "3241", "3124", "3142", "3412", "3421",
                "4231", "4213", "4321", "4312", "4132", "4123",};


            Random randomNumberGenerator = new Random(DateTime.Now.Millisecond);
            int randomPaternNumber = randomNumberGenerator.Next(helpPaterns.Length - 1);
            HelpPattern = helpPaterns[randomPaternNumber];
        }

        
    }
}
