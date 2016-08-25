using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yahtzee.Models;

namespace Yahtzee.DAL.Repositories
{
    interface IGameRepository
    {
     
        Game CreateGame(Game game);
        Game ReadGame(int id);
        ICollection<Game> ReadGames(string userA);
        ICollection<Game> ReadInvitations(string userB);
        Game UpdateStatus(int id, GameStatus status);
        ICollection<Game> ReadActiveGames(string userA);
        void DeleteGame(int game);
        ICollection<Game> ReadHistory(string name);
        Game UpdateUpperHalf(int key, int score, bool playerA, int id);
        Game UpdateLowerHalf(int key, int score, bool playerA, int id);
        Game UpdateEndGame(int gameId, int scoreA, int scoreB, string winnerId);
        ICollection<Game> ReadGamesByUserId(string id);
        Game CreateChat(int gameId, ChatMessage message);
    }
}
