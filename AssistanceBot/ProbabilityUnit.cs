using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokerObjects;
using SingleRoung;

namespace AssistanceBot
{
    public class ProbabilityUnit : IProbabilityUnit
    {
        private AssistanceInfo GetOddsOnPreflop(Card[] hand, Card[] onTable, GameState state, double moneyToCall,
            string playerId)
        {
            return new AssistanceInfo(5.1, 4.2, 4);
        }
        private AssistanceInfo GetOddsForOneCard(Card[] hand, Card[] onTable, GameState state, double moneyToCall,
            string playerId)
        {
            return new AssistanceInfo(5.1, 4.2, 4);
        }
        public AssistanceInfo GetOdds(Card[] hand, Card[] onTable, GameState state, double moneyToCall, string playerId)
        {
            return 
                state.Street == Round.Preflop ? 
                GetOddsOnPreflop(hand, onTable, state, moneyToCall, playerId) : 
                GetOddsForOneCard(hand, onTable, state, moneyToCall, playerId);
        }
    }
}
