using Lr2;

public class Program
{
    public static void Main()
    {
        //GameAccount player1 = new GameAccount("Player1");
        //GameAccount player2 = new GameAccount("Player2");
        BaseGameAccount player1 = new BaseGameAccount("Player1");
        BaseGameAccount player2 = new BaseGameAccount("Player2");

        Console.WriteLine($"Current rating of {player1.UserName}: " + player1.CurrentRating);
        Console.WriteLine($"Current rating of {player2.UserName}: " + player2.CurrentRating);
        Console.WriteLine($"Current games count of {player1.UserName}: " + player1.GamesCount);
        Console.WriteLine($"Current games count of {player2.UserName}: " + player2.GamesCount);


        player1.WinGame(GameType.TrainingGame, player2);
        player2.LoseGame(GameType.StandartGame, player1, 70);

        try 
        { 
            player1.LoseGame(GameType.StandartGame, player1, 20);
        }
        catch (ArgumentException)
        {
            Console.WriteLine("You can't play with yourself\n");
        }

        player2.WinGame(GameType.StandartGame, player1, 50);

        Console.WriteLine("Rating of player1 = " + player1.CurrentRating);
        Console.WriteLine("Rating of player2 = " + player2.CurrentRating);
        player2.WinGame(GameType.AllInRatingGame, player1);

        Console.WriteLine(player1.GetStats());
        Console.WriteLine(player2.GetStats());

        try
        { 
            player1.WinGame(GameType.StandartGame, player2, -10);
        }
        catch (ArgumentOutOfRangeException)
        {
            Console.WriteLine("Rating must be positive value");
        }

        Console.WriteLine($"Current rating of {player1.UserName}: " + player1.CurrentRating);
        Console.WriteLine($"Current rating of {player2.UserName}: " + player2.CurrentRating);
        Console.WriteLine($"Current games count of {player1.UserName}: " + player1.GamesCount);
        Console.WriteLine($"Current games count of {player2.UserName}: " + player2.GamesCount);

        StreakGameAccount streakplayer1 = new StreakGameAccount("Sp1");
        StreakGameAccount streakplayer2 = new StreakGameAccount("Sp2");
        streakplayer1.WinGame(GameType.StandartGame, streakplayer2, 10);
        streakplayer2.WinGame(GameType.TrainingGame, streakplayer1);
        streakplayer1.WinGame(GameType.StandartGame, streakplayer2, 20);
        Console.WriteLine(streakplayer1.CurrentRating);
        streakplayer1.WinGame(GameType.StandartGame, streakplayer2, 10);
        Console.WriteLine(streakplayer1.CurrentRating);
        streakplayer1.WinGame(GameType.StandartGame, streakplayer2, 10);
        streakplayer1.WinGame(GameType.TrainingGame, streakplayer2);
        streakplayer1.WinGame(GameType.AllInRatingGame, streakplayer2);
        streakplayer1.LoseGame(GameType.StandartGame, streakplayer2, 30);
        streakplayer1.WinGame(GameType.StandartGame, streakplayer2, 20);
        streakplayer2.WinGame(GameType.StandartGame, streakplayer1, 10);
        streakplayer2.WinGame(GameType.StandartGame, streakplayer1, 30);
        streakplayer2.WinGame(GameType.TrainingGame, streakplayer1);
        streakplayer2.WinGame(GameType.AllInRatingGame, streakplayer1);
        streakplayer2.WinGame(GameType.AllInRatingGame, streakplayer1);
        Console.WriteLine(streakplayer1.GetStats());
        Console.WriteLine(streakplayer2.GetStats());
        Console.WriteLine("Rating of Sp1 = " + streakplayer1.CurrentRating);
        Console.WriteLine("Rating of Sp2 = " + streakplayer2.CurrentRating);
        Console.WriteLine();


        BonusGameAccount bonusplayer1 = new BonusGameAccount("Bonusp1");
        BaseGameAccount baseplayer1 = new BaseGameAccount("Bp2");
        BaseGameAccount baseplayer2 = new BaseGameAccount("Bp3");
        bonusplayer1.WinGame(GameType.StandartGame, baseplayer2, 30);
        baseplayer1.WinGame(GameType.StandartGame, baseplayer2, 30);
        bonusplayer1.LoseGame(GameType.StandartGame, baseplayer2, 20);
        baseplayer1.LoseGame(GameType.StandartGame, baseplayer2, 20);
        Console.WriteLine(bonusplayer1.GetStats());
        Console.WriteLine(baseplayer1.GetStats());
        Console.WriteLine("Rating of Bonusp1 = " + bonusplayer1.CurrentRating);
        Console.WriteLine("Rating of Bp2 = " + baseplayer1.CurrentRating);
    }
}