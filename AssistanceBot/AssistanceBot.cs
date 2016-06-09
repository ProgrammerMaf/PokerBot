using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokerObjects;

namespace AssistanceBot
{
    public class AssistanceBot : IAssistanceBot
    {
        public const double AggressivenessNoteCoeffitient = 0.1;
        private readonly IDatabaseUnit databaseUnit;
        private readonly IProbabilityUnit probabilityUnit;

        public AssistanceBot(IDatabaseUnit databaseUnit, IProbabilityUnit probabilityUnit)
        {
            this.databaseUnit = databaseUnit;
            this.probabilityUnit = probabilityUnit;
        }
        public Bet OfferBet(string playerId, GameState state, double moneyToCall, Card[] hand, Card[] onTable)
        {
            var aggressiveness = databaseUnit.GetAgressiveness(state.RemainedPlayersState);
            var odds = probabilityUnit.GetOdds(hand, onTable, state, moneyToCall, playerId);
            var clarifiedHandOdds = odds.DiscountedOdds * (1 + AggressivenessNoteCoeffitient * (1 - aggressiveness));
            return clarifiedHandOdds <= odds.PotOdds ? new Bet(moneyToCall, playerId) : null;
        }
    }
}
