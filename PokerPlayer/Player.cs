using PokerObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerPlayer
{
    public class Player : PlayerBase
    {
        public string Id;
        public Player(string id, List<Card> cards, int cashe) 
            : base(cards, cashe)
        {
            Id = id;
        }
        public double? MakeBet(List<PlayerInRoundState> players, int selfNumber, Card[] hand, Card[] onTable)
        {
            //TODO: random bet, Arsenij
            throw new NotImplementedException();
        }

        public override double? MakeBet(double cash, int playersOnTable, double callCost, Card[] onTable)
        {
            throw new NotImplementedException();
        }

        public override double GetSmallBet()
        {
            throw new NotImplementedException();
        }

        public override double GetBigBet()
        {
            throw new NotImplementedException();
        }
    }
}
