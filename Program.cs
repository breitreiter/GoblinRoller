using GoblinRoller;
using System.Xml;

internal class Program
{
    private static void Main(string[] args)
    {
        int playerCount = 6;

        List<int> winRate = new List<int>();
        for (int i = 0; i < playerCount; i++)
            winRate.Add(0);

        int totalTurns = 0;

        for (int i = 0; i < 100; i++)
        {
            Deck deck = new Deck();
            deck.Init(playerCount);

            GameTurn game = new GameTurn();

            game.Players.Add(new GoblinRoller.Bots.Blitz(deck));
            game.Players[0].Name = "Annie B";

            game.Players.Add(new GoblinRoller.Bots.Blitz(deck));
            game.Players[1].Name = "Bob B";

            game.Players.Add(new GoblinRoller.Bots.Simple(deck));
            game.Players[2].Name = "Chuck S";

            game.Players.Add(new GoblinRoller.Bots.Simple(deck));
            game.Players[3].Name = "Dom S";

            game.Players.Add(new GoblinRoller.Bots.Rando(deck));
            game.Players[4].Name = "Egon R";

            game.Players.Add(new GoblinRoller.Bots.Human(deck));
            game.Players[5].Name = "You";

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

        Console.WriteLine(totalTurns + " turns played");
        Console.WriteLine("Annie B:" + winRate[0]);
        Console.WriteLine("Bob   B:" + winRate[1]);
        Console.WriteLine("Chuck S:" + winRate[2]);
        Console.WriteLine("Dom   S:" + winRate[3]);
        Console.WriteLine("Egon  R:" + winRate[4]);
        Console.WriteLine("Fred  R:" + winRate[5]);

        Console.ReadLine();
    }
}


