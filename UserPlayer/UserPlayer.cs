using PokerPlayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokerObjects;

namespace UserPlayer
{
    public class UserPlayer : IPlayer
    {
        public double? MakeBet(List<PlayerInRoundState> players, int selfNumber, Card[] hand, Card[] onTable)
        {
            throw new NotImplementedException();
            //TODO: Arsenij
        }

        public double? MakeBet(double cash, int playersOnTable, double callCost, Card[] onTable)
        {
            throw new NotImplementedException();
        }

        public double MakeForceBlind()
        {
            throw new NotImplementedException();
        }

        public double GetBigBlind()
        {
            throw new NotImplementedException();
        }
    }
}
