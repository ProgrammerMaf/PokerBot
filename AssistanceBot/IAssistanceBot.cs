using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokerObjects;

namespace AssistanceBot
{
    public interface IAssistanceBot
    {
        Bet OfferBet(GameState state, double moneyToCall, Card[] hand, Card[] onTable);
    }
}
