using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoblinRoller.Bots
{
    /// <summary>
    /// Picks randomly
    /// </summary>
    internal class Rando : BotBase
    {
        private Random _r = new();

        internal Rando(Deck _deck) : base(_deck)
        {
        }

        internal override void PlanTurn(GameTurn turn)
        {
            base.PlanTurn(turn);

            List<Card> planToPlay = new List<Card>();

            foreach(Card card in MyHand.Keep)
            {
                if (_r.Next(0,2) == 1)
                    planToPlay.Add(card);
            }

            if (planToPlay.Count > 0)
                foreach(Card card in planToPlay)
                    MyHand.MoveToPlay(card);
        }
    }

    /// <summary>
    /// Zero defense
    /// </summary>
    internal class Blitz : BotBase
    {
        internal Blitz(Deck _deck) : base(_deck)
        {
        }

        internal override void PlanTurn(GameTurn turn)
        {
            base.PlanTurn(turn);

            List<Card> planToPlay = new List<Card>();


            foreach (Card card in MyHand.Keep)
            {
                planToPlay.Add(card);
            }

            if (planToPlay.Count > 0)
                foreach (Card card in planToPlay)
                    MyHand.MoveToPlay(card);
        }
    }

    /// <summary>
    /// Simple heuristics
    /// </summary>
    internal class Simple : BotBase
    {
        internal Simple(Deck _deck) : base(_deck)
        {
        }

        internal override void PlanTurn(GameTurn turn)
        {
            base.PlanTurn(turn);

            List<Card> planToPlay = new List<Card>();

            int maxEnemyCash = 0;
            foreach (BotBase bot in turn.Players)
                if (bot != this && bot.Gold > maxEnemyCash)
                    maxEnemyCash = bot.Gold;

            if (turn.GoldAtStake < 4 && maxEnemyCash < 3)
            {
                // dump all 1s
                foreach (Card card in MyHand.Keep)
                    if (card.Power == 1)
                        planToPlay.Add(card);
            }
            else if (turn.GoldAtStake > 3 && maxEnemyCash > 3)
            {
                // blitz
                foreach (Card card in MyHand.Keep)
                    planToPlay.Add(card);
            }
            else if (turn.GoldAtStake > 3)
            {
                // mine
                foreach (Card card in MyHand.Keep)
                    if (card.IsGoblin || card.Power == 1)
                        planToPlay.Add(card);
            }
            else 
            {
                // steal
                foreach (Card card in MyHand.Keep)
                    if (!card.IsGoblin || card.Power == 1)
                        planToPlay.Add(card);
            }

            if (planToPlay.Count > 0)
                foreach (Card card in planToPlay)
                    MyHand.MoveToPlay(card);
        }
    }

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
                if(int.TryParse(c.ToString(), out cardIndex))
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
