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
        private readonly Random randomGenerator;

        public Player(string id, double cashe) 
            : base(id, cashe)
        {
            randomGenerator = new Random(0);
            GetBet = MakeBet;
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
        public Bet MakeBet(List<PlayerInRoundState> players, int selfNumber, Card[] hand, Card[] onTable)
        {     
            double maxBetValue = players.Select(e => e.MadeBet.Value).Aggregate(Math.Max);
            double maxNewBet = players[selfNumber].RemainedCash;
            double minNewBet = Math.Min(players[selfNumber].RemainedCash, maxBetValue - players[selfNumber].MadeBet.Value);

            var randomNumber = randomGenerator.NextDouble();
            if (InInterval(0, 0.2, randomNumber))
                return null;    //Fold
            if (InInterval(0.2, 0.5, randomNumber))
                return new Bet(GetBetValue(minNewBet, maxNewBet), id);   //Raise
            return new Bet(minNewBet, id);   //Call
        }

        public override Bet MakeBet(double cash, int playersOnTable, double callCost, Card[] onTable)
        {
            return new Bet(2, id);
        }
    }
}
