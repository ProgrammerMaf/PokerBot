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
        private double cashe;
        private readonly int id;

        public Func<double, int, double, Card[], double?> GetBet;
        public Func<Card[]> GetSelfCards;

        protected PlayerBase(int id, double cashe)
        {
            cards = null;
            this.cashe = cashe;
            this.id = id;
        }

        public abstract double? MakeBet(double cash, int playersOnTable, double callCost, Card[] onTable);

        public double MakeForceBlind(double count)
        {
            cashe -= count;
            return count;
        }

        public void AddCards(List<Card> cards)
        {
            if (cards.Count != 2)
                throw new Exception("Полученно не верное количество карт");

        }
        public void AddCashe(double count)
            => cashe += count;
        
        
        public override int GetHashCode()
            => id;
        

        public override bool Equals(object obj)
            =>((PlayerBase) obj).id == id;
        
    }
}
