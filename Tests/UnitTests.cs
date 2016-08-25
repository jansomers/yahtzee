using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yahtzee.DAL;
using Yahtzee.Managers;
using Yahtzee.Models;


namespace Tests
{
    [TestClass]
    public class UnitTests
    {
        private ICollection<Game> _testGames;
        private GameManager _gameManager;

        [TestInitialize]
        public void InitTests()
        {
            _testGames = new List<Game>();
            _gameManager = new GameManager(new Context());
            InitializeTestGames(_testGames);

        }

        private void InitializeTestGames(ICollection<Game> collection)
        {
            Game game1 = new Game
            {
                ScoreA = 0,
                ScoreB = 0,
                PlayerAId = 1.ToString(),
                PlayerBId = 2.ToString()
            };

            Game game2 = new Game
            {
                ScoreA = 200,
                ScoreB = 200,
                PlayerAId = 1.ToString(),
                PlayerBId = 2.ToString()
            };

            Game game3 = new Game
            {
                ScoreA = 400,
                ScoreB = 400,
                PlayerAId = 1.ToString(),
                PlayerBId = 2.ToString()
            };

            _testGames.Add(game1);
            _testGames.Add(game2);
            _testGames.Add(game3);

        }

        [TestMethod]
        public void Average()
        {


            var averageResult = _gameManager.CalculateAvgScore(_testGames, 1.ToString());
            var expectedResult = 200;
            Assert.AreEqual(averageResult, expectedResult);


        }

        [TestMethod]
        public void Best()
        {
            var bestResult = _gameManager.CalculateBestScore(_testGames, 2.ToString());
            var expectedResult = 400;
            Assert.AreEqual(bestResult, expectedResult);
        }
    }
}
