using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yahtzee.Models;

namespace Yahtzee.Managers
{
    interface IUserManager
    {
        IQueryable<User> GetUsers();
        User GetUserByEmail(string email);
        User GetUserById(string id);
        void ChangeScreenName(string userName, string screenName);
        void ChangeAvatar(string userName, string avatar);
        void AddFriend(User user, User friend);
        int GetWonGames(string name);
        
    }
}
