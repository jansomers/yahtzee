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
        private IGameManager _gameManager;
        private IUserManager _userManager;
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public GameController()
        {
            var dbContext = new Context();
            _gameManager = new GameManager(dbContext);
            _userManager = new UserManager(dbContext);

        }

        [HttpGet]
        public Game GetGame(int id)
        {
            return _gameManager.GetGame(id);
        }
        [HttpGet]
        public ICollection<Game> ActiveGames()
        {
            Logger.Debug("Received request: ActiveGames for user " + HttpContext.Current.User.Identity.Name);
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
            Logger.Debug("Received request: Invitations for user " + HttpContext.Current.User.Identity.Name);
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
            Logger.Debug("Received request: History for user " + HttpContext.Current.User.Identity.Name);
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
            Logger.Debug("Received request: MakeGame for user " + HttpContext.Current.User.Identity.Name);
            User userA = _userManager.GetUserByEmail(HttpContext.Current.User.Identity.Name);
            User userB = _userManager.GetUserByEmail(emailB);

            
            _userManager.AddFriend(userA, userB);

            return _gameManager.MakeGame(userA, userB, gameName);

        }
        
        [HttpPost]
        public Game PostChat(int gameId, string name, string message)
        {
            Logger.Debug("Received request: Post for user " + HttpContext.Current.User.Identity.Name + " for game: " + gameId + " with message: " + message);
            return _gameManager.PostChat(gameId, name, message);

        }

        [Authorize]
        [HttpPut]
        public IHttpActionResult AcceptGame(int game)
        {
            Logger.Debug("Received request: AcceptGame for game " + game);

            var updatedGame = _gameManager.AcceptGame(game);
            if (updatedGame.Status != GameStatus.AIsPlaying)
            {
                return BadRequest();
            }
            return Ok();
        }
        [Authorize]
        [HttpPut]
        public Game PutUpperResult(int key, int score, bool playerA, int gameId)
        {
            Logger.Debug("Received request: PutUpperResult for game " + gameId);
            Game updatedGame = _gameManager.RegisterUpperResult(key, score, playerA, gameId);
            
            return updatedGame;
        }
        [Authorize]
        [HttpPut]
        public Game PutLowerResult(int key, int score, bool playerA, int gameId)
        {
            Logger.Debug("Received request: PutLowerResult for game " + gameId);
            Game updatedGame = _gameManager.RegisterLowerResult(key, score, playerA, gameId);

            return updatedGame;
        }
        [Authorize]
        [HttpPut]
        public Game EndGame(int gameId, int scoreA, int scoreB, string winnerId)
        {
            Logger.Debug("Received request: EndGame for game " + gameId);
            Game endedGame = _gameManager.EndGame(gameId, scoreA, scoreB, winnerId);

            return endedGame;
        }
        [Authorize]
        [HttpDelete]
        public IHttpActionResult RemoveGame(int game)
        {
            Logger.Debug("Received request: PutUpperResult for game " + game);
            _gameManager.RemoveGame(game);

            if (_gameManager.GetGame(game) == null)
            {
                return Ok("game deleted");
            }
                return Content(HttpStatusCode.InternalServerError, "Something went wrong deleting the game");
            
        }
    }
}