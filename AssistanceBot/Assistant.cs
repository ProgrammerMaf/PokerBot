using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokerObjects;

namespace AssistanceBot
{
    public class Assistant : IAssistant
    {
        public const double AggressivenessNoteCoeffitient = 0.1;
        private readonly IProbabilityUnit probabilityUnit;

        public Assistant(IProbabilityUnit probabilityUnit)
        {
            this.probabilityUnit = probabilityUnit;
        }
        public Bet OfferBet(string playerId, GameState state, double moneyToCall, Card[] hand, Card[] onTable)
        {
            var odds = probabilityUnit.GetOdds(hand, onTable, state, moneyToCall, playerId);
            return odds.DiscountedOdds <= odds.PotOdds ? new Bet(moneyToCall, playerId) : null;
        }
    }
}
