using PokerObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerPlayer
{
    public class Player : PlayerBase
    {
        public string Id;
        private readonly Random randomGenerator;

        public Player(string id, List<Card> cards, int cashe) 
            : base(cards, cashe)
        {
            Id = id;
            randomGenerator = new Random(0);
        }
        private bool InInterval(double from, double to, double val)
        {
            return from <= val && val < to;
        }
        private double GetBetValue(double minValue, double maxValue)
        {
            var randomNumber = randomGenerator.NextDouble();
            var value = minValue + randomNumber * (maxValue - minValue);
            return Math.Round(value, 1);
        }
        public double? MakeBet(List<PlayerInRoundState> players, int selfNumber, Card[] hand, Card[] onTable)
        {     
            double maxBetValue = players.Select(e => e.MadeBet.Value).Aggregate(Math.Max);
            double maxNewBet = players[selfNumber].RemainedCash;
            double minNewBet = Math.Min(players[selfNumber].RemainedCash, maxBetValue - players[selfNumber].MadeBet.Value);

            var randomNumber = randomGenerator.NextDouble();
            if (InInterval(0, 0.2, randomNumber))
                return null;    //Fold
            if (InInterval(0.2, 0.5, randomNumber))
                return GetBetValue(minNewBet, maxNewBet);   //Raise
            return minNewBet;   //Call
        }

        public override double? MakeBet(double cash, int playersOnTable, double callCost, Card[] onTable)
        {
            throw new NotImplementedException();
        }

        public override double MakeForceBlind()
        {
            throw new NotImplementedException();
        }

        public override double GetBigBlind()
        {
            throw new NotImplementedException();
        }
    }
}
