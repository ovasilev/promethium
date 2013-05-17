using System;
using BullsAndCowsGame.GameObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BullsAndCowsGameTests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class ScoreBoardTests
    {
        [TestMethod]
        public void ScoreBoard_ToString_EmptyScoreBoard()
        {
            ScoreBoard<Player> scoreBoard = new ScoreBoard<Player>();

            var actual = scoreBoard.ToString();
            var expected = "Top scoreboard is empty.";

            Assert.AreEqual(expected, actual);
        }
    }
}
