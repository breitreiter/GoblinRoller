using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoblinRoller.Bots
{
    /// <summary>
    /// Zero defense
    /// </summary>
    internal class Blitz : BotBase
    {
        private Random _r = new();

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

            // 50% chance to keep a random card to add some theft deterrence
            int maybeKeep = _r.Next(0, 14);
            if (maybeKeep < 7)
                planToPlay.RemoveAt(maybeKeep);

            if (planToPlay.Count > 0)
                foreach (Card card in planToPlay)
                    MyHand.MoveToPlay(card);
        }
    }
}
