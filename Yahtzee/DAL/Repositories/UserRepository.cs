using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Yahtzee.Models;

namespace Yahtzee.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly Context _context;

        public UserRepository(Context context)
        {
            _context = context;
        }

        public IQueryable<User> ReadUsers()
        {
            var users = _context.Users;
            return users;
        }
        public User ReadUserById(string id)
        {
            var user = _context.Users.Include(u => u.Friends).FirstOrDefault(u => u.Id == id);
            return user;
        }

        public void UpdateScreenName(string userName, string screenName)
        {
            var user = this.ReadUserByEmail(userName);
            if (user != null)
            {
                user.ScreenName = screenName;
                _context.SaveChanges();
            }
        }

        public void UpdateAvatar(string userName, string avatar)
        {
            var user = this.ReadUserByEmail(userName);
            if (user != null)
            {
                user.Avatar = avatar;
                _context.SaveChanges();
            }
        }

        public void UpdateUser(User user)
        {
            var oldUser = this.ReadUserById(user.Id);
            oldUser.Friends = user.Friends;
            _context.SaveChanges();
        }

        public void AddFriend(User userToUpdate, User friend)
        {
            var user = ReadUserById(userToUpdate.Id);
            user.Friends.Add(friend);
            _context.SaveChanges();
        }

        public int ReadWonGames(string name)
        {
            var user = ReadUserByEmail(name);
            return _context.Games.Count(g => g.WinnerId == user.Id);
        }

        public User ReadUserByEmail(string email)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
           
            return user;
        }

        public User UpdateGames(User player, Game game)
        {
            player.Games.Add(game);
            _context.SaveChanges();

            return player;
        }

        public User UpdateInvitations(User player, Game game)
        {
            player.Invitations.Add(game);
            _context.SaveChanges();
            return player;
        }

        
    }
}