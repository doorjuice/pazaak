using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Deck
    {
        public static Deck GenMainDeck(int nbSets = 4)
        {
            var deck = new Deck(nbSets * 10);
            for (int i = 0; i < nbSets; i++)
                for (int val = 1; val <= 10; val++)
                    deck.AddCard(new Card(val));
            return deck;
        }

        public readonly int MaxCards;

        private readonly List<Card> cards;
        private readonly Random rng;

        public Deck(int maxCards, int rngSeed = 0)
        {
            MaxCards = maxCards;
            cards = new List<Card>(MaxCards);
            if (rngSeed > 0)
                rng = new Random(rngSeed);
            else
                rng = new Random();
        }

        public bool IsEmpty { get { return cards.Count == 0;} }
        public bool IsFull { get { return cards.Count == MaxCards; } }
        public int TotalValue { get { return cards.Aggregate(0, (agg, card) => agg + card.Value); } }

        public void AddCard(Card card)
        {
            if (IsFull)
                throw new Exception($"Deck is full (max {MaxCards} cards)");
            cards.Add(card);
        }

        public Card TakeCard(Card card)
        {
            if (!cards.Remove(card))
                throw new Exception($"No such card <{card}> in the deck");
            return card;
        }

        public Card TakeRandomCard()
        {
            if (IsEmpty)
                throw new Exception("Deck is empty");
            int index = rng.Next(cards.Count);
            Card pick = cards[index];
            cards.RemoveAt(index);
            return pick;
        }
    }
}
