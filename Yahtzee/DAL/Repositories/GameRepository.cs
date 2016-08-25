using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Yahtzee.Models;

namespace Yahtzee.DAL.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly Context _dbContext;

        public GameRepository(Context context)
        {
            _dbContext = context;

        }
        public Game ReadGame(int id)
        {

            return _dbContext.Games.Include(gr => gr.GameResultA)
                .Include(gr => gr.GameResultB)
                .FirstOrDefault(g => g.Id == id);
        }

        public ICollection<Game> ReadGames(string userA)
        {
            User user = _dbContext.Users.FirstOrDefault(u => u.UserName == userA);

            if (user != null)
            {
                return user.Games;
            }
            else
            {
                return new Collection<Game>();
            }
        }

        public ICollection<Game> ReadInvitations(string userB)
        {

            var status = new GameStatus[] { GameStatus.Making, GameStatus.Created };

            ICollection<Game> invitations = new List<Game>();

            
                invitations = _dbContext.Games.Include(gr => gr.GameResultA)
                        .Include(gr => gr.GameResultB)
                        .Where(g => g.PlayerB.UserName == userB && status.Contains(g.Status)).ToList();

            

            return invitations;

        }

        public Game UpdateStatus(int id, GameStatus status)
        {
            Game result = this.ReadGame(id);
            if (result != null)
            {
                result.Status = status;
                _dbContext.SaveChanges();

            }
            return result;

        }

        public ICollection<Game> ReadActiveGames(string userA)
        {

            var status = new GameStatus[] { GameStatus.AIsPlaying, GameStatus.BIsPlaying };
            ICollection<Game> games = new List<Game>();
            
                games = _dbContext.Games.Include(gr => gr.GameResultA)
                         .Include(gr => gr.GameResultB).Where(g => (g.PlayerA.UserName == userA || g.PlayerB.UserName == userA) && status.Contains(g.Status)).ToList();

            return games;
        }

        public void DeleteGame(int game)
        {
            Game gameToDelete = _dbContext.Games.FirstOrDefault(g => g.Id == game);
            if (gameToDelete != null)
            {
                _dbContext.Games.Remove(gameToDelete);
                _dbContext.SaveChanges();
            }
        }

        public ICollection<Game> ReadHistory(string name)
        {

            var status = new GameStatus[] { GameStatus.Ended, GameStatus.Finished };
            ICollection<Game> history = new List<Game>();
            
                history =
                    _dbContext.Games.Where(
                        g => (g.PlayerA.UserName == name || g.PlayerB.UserName == name) && status.Contains(g.Status))
                        .ToList();
            


            return history;
        }

        public Game UpdateUpperHalf(int key, int score, bool playerA, int id)
        {
            Game game = this.ReadGame(id);
            game.Status = playerA ? GameStatus.BIsPlaying : GameStatus.AIsPlaying;

            GameResult gameResult = playerA ? game.GameResultA : game.GameResultB;

            switch (key)
            {
                case 1:
                    gameResult.Ones = gameResult.Ones > 0 ? gameResult.Ones : score;
                    break;
                case 2:
                    gameResult.Twos = gameResult.Twos > 0 ? gameResult.Twos : score;
                    break;
                case 3:
                    gameResult.Threes = gameResult.Threes > 0 ? gameResult.Threes : score;
                    break;
                case 4:
                    gameResult.Fours = gameResult.Fours > 0 ? gameResult.Fours : score;
                    break;
                case 5:
                    gameResult.Fives = gameResult.Fives > 0 ? gameResult.Fours : score;
                    break;
                case 6:
                    gameResult.Sixes = gameResult.Sixes > 0 ? gameResult.Fives : score;
                    break;

            }

            if (playerA)
            {
                game.GameResultA = gameResult;
            }
            else
            {
                game.GameResultB = gameResult;
            }
            _dbContext.SaveChanges();

            return game;
        }

        public Game UpdateLowerHalf(int key, int score, bool playerA, int id)
        {
            Game game = this.ReadGame(id);
            game.Status = playerA ? GameStatus.BIsPlaying : GameStatus.AIsPlaying;

            GameResult gameResult = playerA ? game.GameResultA : game.GameResultB;

            switch (key)
            {
                case 1:
                    gameResult.Tok = gameResult.Tok > 0 ? gameResult.Tok : score;
                    break;
                case 2:
                    gameResult.Fok = gameResult.Fok > 0 ? gameResult.Fok : score;
                    break;
                case 3:
                    gameResult.Fh = gameResult.Fh > 0 ? gameResult.Fh : score;
                    break;
                case 4:
                    gameResult.SmStr = gameResult.SmStr > 0 ? gameResult.SmStr : score;
                    break;
                case 5:
                    gameResult.LaStr = gameResult.LaStr > 0 ? gameResult.LaStr : score;
                    break;
                case 6:
                    gameResult.Yahtzee = gameResult.Yahtzee > 0 ? gameResult.Yahtzee : score;
                    break;
                case 7:
                    gameResult.Chance = gameResult.Chance > 0 ? gameResult.Chance : score;
                    break;

            }

            if (playerA)
            {
                game.GameResultA = gameResult;
            }
            else
            {
                game.GameResultB = gameResult;
            }
            _dbContext.SaveChanges();

            return game;
        }

        public Game UpdateEndGame(int gameId, int scoreA, int scoreB, string winnerId)
        {
            Game game = this.ReadGame(gameId);
            game.ScoreA = scoreA;
            game.ScoreB = scoreB;
            game.WinnerId = winnerId;
            game.Status = GameStatus.Ended;
            _dbContext.SaveChanges();

            return game;
            
        }

        public ICollection<Game> ReadGamesByUserId(string id)
        {
            return _dbContext.Games.Where(g => g.PlayerAId == id || g.PlayerBId == id).ToList();
        }

        public Game CreateChat(int gameId, ChatMessage message)
        {
            Game game = ReadGame(gameId);

            if (game != null)
            {
                game.ChatMessages.Add(message);
                _dbContext.SaveChanges();
            }

            return game;

        }


        public Game CreateGame(Game game)
        {
            _dbContext.Games.Add(game);
            _dbContext.SaveChanges();

            var gameResultA = new GameResult();
            gameResultA.Player = 'A';
            gameResultA.GameId = game.Id;
            var gameResultB = new GameResult();
            gameResultA.Player = 'A';
            gameResultB.GameId = game.Id;
            game.GameResultA = gameResultA;
            game.GameResultB = gameResultB;
            game.Status = GameStatus.Created;
            _dbContext.GameResults.Add(gameResultA);
            _dbContext.GameResults.Add(gameResultB);
            _dbContext.SaveChanges();

            return game;


        }


    }
}