﻿using System;

namespace PokerObjects
{
    public class Card
    {
        public readonly char Suit;
        public readonly int Rank;

        public Card(char suit, int rank)
        {
            Suit = suit;
            Rank = rank;
        }
    }
}