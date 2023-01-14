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

        internal Rando(Deck _deck, string name) : base(_deck, name)
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

}
