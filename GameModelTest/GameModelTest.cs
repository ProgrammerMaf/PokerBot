using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerObjects;
/*
namespace GameModelTest
{
    [TestClass]
    public class GameModelTest
    {
        public Card GetCardFromStrings(char c, int r)
        {
            var suits = new Dictionary<char, Suit>
            {
                {'A', Suit.Spade},
                {'B', Suit.Club},
                {'C', Suit.Diamond},
                {'D', Suit.Heart}
            };
            var ranks = new Dictionary<int, CardRank>
            {
                {2, CardRank.Two},
                {3, CardRank.Three},
                {4, CardRank.Four},
                {5, CardRank.Five},
                {6, CardRank.Six},
                {7, CardRank.Seven},
                {8, CardRank.Eight},
                {10, CardRank.Nine},
                {11, CardRank.Ten},
                {12, CardRank.Jack},
                {13, CardRank.Queen},
                {14, CardRank.King},
                {15, CardRank.Ace}
            };
            return new Card(suits[c], ranks[r]);
        }
        private class SimpleBettor
        {
            private readonly double?[] bets;
            private int betPosition;
            private readonly string id;
            private Card[] cards;
            public SimpleBettor(string id, Card[] cards, params double?[] bets)
            {
                this.bets = bets;
                this.id = id;
                this.cards = cards;
                betPosition = 0;
            }

            public Bet GetBet()
            {
                if (bets[betPosition] != null)
                {
                    var d = bets[betPosition++];
                    if (d != null) return new Bet((double)d, id);
                }
                betPosition++;
                return null;
            }

            public Bet GetForceBet(double value)
            {
                return GetBet();
            }
            public Card[] GetCards()
            {
                return cards;
            }

        }
        [TestMethod]
        public void SimpleScenario()
        {
            var firstPlayer = new SimpleBettor("Petya", new [] { GetCardFromStrings('A', 14), GetCardFromStrings('B', 14)}, 250, 250, 250, 0, 0, 0);
            var secondPlayer = new SimpleBettor("Vasya", new [] { GetCardFromStrings('C', 14), GetCardFromStrings('D', 14)}, 250, 500, 0, 0, 0, 0);
            // Нет торга.
            var game = new Game(new[] {firstPlayer, secondPlayer}, 250, 500); // last two numbers are ante and big blind size
            game.AskBets();
            if (!game.Ends())
            { 
                game.GoNextStreet(new[] { GetCardFromStrings('A', 12), GetCardFromStrings('A', 11), GetCardFromStrings('B', 13)});
                game.AskBets();
            }
            if (!game.Ends())
            {
                game.GoNextStreet(new[] { GetCardFromStrings('A', 10)});
                game.AskBets();
            }
            if (!game.Ends())
            {
                game.GoNextStreet(new[] { GetCardFromStrings('B', 10) });
                game.AskBets();
            }
            var gameResult = game.GetResult();
            //comparing result of game
        }
    }
}*/
