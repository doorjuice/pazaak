using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Deck
    {
        public Deck(int maxCards)
        {
            MaxCards = maxCards;
            Cards = new List<Card>(MaxCards);
        }

        public readonly int MaxCards;
        public List<Card> Cards { get; }
        public virtual bool IsEmpty { get { return Cards.Count == 0;} }
        public virtual bool IsFull { get { return Cards.Count == MaxCards; } }
        public int TotalValue { get { return Cards.Aggregate(0, (agg, card) => agg + card.Value); } }

        public virtual void AddCard(Card card)
        {
            if (IsFull)
                throw new Exception($"Deck is full (max {MaxCards} cards)");
            Cards.Add(card);
        }

        public virtual Card TakeCard(Card card)
        {
            if (!Cards.Remove(card))
                throw new Exception($"No such card <{card}> in the deck");
            return card;
        }
    }

    public class FixedDeck : Deck
    {
        private readonly Card placeHolder = new BlankCard();
        private int nbCards = 0;

        public FixedDeck(int maxCards) : base(maxCards)
        {
            for (int i = 0; i < maxCards; i++)
                Cards.Add(placeHolder);
        }

        public override bool IsEmpty { get { return nbCards == 0; } }
        public override bool IsFull { get { return nbCards == MaxCards; } }

        public override void AddCard(Card card)
        {
            if (IsFull)
                throw new Exception($"Deck is full (max {MaxCards} cards)");
            Cards[nbCards++] = card;
        }

        public override Card TakeCard(Card card)
        {
            var index = Cards.IndexOf(card);
            if (index < 0)
                throw new Exception($"No such card <{card}> in the deck");
            Cards[index] = placeHolder;
            nbCards--;
            return card;
        }
    }

    public class RandomDeck : Deck
    {
        public static RandomDeck GenMainDeck(int nbSets = 4)
        {
            var deck = new RandomDeck(nbSets * 10);
            for (int i = 0; i < nbSets; i++)
                for (int val = 1; val <= 10; val++)
                    deck.AddCard(new StandardCard(val));
            return deck;
        }

        private readonly Random rng;

        public RandomDeck(int maxCards, int rngSeed = 0) : base(maxCards)
        {
            if (rngSeed > 0)
                rng = new Random(rngSeed);
            else
                rng = new Random();
        }

        public Card TakeRandomCard()
        {
            if (IsEmpty)
                throw new Exception("Deck is empty");
            int index = rng.Next(Cards.Count);
            Card pick = Cards[index];
            Cards.RemoveAt(index);
            return pick;
        }
    }
}
