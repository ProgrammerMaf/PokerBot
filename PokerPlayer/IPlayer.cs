using PokerObjects;

namespace PokerPlayer
{
    public interface IPlayer
    {
        Bet MakeBet(double cash, int playersOnTable, double callCost, Card[] onTable);

        Bet GetSmallBlind();
        Bet GetBigBlind();

    }
}
