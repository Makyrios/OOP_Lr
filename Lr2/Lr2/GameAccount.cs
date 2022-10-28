using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lr2
{
    public class BaseGameAccount
    {
        public string UserName { get; set; }
        public virtual int CurrentRating
        {
            get
            {
                int rating = 1;
                foreach (var item in allGames)
                {
                    rating = ChangeRating(item, rating);
                    if (rating < 1)
                        rating = 1;
                }


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

        GameFactory gf = new GameFactory();

        public BaseGameAccount(string userName)
        {
            UserName = userName;
        }

        protected int ChangeRating(Game game, int rating)
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

        public virtual Game WinGame(GameType type, BaseGameAccount opponent, int rating = 0)
        {
            if (opponent == this)
            {
                throw new ArgumentException("You cannot play with yourself");
            }
            Game game = gf.CreateGame(type, this, opponent, rating);
            allGames.Add(game);
            opponent.allGames.Add(game);
            return game;
        }

        public virtual Game LoseGame(GameType type, BaseGameAccount opponent, int rating = 0)
        {
            if (opponent == this)
            {
                throw new ArgumentException("You cannot play with yourself");
            }
            Game game = gf.CreateGame(type, opponent, this, rating);
            allGames.Add(game);
            opponent.allGames.Add(game);
            return game;
        }

        public virtual string GetStats()
        {
            Console.WriteLine($"Statistics for {this.GetType().Name}");
            var report = new StringBuilder();

            int rating = 0;
            report.AppendLine($"{"GameID",11}{"GameType",20}{"Opponent",15}{"Rating",10}{"Result",10}");
            foreach (var item in allGames)
            {
                rating = ChangeRating(item, rating);
                var opponent = item.Player1 == this ? item.Player2 : item.Player1;
                string getResult = item.Player1== this ? "Won" : "Lose";
                report.AppendLine($"{item.GameID,11}{item.GameType,20}{opponent.UserName,15}{item.Rating,10}{getResult,10}");
            }
            return report.ToString();
        }
    }

    // For won games get 2x points, for lost games lose 0.5x points
    public class BonusGameAccount : BaseGameAccount
    {
        public BonusGameAccount(string username) : base(username) { }

        public override int CurrentRating
        {
            get
            {
                int rating = 1;
                foreach (var item in allGames)
                {
                    int changeRating = ChangeRating(item, rating);
                    if (changeRating >= rating)
                    {
                        rating += item.Rating * 2;
                    }
                    else
                    {
                        rating -= item.Rating / 2;
                    }
                    if (rating < 1)
                        rating = 1;
                }


                return rating;
            }
        }
    }

    // Account gets additional points for win streak
    public class StreakGameAccount : BaseGameAccount
    {
        // Necessary count of won games. 1 = every first game, 2 = every second game etc.
        private const uint StreakCount = 2;
        // Bonus points for streak
        private const int BonusPoints = 50;

        public StreakGameAccount(string username) : base(username) { }

        public override Game WinGame(GameType type, BaseGameAccount opponent, int rating = 0)
        {
            // Check if streak > StreakCount
            int currentStreak = 0;
            bool isStreak = false;
            if (allGames.Count() >= StreakCount)
            {
                // Check if last N not training games were won
                for (int i = allGames.Count() - 1; i >= 0; i--)
                {
                    if (allGames[i].GameType == GameType.TrainingGame)
                        continue;

                    if (allGames[i].Player1 == this)
                    {
                        currentStreak++;
                    }

                    if (i <= 1 || allGames[i].Player1 != this)
                    {
                        if (currentStreak + 1 >= StreakCount)
                        {
                            isStreak = true;
                        }
                        break;
                    }
                }
            }
            Game game;
            if (isStreak && type != GameType.TrainingGame || StreakCount == 1) { 
                game = base.WinGame(type, opponent, rating + BonusPoints);
                game.IsStreak = true;
                Console.WriteLine($"Current streak is {currentStreak + 1} wins. Added {BonusPoints} points");
            }
            else 
            {
                isStreak = false;
                game = base.WinGame(type, opponent, rating);
            }
            return game;
        }

        public override string GetStats()
        {
            Console.WriteLine($"Statistics for {this.GetType().Name}");
            var report = new StringBuilder();

            int rating = 0;
            report.AppendLine($"{"GameID",11}{"GameType",20}{"Opponent",15}{"Rating",14}{"Result",15}");
            foreach (var item in allGames)
            {
                rating = ChangeRating(item, rating);
                var opponent = item.Player1 == this ? item.Player2 : item.Player1;
                string getResult = item.Player1 == this ? "Won" : "Lose";
                string bonusPointLine = item.IsStreak && item.Player1 == this ? "+" + BonusPoints.ToString() : "";
                report.AppendLine($"{item.GameID,11}{item.GameType,20}{opponent.UserName,15}{item.Rating+bonusPointLine,14}{getResult,15}");
            }
            return report.ToString();
        }
    }
    
}
