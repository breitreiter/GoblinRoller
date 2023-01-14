using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoblinRoller.Bots
{
    /// <summary>
    /// Simple heuristics
    /// </summary>
    internal class Simple : BotBase
    {
        private Random _r = new();

        internal Simple(Deck _deck, string name) : base(_deck, name)
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

                if (this.Gold > 4)
                {
                    // Keep a random card to add some theft deterrence
                    int maybeKeep = _r.Next(0, 7);
                    planToPlay.RemoveAt(maybeKeep);
                }
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
}
