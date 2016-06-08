namespace PokerObjects
{
    public class PlayerInRoundState
    {
        public readonly string Id;
        public readonly double RemainedCash;
        public readonly Bet MadeBet;
        public PlayerInRoundState(string id, double remainedCash, Bet bet)
        {
            Id = id;
            RemainedCash = remainedCash;
            MadeBet = bet;
        }
    }
}
