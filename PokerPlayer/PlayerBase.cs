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
        protected double cashe;
        protected readonly string id;

        public Func<double, int, double, Card[], Bet> GetBet;
        public Func<Card[]> GetSelfCards;

        protected PlayerBase(string id, double cashe)
        {
            this.cashe = cashe;
            this.id = id;
        }

        public abstract Bet MakeBet(double cash, int playersOnTable, double callCost, Card[] onTable);

        public Bet MakeForceBlind(double count)
        {
            cashe -= count;
            return new Bet(count, id);
        }

        public void AddCards(List<Card> cards)
        {
            if (cards.Count != 2)
                throw new Exception("Полученно не верное количество карт");
            GetSelfCards = cards.ToArray;

        }
        public void AddCashe(double count)
            => cashe += count;
        
        
        public override int GetHashCode()
            => id.GetHashCode();
        

        public override bool Equals(object obj)
            =>((PlayerBase) obj).id == id;

        public override string ToString()
            => id;
    }
}
