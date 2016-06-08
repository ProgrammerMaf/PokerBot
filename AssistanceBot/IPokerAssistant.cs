using PokerObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerAssistant
{
    public interface IPokerAssistant
    {
        Bet OfferBet(double cash, int playersOnTable, double callCost, Card[] hand, Card[] onTable);
    }
}
