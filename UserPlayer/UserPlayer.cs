using System;
using System.Collections.Generic;
using PokerObjects;
using PokerPlayer;

namespace UserPlayer
{
    public class UserPlayer : IPlayer
    {
        public Bet GetBigBlind()
        {
            throw new NotImplementedException();
        }

        public Bet GetSmallBlind()
        {
            throw new NotImplementedException();
        }

        public Bet MakeBet(double cash, int playersOnTable, double callCost, Card[] onTable)
        {
            throw new NotImplementedException();
        }
    }
}
