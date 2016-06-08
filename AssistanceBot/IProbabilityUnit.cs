using PokerObjects;

namespace AssistanceBot
{
    public interface IProbabilityUnit
    {
        double GetWinProbability(Card[] hand, Card[] onTable, int count);
    }
}