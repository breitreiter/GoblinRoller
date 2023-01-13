using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoblinRoller.Bots
{ 
    /// <summary>
    /// Interactive shell for playing against the computer
    /// </summary>
    internal class Human : BotBase
{
    internal Human(Deck deck) : base(deck)
    {
    }

    internal override void PlanTurn(GameTurn turn)
    {
        foreach (BotBase bot in turn.Players)
            Console.WriteLine(bot.Name + ":" + bot.Gold);

        Console.WriteLine("Your hand: ");
        int i = 0;
        foreach (Card card in this.MyHand.Keep)
        {
            Console.WriteLine(i + " " + card.ToString());
            i++;
        }
        Console.Write("Play which? ");
        string toPlay = Console.ReadLine();

        if (toPlay == null || toPlay.Length == 0)
            return;

        List<Card> planToPlay = new List<Card>();

        foreach (char c in toPlay)
        {
            int cardIndex = 0;
            if (int.TryParse(c.ToString(), out cardIndex))
            {
                planToPlay.Add(MyHand.Keep[cardIndex]);
            }
        }

        if (planToPlay.Count > 0)
            foreach (Card card in planToPlay)
                MyHand.MoveToPlay(card);

    }
}
}
