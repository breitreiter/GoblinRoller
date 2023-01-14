using GoblinRoller;
using GoblinRoller.Bots;
using System.Text;
using System.Xml;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

        Console.WriteLine("Welcome to Goblins and Gremlins!");

        int playerCount = int.MinValue;

        while(playerCount == int.MinValue)
        {
            Console.Write("Total number of players (3-6)?");
            string? countInput = Console.ReadLine();
            if (countInput != null)
                int.TryParse(countInput, out playerCount);
            if (playerCount > 6 || playerCount < 3)
            {
                Console.WriteLine("Please enter a value between 3 and 6");
                playerCount = int.MinValue;
            }
        }

        bool humanPlayer = false;
        Console.Write("Include a human player (y/N)?");
        ConsoleKeyInfo k = Console.ReadKey();
        if (k.KeyChar == 'y')
            humanPlayer = true;

        Console.WriteLine();

        string[] playerNames = { "Annie", "Bob", "Chuck", "Dom", "Egon", "Fred" };

        List<int> winRate = new List<int>();
        for (int i = 0; i < playerCount; i++)
            winRate.Add(0);

        int totalTurns = 0;

        int rounds = int.MinValue;

        while (rounds == int.MinValue)
        {
            Console.Write("Number of rounds to play?");
            string? countInput = Console.ReadLine();
            if (countInput != null)
                int.TryParse(countInput, out rounds);
            if (playerCount > 6 || playerCount < 3)
            {
                Console.WriteLine("Please enter a numeric value");
                rounds = int.MinValue;
            }
        }

        for (int i = 0; i < rounds; i++)
        {
            Deck deck = new Deck();
            deck.Init(playerCount);

            GameTurn game = new GameTurn();

            if (humanPlayer)
                game.Players.Add(new GoblinRoller.Bots.Human(deck, "You"));

            while(game.Players.Count < playerCount)
            {
                Random random = new Random();
                int botType = random.Next(1, 4);
                if (botType == 1)
                    game.Players.Add(new GoblinRoller.Bots.Blitz(deck, playerNames[game.Players.Count]));
                else if (botType == 2)
                    game.Players.Add(new GoblinRoller.Bots.Simple(deck, playerNames[game.Players.Count]));
                else
                    game.Players.Add(new GoblinRoller.Bots.Rando(deck, playerNames[game.Players.Count]));
            }

            int turns = 0;
            while (game.Winner == null)
            {
                game.ResolveTurn();
                turns++;
            }

            winRate[game.Players.IndexOf(game.Winner)]++;

            Console.WriteLine("!! " + game.Winner.Name + " wins in " + turns);

            totalTurns += turns;
        }

        Console.WriteLine(totalTurns + " turns played, avg " + (totalTurns / rounds) + " turns per game ");

        for (int i = 0; i < winRate.Count; i++)
            if (humanPlayer && i == 0)
                Console.WriteLine("You:" + winRate[i]);
            else
                Console.WriteLine(playerNames[i] + ":" + winRate[i]);

    }
}


