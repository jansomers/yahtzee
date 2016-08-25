using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yahtzee.Models;

namespace Yahtzee.DAL.Repositories
{
    interface IUserRepository
    {
        IQueryable<User> ReadUsers();
        User ReadUserByEmail(string email);
        User UpdateGames(User playerA, Game game);
        User UpdateInvitations(User playerB, Game game);
        User ReadUserById(string id);
        void UpdateScreenName(string userName, string screenName);
        void UpdateAvatar(string userName, string avatar);
        void UpdateUser(User user);
        void AddFriend(User user, User friend);
        int ReadWonGames(string name);
        
    }
}
