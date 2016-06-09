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
        public Bet OfferBet(GameState state, double moneyToCall, Card[] hand, Card[] onTable)
        {
            var aggressiveness = databaseUnit.GetAgressiveness(state.RemainedPlayersState);
            var odds = probabilityUnit.GetOdds(hand, onTable, state.RemainedPlayersState.Count);
            

            return null;
        }
    }
}
