using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerObjects;

namespace GameModelTest
{
    [TestClass]
    public class GameModelTest
    {
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
            var firstPlayer = new SimpleBettor("Petya", new [] {new Card('A', 14), new Card('B', 14)}, 250, 250, 250, 0, 0, 0);
            var secondPlayer = new SimpleBettor("Vasya", new [] {new Card('C', 14), new Card('D', 14)}, 250, 500, 0, 0, 0, 0);
            // Нет торга.
            var game = new Game(new[] {firstPlayer, secondPlayer}, 250, 500); // last two numbers are ante and big blind size
            game.AskBets();
            if (!game.Ends())
            { 
                game.GoNextStreet(new[] { new Card('A', 12), new Card('A', 11), new Card('B', 13)});
                game.AskBets();
            }
            if (!game.Ends())
            {
                game.GoNextStreet(new[] {new Card('A', 10)});
                game.AskBets();
            }
            if (!game.Ends())
            {
                game.GoNextStreet(new[] { new Card('B', 10) });
                game.AskBets();
            }
            var gameResult = game.GetResult();
            //comparing result of game
        }
    }
}
