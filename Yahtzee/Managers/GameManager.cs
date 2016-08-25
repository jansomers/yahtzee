using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebGrease.Css.Extensions;
using Yahtzee.DAL;
using Yahtzee.DAL.Repositories;
using Yahtzee.Models;

namespace Yahtzee.Managers
{
    public class GameManager : IGameManager
    {
        private readonly IGameRepository _gameRepository;

        public GameManager(Context context)
        {

            _gameRepository = new GameRepository(context);

        }

        public Game GetGame(int id)
        {
            return _gameRepository.ReadGame(id);
        }

        public ICollection<Game> GetGames(string userA)
        {
            return _gameRepository.ReadGames(userA);
        }

        public ICollection<Game> GetInvitations(string userB)
        {
            return _gameRepository.ReadInvitations(userB);
        }

        public Game AcceptGame(int id)
        {
            return _gameRepository.UpdateStatus(id, GameStatus.AIsPlaying);
        }

        public ICollection<Game> GetActiveGames(string userA)
        {
            return _gameRepository.ReadActiveGames(userA);
        }

        public void RemoveGame(int game)
        {
            _gameRepository.DeleteGame(game);
        }

        public ICollection<Game> GetHistory(string name)
        {
            return _gameRepository.ReadHistory(name);
        }

        public Game RegisterUpperResult(int key, int score, bool playerA, int id)
        {
            return _gameRepository.UpdateUpperHalf(key, score, playerA, id);
        }

        public Game RegisterLowerResult(int key, int score, bool playerA, int id)
        {
            return _gameRepository.UpdateLowerHalf(key, score, playerA, id);
        }

        public Game EndGame(int gameId, int scoreA, int scoreB, string winnerId)
        {
            return _gameRepository.UpdateEndGame(gameId, scoreA, scoreB, winnerId);
        }

        public ICollection<Game> GetGamesByUserId(string id)
        {
            return _gameRepository.ReadGamesByUserId(id);
        }

        public int CalculateBestScore(ICollection<Game> games, string id)
        {
            int bestScore = 0;
            games.ForEach(delegate (Game game)
            {
                if (game.PlayerAId == id)
                {
                    if (bestScore < game.ScoreA)
                    {
                        if (game.ScoreA != null)
                            bestScore = (int)game.ScoreA;
                    }
                }
                else
                {
                    if (bestScore < game.ScoreB)
                    {
                        if (game.ScoreB != null)
                            bestScore = (int)game.ScoreB;
                    }
                }
            });
            return bestScore;



        }
        public int CalculateAvgScore(ICollection<Game> games, string id)
        {
            int total = 0;
            int counter = 0;
            games.ForEach(delegate (Game game)
            {
                if (game.PlayerAId == id)
                {
                    if (game.ScoreA != null)
                    {
                        total += (int)game.ScoreA;
                        counter++;
                    }
                }
                else
                {

                    if (game.ScoreB != null)
                    {
                        total += (int)game.ScoreB;
                        counter++;
                    }
                }
            });

            if (total == 0)
            {
                return 0;
            }
            else
            {
                return total / counter;
            }

        }

        public Game PostChat(int gameId, string name, string message)
        {
            ChatMessage chatMessage = new ChatMessage();
            chatMessage.ScreenName = name;
            chatMessage.Message = message;
            return _gameRepository.CreateChat(gameId, chatMessage);
        }




        public Game MakeGame(User userA, User userB, string gameName)
        {

            Game game = new Game();
            game.GameName = gameName;
            game.PlayerA = userA;
            game.PlayerAId = userA.Id;
            game.PlayerB = userB;
            game.PlayerBId = userB.Id;
            game.TimeStamp = DateTime.Now;
            game.Status = GameStatus.Making;


            return MakeGame(game);
        }

        public Game MakeGame(Game game)
        {
            Game newGame = _gameRepository.CreateGame(game);
            return newGame;

        }


    }
}