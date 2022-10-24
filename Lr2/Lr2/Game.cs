using Lr2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lr2
{

    public enum GameType
    {
        StandartGame,
        TrainingGame,
        AllInRatingGame
    }

    public abstract class Game
    {
        // Fields
        protected static int gameIDSEED = 1234567890;
        protected int gameID;
        protected int rating;
        protected BaseGameAccount player1;
        protected BaseGameAccount player2;
        protected GameType gameType;
        protected bool isStreak = false;

        // Properties
        public GameType GameType { get { return gameType; } }
        public int GameID { get { return gameID; } }
        public int Rating { get { return rating; } }
        public BaseGameAccount Player1 { get { return player1; } }
        public BaseGameAccount Player2 { get { return player2; } }
        
        // bool field for StreakGameAccount to determine if game is in streak
        public bool IsStreak {
            get { return isStreak; }
            set { isStreak = value; }
        }

        // Constructor
        public Game(BaseGameAccount p1, BaseGameAccount p2)
        {
            player1 = p1;
            player2 = p2;
        }
    }

    // Standart game on certain rating
    class StandartGame : Game
    {
        public StandartGame(int rating, BaseGameAccount p1, BaseGameAccount p2) : base(p1, p2)
        {
            if (rating < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(rating), "Rating must be positive value");
            }

            this.rating = rating;
            gameID = gameIDSEED++;
            gameType = GameType.StandartGame;
        }
    }

    // Training game with no rating
    class TrainingGame : Game
    {
        public TrainingGame(BaseGameAccount p1, BaseGameAccount p2) : base(p1, p2)
        {
            rating = 0;
            gameID = gameIDSEED++;
            gameType = GameType.TrainingGame;
        }
    }

    // Game on the lowest rating of two players
    class AllInRatingGame : Game
    {
        public AllInRatingGame(BaseGameAccount p1, BaseGameAccount p2) : base(p1, p2)
        {
            rating = Math.Min(p1.CurrentRating, p2.CurrentRating);
            gameID = gameIDSEED++;
            gameType = GameType.AllInRatingGame;
        }
    }

    // Factory class
    public class GameFactory
    {
        public Game CreateGame(GameType type, BaseGameAccount p1, BaseGameAccount p2, int raiting = 0)
        {
            switch (type)
            {
                case GameType.StandartGame:
                    return new StandartGame(raiting, p1, p2);

                case GameType.TrainingGame:
                    return new TrainingGame(p1, p2);

                case GameType.AllInRatingGame:
                    return new AllInRatingGame(p1, p2);

                default:
                    return new TrainingGame(p1, p2);
            }
        }
    }
}
