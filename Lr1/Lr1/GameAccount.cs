using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lr1
{
    class Game
    {
        private static int gameIDSEED = 1234567890;
        public int GameID;
        public int Rating;
        public GameAccount Player1;
        public GameAccount Player2;

        public Game(int rating, GameAccount p1, GameAccount p2)
        {
            if (rating < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(rating), "Rating must be positive value");
            }
            Rating = rating;
            Player1 = p1;
            Player2 = p2;
            GameID = gameIDSEED++;
        }
    }

    class GameAccount
    {
        public string UserName { get; set; }
        public int CurrentRating
        {
            get
            {
                int rating = 1;
                foreach (var item in allGames)
                {
                    rating = ChangeRating(item, rating);
                }

                if (rating < 1)
                    rating = 1;

                return rating;
            }
        }
        public int GamesCount
        {
            get
            {
                int count = 0;
                foreach(var item in allGames)
                {
                    count++;
                }
                return count;
            }
        }
        public List<Game> allGames = new List<Game>();

        public GameAccount(string userName)
        {
            UserName = userName;
        }

        private int ChangeRating(Game game, int rating)
        {
            if (game.Player1 == this)
            {
                rating += game.Rating;
            }
            else
            {
                rating -= game.Rating;
            }
            return rating;
        }

        public void WinGame(GameAccount opponent, int rating)
        {
            Game game = new Game(rating, this, opponent);
            allGames.Add(game);
            opponent.allGames.Add(game);
        }

        public void LostGame(GameAccount opponent, int rating)
        {
            Game game = new Game(rating, opponent, this);
            allGames.Add(game);
            opponent.allGames.Add(game);
        }
        public string GetStats()
        {
            var report = new StringBuilder();

            int rating = 0;
            report.AppendLine($"{"GameID",-11}{"Opponent",15}{"Rating",10}{"Result",10}");
            foreach (var item in allGames)
            {
                rating = ChangeRating(item, rating);
                var opponent = item.Player1 == this ? item.Player2 : item.Player1;
                string getResult = item.Player1 == this ? "Won" : "Lose";
                report.AppendLine($"{item.GameID,11}{opponent.UserName,15}{item.Rating,10}{getResult,10}");
            }
            return report.ToString();
        }
    }

    
}
