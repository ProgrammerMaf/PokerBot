namespace PokerObjects
{
    public class Bet
    {
        public readonly double Value;
        public readonly string BettorId;

        public Bet(double value, string bettorId)
        {
            Value = value;
            BettorId = bettorId;
        }
    }
}