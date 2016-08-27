using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebGrease.Css.Extensions;
using Yahtzee.DAL;
using Yahtzee.Managers;
using Yahtzee.Models;

namespace Yahtzee.Controllers
{
    public class UserController : ApiController
    {
        private IUserManager _userManager;
        private IGameManager _gameManager;
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        // GET api/<controller>
        public UserController()
        {
            var dbContext = new Context();
            _userManager  = new UserManager(dbContext);
            _gameManager = new GameManager(dbContext);
        }
        [Authorize]
        [HttpGet]
        public User DetailsByEmail(string email)
        {
            Logger.Debug("Received request: DetailsByEmail with email: \t" + email);
            return _userManager.GetUserByEmail(email);
        }

        [Authorize]
        [HttpGet]
        public User DetailsById(string id)
        {
            Logger.Debug("Received request: DetailsById with id: \t" + id);
            return _userManager.GetUserById(id);
        }
        [Authorize]
        [HttpPut]
        public IHttpActionResult UpdateScreenName(string screenName)
        {
            string userName = HttpContext.Current.User.Identity.Name;
            Logger.Debug("Received request: UpdateScreenName with screen name: \t" + screenName);
            _userManager.ChangeScreenName(userName, screenName);
            Logger.Debug("Verifying if screen name has indeed been updated");
            if (_userManager.GetUserByEmail(userName).ScreenName == screenName)
            {
                Logger.Debug("Screen name updated. Returning Ok()");
                return Ok();
            }

            else
            {
                Logger.Debug("Screenname was not updated. Bad Request?");
                return BadRequest();
            }
        }
        
        [HttpGet]
        public int WonGames()
        {
            Logger.Debug("Received WonGames request for user: " + HttpContext.Current.User.Identity.Name);
            return _userManager.GetWonGames(HttpContext.Current.User.Identity.Name);
        }
        [HttpGet]
        public int BestScore()
        {
            Logger.Debug("Received BestScore request for user: " + HttpContext.Current.User.Identity.Name);
            var user = _userManager.GetUserByEmail(HttpContext.Current.User.Identity.Name);
            var games = _gameManager.GetGamesByUserId(user.Id);
            var bestScore = _gameManager.CalculateBestScore(games , user.Id);
            return bestScore;
        }
        [HttpGet]
        public int AvgScore()
        {
            Logger.Debug("Received WonGames request for user: " + HttpContext.Current.User.Identity.Name);
            var user = _userManager.GetUserByEmail(HttpContext.Current.User.Identity.Name);
            var games = _gameManager.GetGamesByUserId(user.Id);
            var avgScore = _gameManager.CalculateAvgScore(games, user.Id);
            return avgScore;

        }
        [HttpPut]
        public IHttpActionResult UpdateAvatar(string avatar)
        {
            string userName = HttpContext.Current.User.Identity.Name;
            Logger.Debug("Received request: UpdateAvatar with avatar: \t" + avatar);
            _userManager.ChangeAvatar(userName, avatar);
            Logger.Debug("Verifying if avatar has indeed been updated");
            if (_userManager.GetUserByEmail(userName).Avatar == avatar)
            {
                Logger.Debug("Avatar updated. Returning Ok()");
                return Ok();
            }
            else
            {
                Logger.Debug("Avatar was not updated.Bad Request?");
                return BadRequest();
            }
                
        }

       

      
       
    }
}