using PokerObjects;

namespace AssistanceBot
{
    public interface IProbabilityUnit
    {
        AssistanceInfo GetOdds(Card[] hand, Card[] onTable, int count);
    }
    
}