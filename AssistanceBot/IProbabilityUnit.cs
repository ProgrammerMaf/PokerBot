using PokerObjects;

namespace AssistanceBot
{
    public interface IProbabilityUnit
    {
        AssistanceInfo GetOdds(Card[] hand, Card[] onTable, GameState state, double moneyToCall, string playerId);
    }
    
}