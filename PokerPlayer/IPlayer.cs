using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokerObjects;

namespace PokerPlayer
{
    public interface IPlayer
    {
        Bet MakeBet(double cash, int playersOnTable, double callCost, Card[] onTable);

        Bet MakeForceBlind(double count);

        void AddCashe(double count);

    }
}
