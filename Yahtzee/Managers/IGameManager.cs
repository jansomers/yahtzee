using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yahtzee.Models;

namespace Yahtzee.Managers
{
    interface IGameManager
    {
        Game MakeGame(User userA, User userB, string gameName);
        Game MakeGame(Game game);
        Game GetGame(int id);
        ICollection<Game> GetGames(string userA);
        ICollection<Game> GetInvitations(string userB);
        Game AcceptGame(int id);
        ICollection<Game> GetActiveGames(string userA);
        void RemoveGame(int game);
        ICollection<Game> GetHistory(string name);
        Game RegisterUpperResult(int key, int score, bool playerA, int id);
        Game RegisterLowerResult(int key, int score, bool playerA, int gameId);
        Game EndGame(int gameId, int scoreA, int scoreB, string winnerId);
        ICollection<Game> GetGamesByUserId(string id);
        int CalculateBestScore(ICollection<Game> games, string id);
        Game PostChat(int gameId, string name, string message);
        int CalculateAvgScore(ICollection<Game> games, string id);
    }
}
