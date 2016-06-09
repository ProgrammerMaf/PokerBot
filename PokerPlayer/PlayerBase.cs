using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokerObjects;

namespace PokerPlayer
{
    public abstract class PlayerBase : IPlayer
    {
        private List<Card> cards;
        private int cashe;

        protected PlayerBase(List<Card> cards, int cashe)
        {
            this.cards = cards;
            this.cashe = cashe;
        }

        public abstract double? MakeBet(double cash, int playersOnTable, double callCost, Card[] onTable);

        public abstract double MakeForceBlind();

        public abstract double GetBigBlind();
    }
}
