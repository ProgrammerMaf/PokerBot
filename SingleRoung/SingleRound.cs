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
        River,
        Finish
    }
    public class SingleRound
    {
        public List<double?> Bets;
        public List<IPlayer> Players;
        public int PlayersCount;
        public int Dealer;
        public Round Round;
        public double pot;

        private readonly int countSmallBlind;
        private readonly int countAnte;

        public SingleRound(List<double?> bets, List<IPlayer> players, int dealer, Round round, 
            int countSmallBlind, int countAnte)
        {
            Bets = bets;
            Players = players;
            PlayersCount = players.Count;
            Dealer = dealer;
            Round = round;

            this.countSmallBlind = countSmallBlind;
            this.countAnte = countAnte;

            pot = 0;
            if (players.Count < 2)
                throw new Exception("Не достаточное количество игроков.");
            if (dealer < 0 || dealer >= players.Count)
                throw new Exception($"Невозможно назначить дилера с номером {dealer}.");
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
            if (Round != Round.Finish)
                throw new Exception("Попытка определить победителя, когда игра еще не была закончена.");
        }
        public SingleRound PlayRound(ICardDeck deck)
        {
            if (Round == Round.Preflop)
            {
                foreach (var player in Players)
                    Bets.Add(player.MakeForceBlind(countAnte));
                
                Bets.Add(Players.GetShift(Dealer + 1).MakeForceBlind(countSmallBlind));
                Bets.Add(Players.GetShift(Dealer + 2).MakeForceBlind(countSmallBlind*2));
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
                pot += Bets.Select(e => e ?? 0).Sum();
                Bets = new List<double?>();


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

                pot += Bets.Select(e => e ?? 0).Sum();
                Bets = new List<double?>();

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

                pot += Bets.Select(e => e ?? 0).Sum();
                Bets = new List<double?>();

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

                pot += Bets.Select(e => e ?? 0).Sum();
                Bets = new List<double?>();

                Round = Round.Finish;
            }

            if (Round == Round.Finish)
                throw new Exception("Попытка сыграть раунд, когда игра была закончена.");
 
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
