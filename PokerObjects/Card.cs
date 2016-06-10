using System;
using System.Collections.Generic;
using System.Linq;

namespace PokerObjects
{
    public enum Suit
    {
        Heart,
        Diamond,
        Club,
        Spade
    }

    public enum CardRank
    {
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King,
        Ace 
    }
   
    public class Card
    {
        public readonly Suit Suit;
        public readonly CardRank CardRank;
        public const int MaxRank = 14;
        private static readonly Suit[] Suits =  {Suit.Club, Suit.Diamond, Suit.Heart, Suit.Spade};
        private static readonly CardRank[] Ranks = {CardRank.Ace, CardRank.King, CardRank.Queen, CardRank.Jack, CardRank.Ten, CardRank.Nine, CardRank.Eight, CardRank.Seven, CardRank.Six, CardRank.Five, CardRank.Four, CardRank.Three, CardRank.Two};

        public static IEnumerable<Card> GetAllCards()
        {
            return from s in Suits from r in Ranks select new Card(s, r);
        }

        public Card(Suit suit, CardRank cardRank)
        {
            Suit = suit;
            CardRank = cardRank;
        }

        public override string ToString()
        {
            string suitChar = null;
            if (Suit == Suit.Heart) suitChar = "♥";
            if (Suit == Suit.Diamond) suitChar = "♦";
            if (Suit == Suit.Club) suitChar = "♣";
            if (Suit == Suit.Spade) suitChar = "♠";
            return $"{CardRank}{suitChar}" ;
        }

        public override int GetHashCode()
        {
            return Suit.GetHashCode() * 17 + CardRank.GetHashCode();
        }
    }
}