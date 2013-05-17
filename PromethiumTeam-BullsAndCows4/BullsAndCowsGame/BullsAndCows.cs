using System;
using System.Linq;
using BullsAndCowsGame.GamePlay;

namespace BullsAndCowsGame
{
    class BullsAndCows
    {
        static void Main(string[] args)
        {
            Engine game = new Engine();
            game.StartGame();
        }
    }
}
