using Lr1;

public class Program
{
    public static void Main()
    {
        GameAccount player1 = new GameAccount("Player1");
        GameAccount player2 = new GameAccount("Player2");

        Console.WriteLine($"Current rating of {player1.UserName}: " + player1.CurrentRating);
        Console.WriteLine($"Current rating of {player2.UserName}: " + player2.CurrentRating);
        Console.WriteLine($"Current games count of {player1.UserName}: " + player1.GamesCount);
        Console.WriteLine($"Current games count of {player2.UserName}: " + player2.GamesCount);


        player1.WinGame(player2, 20);
        player2.LostGame(player1, 30);
        player1.LostGame(player2, 10);
        player2.WinGame(player1, 0);

        Console.WriteLine(player1.GetStats());
        Console.WriteLine(player2.GetStats());

        try
        { 
        player1.WinGame(player2, -10);
        }
        catch (ArgumentOutOfRangeException)
        {
            Console.WriteLine("Rating must be positive value");
        }

        Console.WriteLine($"Current rating of {player1.UserName}: " + player1.CurrentRating);
        Console.WriteLine($"Current rating of {player2.UserName}: " + player2.CurrentRating);
        Console.WriteLine($"Current games count of {player1.UserName}: " + player1.GamesCount);
        Console.WriteLine($"Current games count of {player2.UserName}: " + player2.GamesCount);
    }
}