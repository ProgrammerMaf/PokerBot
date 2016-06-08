using System.Collections.Generic;
using PokerObjects;

namespace PokerPlayer
{
    public abstract class BasePlayer : IPlayer
    {
        private List<Card> cards;
        private int cashe;

        protected BasePlayer(List<Card> cards, int cashe)
        {
            this.cards = cards;
            this.cashe = cashe;
        }

        public abstract Bet MakeBet(double cash, int playersOnTable, double callCost, Card[] onTable);

        public abstract Bet GetSmallBlind();

        public abstract Bet GetBigBlind();
    }
}
