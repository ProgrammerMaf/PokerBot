using System;
using System.Collections.Generic;
using System.Linq;
using CardDeck;
using PokerObjects;
using PokerPlayer;
using CombinationsComparer;

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
        public List<PlayerBase> Players;
        public int PlayersCount;
        public int Dealer;
        public Round Round;
        public double pot;

        private readonly int countSmallBlind;
        private readonly int countAnte;
        public List<Card> Deck;

        public SingleRound(
            List<double?> bets, List<PlayerBase> players,
            List<Card> deck,
            int dealer, Round round, 
            int countSmallBlind, int countAnte)
        {
            Bets = bets;
            Players = players;
            PlayersCount = players.Count;
            Dealer = dealer;
            Round = round;

            this.countSmallBlind = countSmallBlind;
            this.countAnte = countAnte;
            this.Deck = deck;

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
            var playersWin = Players.ToArray();
            for (var i = 0; i < playersWin.Length; i++)
                for (var j = 0; j < playersWin.Length; j++)
                {
                    if (CombinationsComparer.CombinationsComparer.CompareCombinations(
                        Players[i].GetSelfCards(), Players[j].GetSelfCards(), Deck.ToArray()) < 0)
                    {
                        var t = Players[i];
                        Players[i] = Players[j];
                        Players[j] = t;
                    }
                }

            var winer = new List<PlayerBase>();
            winer.Add(playersWin.First());
            var winers = playersWin.Where(e => 
                CombinationsComparer
                .CombinationsComparer.CompareCombinations(winer.First().GetSelfCards(), e.GetSelfCards(), Deck.ToArray()) == 0);
            foreach (var playerWin in winers)
                playerWin.AddCashe(pot/winers.Count());
            
        }

        public SingleRound PlayRound()
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
                            .GetBet(
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
                MakeBets();
                Round = Round.Turn;
            }

            if (Round == Round.Turn)
            {
                MakeBets();
                Round = Round.River;
            }

            if (Round == Round.River)
            {
                MakeBets();
                Round = Round.Finish;
            }

            if (Round == Round.Finish)
                throw new Exception("Попытка сыграть раунд, когда игра была закончена.");
 
            return this;
        }

        private void MakeBets()
        {
            var betsRound = new List<double?>();
            double? maxBets = 0;
            bool isReapeat;
            do
            {
                isReapeat = false;
                for (var i = 1; i <= PlayersCount; i++)
                {
                    var bet = Players.GetShift(Dealer + i)
                        .GetBet(
                            Bets.Sum(e => e ?? 0), PlayersCount, (double) maxBets, null);

                    if (bet == null)
                    {
                        Players.Remove(Players.GetShift(Dealer + i));
                        PlayersCount--;
                    }

                    if (bet > maxBets)
                    {
                        maxBets = bet;
                        isReapeat = true;
                    }
                    Bets.Add(bet);
                    betsRound.Add(bet);
                }
            } while (isReapeat); 

            pot += Bets.Select(e => e ?? 0).Sum();
            Bets = new List<double?>();
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
