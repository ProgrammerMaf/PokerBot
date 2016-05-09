using PokerObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerPlayer
{
    public class Player : IPlayer
    {
        public string Id;
        public Player(string id)
        {
            Id = id;
        }

        public double? MakeBet(double cash, int playersOnTable, double callCost, Card[] hand, Card[] onTable)
        {
            throw new NotImplementedException();
        }
    }
}
