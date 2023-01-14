using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoblinRoller
{
    internal class d6
    {
        private Random _rnd = new();

        internal int Roll()
        {
            return _rnd.Next(1, 7);
        }

        internal int Roll(int modifier)
        {
            return Roll() + modifier;
        }
    }

    internal class Card
    {
        internal bool IsGoblin;
        internal int Power;
        internal Card(bool isGoblin, int power)
        {
            IsGoblin = isGoblin;
            Power = power;
        }

        public override string ToString()
        {
            if (IsGoblin)
                return "⛏️" + Power;
            else
                return "🗡" + Power;
        }
    }

    internal class Deck
    {
        private List<Card> _deck = new List<Card>();
        private List<Card> _discard = new List<Card>();
        private Random _rnd = new();

        internal void Init(int playerCount)
        {
            for (int i = 0; i < playerCount; i++)
            {
                // Goblins
                _deck.Add(new Card(true, 1));
                _deck.Add(new Card(true, 1));
                _deck.Add(new Card(true, 2));
                _deck.Add(new Card(true, 2));
                _deck.Add(new Card(true, 4));
                _deck.Add(new Card(true, 4));
                // Gremlins
                _deck.Add(new Card(false, 1));
                _deck.Add(new Card(false, 1));
                _deck.Add(new Card(false, 2));
                _deck.Add(new Card(false, 2));
                _deck.Add(new Card(false, 4));
                _deck.Add(new Card(false, 4));
            }
        }

        internal Card Draw()
        {
            if (_deck.Count == 0)
            {
                _deck = _discard;
                _discard = new List<Card>();
            }

            int returnIdx = _rnd.Next(0, _deck.Count - 1);
            Card output = _deck[returnIdx];
            _deck.RemoveAt(returnIdx);

            return output;

        }

        internal void Discard(Card card)
        {
            _discard.Add(card);
        }
    }
}
