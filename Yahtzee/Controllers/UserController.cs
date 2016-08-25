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
        private Context _dbContext;
        private IUserManager _userManager;
        private IGameManager _gameManager;
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        // GET api/<controller>
        public UserController()
        {
            _dbContext = new Context();
            _userManager  = new UserManager(_dbContext);
            _gameManager = new GameManager(_dbContext);
        }
        [HttpGet]
        public User DetailsByEmail(string email)
        {
            logger.Debug("Received request: DetailsByEmail with email: \t" + email);
            return _userManager.GetUserByEmail(email);
        }

        // GET api/<controller>/5
        [HttpGet]
        public User DetailsById(string id)
        {
            logger.Debug("Received request: DetailsById with id: \t" + id);
            return _userManager.GetUserById(id);
        }
        [HttpPut]
        public IHttpActionResult UpdateScreenName(string screenName)
        {
            string userName = HttpContext.Current.User.Identity.Name;
            logger.Debug("Received request: UpdateScreenName with screen name: \t" + screenName);
            _userManager.ChangeScreenName(userName, screenName);
            logger.Debug("Verifying if screen name has indeed been updated");
            if (_userManager.GetUserByEmail(userName).ScreenName == screenName)
            {
                logger.Debug("Screen name updated. Returning Ok()");
                return Ok();
            }

            else
            {
                logger.Debug("Screenname was not updated. Bad Request?");
                return BadRequest();
            }
        }
        [HttpGet]
        public int WonGames()
        {
            return _userManager.GetWonGames(HttpContext.Current.User.Identity.Name);
        }
        [HttpGet]
        public int BestScore()
        {
            var user = _userManager.GetUserByEmail(HttpContext.Current.User.Identity.Name);
            var games = _gameManager.GetGamesByUserId(user.Id);
            var bestScore = _gameManager.CalculateBestScore(games , user.Id);
            return bestScore;
        }
        [HttpGet]
        public int AvgScore()
        {
            var user = _userManager.GetUserByEmail(HttpContext.Current.User.Identity.Name);
            var games = _gameManager.GetGamesByUserId(user.Id);
            var avgScore = _gameManager.CalculateAvgScore(games, user.Id);
            return avgScore;

        }
        [HttpPut]
        public IHttpActionResult UpdateAvatar(string avatar)
        {
            string userName = HttpContext.Current.User.Identity.Name;
            logger.Debug("Received request: UpdateAvatar with avatar: \t" + avatar);
            _userManager.ChangeAvatar(userName, avatar);
            logger.Debug("Verifying if avatar has indeed been updated");
            if (_userManager.GetUserByEmail(userName).Avatar == avatar)
            {
                logger.Debug("Avatar updated. Returning Ok()");
                return Ok();
            }
            else
            {
                logger.Debug("Avatar was not updated.Bad Request?");
                return BadRequest();
            }
                
        }

       

      
       
    }
}