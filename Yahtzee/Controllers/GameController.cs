using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Yahtzee.DAL;
using Yahtzee.Managers;
using Yahtzee.Models;
using System.Web;
using Microsoft.AspNet.SignalR.Client;

namespace Yahtzee.Controllers
{
    // api/Game/method
    public class GameController : ApiController
    {
        // Made public for integration testing
        private Context _dbContext;
        private IGameManager _gameManager;
        private IUserManager _userManager;
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public GameController()
        {
            _dbContext = new Context();
            _gameManager = new GameManager(_dbContext);
            _userManager = new UserManager(_dbContext);

        }
        [HttpGet]
        public ICollection<Game> ActiveGames()
        {
            logger.Debug("Loading Active games");
            ICollection<Game> myGames = new List<Game>();

            if (HttpContext.Current.User.Identity.Name != null)
            {
               myGames = _gameManager.GetActiveGames(HttpContext.Current.User.Identity.Name);
            }

            return myGames;

        }

        [HttpGet]
        public ICollection<Game> Invitations()
        {
            logger.Debug("Loading Invitations");
            ICollection<Game> myInvitations = new List<Game>();

            if (HttpContext.Current.User.Identity.Name != null)
            {
                myInvitations = _gameManager.GetInvitations(HttpContext.Current.User.Identity.Name);
            }

            return myInvitations;

        }

        [HttpGet]
        public ICollection<Game> History()
        {
            logger.Debug("Loading History");
            ICollection < Game > myHistory = new List<Game>();

            if (HttpContext.Current.User.Identity.Name != null)
            {
                myHistory = _gameManager.GetHistory(HttpContext.Current.User.Identity.Name);

            }
            
            return myHistory;
        }


        // POST api/<controller>
        [HttpPost]
        public Game MakeGame(string emailB, string gameName)
        {
            logger.Debug("Making new game");
            User userA = _userManager.GetUserByEmail(HttpContext.Current.User.Identity.Name);
            User userB = _userManager.GetUserByEmail(emailB);

            _userManager.AddFriend(userA, userB);

            return _gameManager.MakeGame(userA, userB, gameName);

        }

        [HttpPost]
        public Game PostChat(int gameId, string name, string message)
        {
            return _gameManager.PostChat(gameId, name, message);

        }


        [HttpPut]
        public IHttpActionResult AcceptGame(int game)
        {


            var updatedGame = _gameManager.AcceptGame(game);

            if (updatedGame.Status != GameStatus.AIsPlaying)
            {
                return BadRequest();
            }


            return Ok();
        }

        [HttpPut]
        public Game PutUpperResult(int key, int score, bool playerA, int gameId)
        {
            Game updatedGame = _gameManager.RegisterUpperResult(key, score, playerA, gameId);
            //afzonderen

            return updatedGame;
        }

        [HttpPut]
        public Game PutLowerResult(int key, int score, bool playerA, int gameId)
        {
            Game updatedGame = _gameManager.RegisterLowerResult(key, score, playerA, gameId);

            return updatedGame;
        }

        [HttpPut]
        public Game EndGame(int gameId, int scoreA, int scoreB, string winnerId)
        {
            Game endedGame = _gameManager.EndGame(gameId, scoreA, scoreB, winnerId);

            return endedGame;
        }

        [HttpDelete]
        public IHttpActionResult RemoveGame(int game)
        {
            _gameManager.RemoveGame(game);

            if (_gameManager.GetGame(game) == null)
            {
                return Ok("game deleted");
            }

            else
            {
                return Content(HttpStatusCode.InternalServerError, "Something went wrong deleting the game");
            }
        }
    }
}