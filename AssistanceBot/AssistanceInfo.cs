namespace AssistanceBot
{
    public class AssistanceInfo
    {
        public readonly double HandOdds;
        public readonly double DiscountedOdds;
        public readonly double PotOdds;

        public AssistanceInfo(double potOdds, double discountedOdds, double handOdds)
        {
            PotOdds = potOdds;
            DiscountedOdds = discountedOdds;
            HandOdds = handOdds;
        }
    }
}