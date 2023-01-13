using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoblinRoller.Bots
{
    internal class Hand
    {
        private Deck _deck;
        internal Hand(Deck deck)
        {
            _deck = deck;
            Keep = new List<Card>();
            Play = new List<Card>();
        }

        internal List<Card> Keep;
        internal List<Card> Play;

        internal int StealPower
        {
            get
            {
                if (Play.Count == 0)
                    return 0;

                int power = 0;
                foreach (Card card in Play)
                    if (!card.IsGoblin)
                        power += card.Power;

                return power;
            }
        }

        internal int MinePower
        {
            get
            {
                if (Play.Count == 0)
                    return 0;

                int power = 0;
                foreach (Card card in Play)
                    if (card.IsGoblin)
                        power += card.Power;

                return power;
            }
        }

        internal int DefensePower
        {
            get
            {
                if (Keep.Count == 0)
                    return 0;

                int power = 0;
                foreach (Card card in Keep)
                    power += card.Power;

                return power;
            }
        }

        internal void DrawUp()
        {
            while (Keep.Count < 7)
            {
                Keep.Add(_deck.Draw());
            }
        }

        internal void Discard()
        {
            foreach (Card card in Play)
            {
                _deck.Discard(card);
            }
            Play.Clear();
        }

        internal void MoveToPlay(Card card)
        {
            Keep.Remove(card);
            Play.Add(card);
        }

        public override string ToString()
        {
            string output = "Play: ";
            foreach (Card card in Play)
                output += card.ToString() + " ";
            output += "; Keep: ";
            foreach (Card card in Keep)
                output += card.ToString() + " ";
            return output;
        }


    }
}
