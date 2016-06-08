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
        private IDatabaseUnit databaseUnit;
        private IProbabilityUnit probabilityUnit;

        public AssistanceBot(IDatabaseUnit databaseUnit, IProbabilityUnit probabilityUnit)
        {
            this.databaseUnit = databaseUnit;
            this.probabilityUnit = probabilityUnit;
        }
        public Bet OfferBet(GameState state, double moneyToCall, Card[] hand, Card[] onTable)
        {
            var opponentsAggressiveness = databaseUnit.GetAgressiveness(state.RemainedPlayersState);
            var opponentsLuckiness = databaseUnit.GetLuckiness(state.RemainedPlayersState);
            var winningProbability = probabilityUnit.GetWinProbability(hand, onTable, state.RemainedPlayersState.Count);
            bool wantToPlay = winningProbability >= (1 - opponentsLuckiness) * opponentsAggressiveness / 2;
            return null;
        }
    }
}
