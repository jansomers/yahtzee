using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yahtzee.Controllers;
using Yahtzee.DAL;
using Yahtzee.Managers;
using Yahtzee.Models;

namespace Tests
{
    [TestClass]
    public class GameControllerTests
    {
        private GameController _gameController;
        private GameManager _gameManager;
        private Context _context;
        private Game _testGame;

        [TestInitialize]
        public void Init()
        {
            _gameController = new GameController();
            _context = new Context();
            _gameManager = new GameManager(_context);
            _testGame = new Game();
            PushGameToContext(_testGame);
        }



        [TestMethod]
        public void PostChat()
        {
            ChatMessage chatMessage = new ChatMessage
            {
                GameId = _testGame.Id,
                Message = "TestMessage",
                ScreenName = "TestUser"
            };

            var gameWithChat = _gameController.PostChat(_testGame.Id, "TestUser", "TestMessage");
            _context.SaveChanges();

            var expectedChat = gameWithChat.ChatMessages.ToList();
            var expectedMessage = expectedChat.First();

            Assert.AreEqual(expectedMessage.Message, chatMessage.Message);
            Assert.AreEqual(expectedMessage.ScreenName, chatMessage.ScreenName);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            DeleteGameFromContext(_testGame);
        }



        private void PushGameToContext(Game testGame)
        {
            DateTime date = DateTime.Now;
            testGame.GameName = "GameTestGame";
            testGame.TimeStamp = date;
            // Not for deployment, only for testing (als dit gedeployed zou worden zou ik zeker testusers aanmaken)
            testGame.PlayerAId = _context.Users.First().Id;
            testGame.PlayerBId = _context.Users.First().Id;
            testGame.ChatMessages = new List<ChatMessage>();
            _context.Games.Add(testGame);
            _context.SaveChanges();
        }
        private void DeleteGameFromContext(Game testGame)
        {
            _gameManager.RemoveGame(testGame.Id);
        }
    }
}
