using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardDeck;
using PokerPlayer;
using PokerObjects;

namespace SingleRoung
{
    public enum Round
    {
        Preflop,
        Flop,
        Turn,
        River
    }
    public class SingleRound
    {
        public List<double?> Bets;
        public List<IPlayer> Players;
        public int PlayersCount;
        public int Dealer;
        public Round Round;

        private int whoNext;

        public SingleRound(List<double?> bets, List<IPlayer> players, int playersCount, int dealer, Round round)
        {
            Bets = bets;
            Players = players;
            PlayersCount = playersCount;
            Dealer = dealer;
            Round = round;
        }
        private void CollectBets()
        {
            throw new NotImplementedException();
        }
        private void AddCards(int cardsCount)
        {

        }
        private void SelectWinners()
        {

        }
        public SingleRound PlayRound(ICardDeck deck)
        {
            if (Round == Round.Preflop)
            {
                Bets.Add(Players.GetShift(Dealer + 1).GetSmallBet());
                Bets.Add(Players.GetShift(Dealer + 2).GetBigBet());
                if (PlayersCount > 2)
                {
                    for(var i = 3; i < PlayersCount - 2; i++)
                        Bets.Add(
                            Players.GetShift(Dealer + i)
                            .MakeBet(
                                Bets.Sum(e => e ?? 0), 
                                PlayersCount, 
                                Bets.Max(e => e ?? 0), 
                                null));
                }
                Round = Round.Flop;
            }
            if (Round == Round.Flop)
            {
                var betsRound = new List<double?>();
                for (var i = 1; i <= PlayersCount; i++)
                {
                    var bet = Players.GetShift(Dealer + i)
                        .MakeBet(Bets.Sum(e => e ?? 0), PlayersCount, betsRound.Max(e => e ?? 0), null);
                    Bets.Add(bet);
                    betsRound.Add(bet);
                }

                Round = Round.Turn;
            }
            if (Round == Round.Turn)
            {
                var betsRound = new List<double?>();
                for (var i = 1; i <= PlayersCount; i++)
                {
                    var bet = Players.GetShift(Dealer + i)
                        .MakeBet(
                        Bets.Sum(e => e ?? 0), PlayersCount, betsRound.Max(e => e ?? 0), null);
                    Bets.Add(bet);
                    betsRound.Add(bet);
                }

                Round = Round.River;
            }

            if (Round == Round.River)
            {
                var betsRound = new List<double?>();
                for (var i = 1; i <= PlayersCount; i++)
                {
                    var bet = Players.GetShift(Dealer + i)
                        .MakeBet(
                        Bets.Sum(e => e ?? 0), PlayersCount, betsRound.Max(e => e ?? 0), null);
                    Bets.Add(bet);
                    betsRound.Add(bet);
                }
            }
            return this;
        }
    }

    public static class ListExtension
    {
        public static T GetShift<T>(this IEnumerable<T> list, int index)
        {
            var count = list.Count();
            if (index > count)
            {
                var newIndex = index - (index/count)*count;
                return list.ElementAt(newIndex);
            }
            return list.ElementAt(index);
        }
    }
        
}
