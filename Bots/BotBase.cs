using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoblinRoller.Bots
{
    internal class BotBase
    {
        internal Hand MyHand;
        internal int Gold = 0;
        internal string Name = string.Empty;

        internal BotBase(Deck deck, string name)
        {
            Name = name;
            MyHand = new Hand(deck);
            MyHand.DrawUp();
        }

        internal virtual void PlanTurn(GameTurn turn)
        {

        }

    }

}
