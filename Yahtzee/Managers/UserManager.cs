using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Yahtzee.DAL;
using Yahtzee.DAL.Repositories;
using Yahtzee.Models;

namespace Yahtzee.Managers
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository _userRepository;
        public UserManager(Context context)
        {
            
            _userRepository = new UserRepository(context);
            
        }

        public IQueryable<User> GetUsers()
        {
            return _userRepository.ReadUsers();
        }

        public User GetUserByEmail(string email)
        {
            return _userRepository.ReadUserByEmail(email);
        }

        public User GetUserById(string id)
        {
            return _userRepository.ReadUserById(id);
        }

        public void ChangeScreenName(string userName, string screenName)
        {
            _userRepository.UpdateScreenName(userName, screenName);
        }

        public void ChangeAvatar(string userName, string avatar)
        {
            _userRepository.UpdateAvatar(userName, avatar);
        }

        public void AddFriend(User user, User friend)
        {
            _userRepository.AddFriend(user, friend);
        }

        public int GetWonGames(string name)
        {
            return _userRepository.ReadWonGames(name);
        }

        
    }
}