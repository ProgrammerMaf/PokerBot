using System;
using System.Collections.Generic;
using System.Linq;
using PokerObjects;
using PokerPlayer;

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
        public readonly int Dealer;
        public readonly Round Round;
        public double Pot;

        private readonly int countSmallBlind;
        private readonly int countAnte;
        public List<Card> Deck;
        public List<Card> DeckOnTable;


        public SingleRound(
            List<PlayerBase> players,
            List<Card> deck,
            int countSmallBlind, int countAnte)
        {

            Bets = new List<double?>();
            Players = players;
            PlayersCount = players.Count;
            Dealer = 0;
            Round = Round.Preflop;

            this.countSmallBlind = countSmallBlind;
            this.countAnte = countAnte;
            Deck = deck;
            DeckOnTable = new List<Card>();

            AddCardFormDeck();
            AddCardFormDeck();

            Pot = 0;
            if (players.Count < 2)
                throw new Exception("Не достаточное количество игроков.");
        }

        public SingleRound(
            List<double?> bets, List<PlayerBase> players,
            List<Card> deck,
            List<Card> deckOnTable, 
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
            Deck = deck;
            DeckOnTable = deckOnTable;

            Pot = 0;
            if (players.Count < 2)
                throw new Exception("Не достаточное количество игроков.");
            if (dealer < 0 || dealer >= players.Count)
                throw new Exception($"Невозможно назначить дилера с номером {dealer}.");
        }

        private void AddCardFormDeck()
        {
            var rand = new Random(DateTime.Now.DayOfYear);

            var indexCards = rand.Next(Deck.Count);
            DeckOnTable.Add(Deck[indexCards]);
            Deck.RemoveAt(indexCards);
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

            var winer = new List<PlayerBase> {playersWin.First()};
            var winers = playersWin.Where(e => 
                CombinationsComparer
                .CombinationsComparer.CompareCombinations(winer.First().GetSelfCards(), e.GetSelfCards(), Deck.ToArray()) == 0);
            foreach (var playerWin in winers)
                playerWin.AddCashe(Pot/winers.Count());
            
        }

        public SingleRound PlayRound()
        {
            if (Round == Round.Preflop)
            {
                foreach (var player in Players)
                    Bets.Add(player.MakeForceBlind(countAnte));
                
                Bets.Add(Players.GetShift(Dealer + 1).MakeForceBlind(countSmallBlind));
                Bets.Add(Players.GetShift(Dealer + 2).MakeForceBlind(countSmallBlind*2));
                MakeBets(2);
                return 
                    new SingleRound(new List<double?>(), 
                        Players, Deck, DeckOnTable, Dealer + 1, Round.Flop, countSmallBlind, countAnte );
            }

            if (Round != Round.Preflop)
            {
                MakeBets();
                return
                    new SingleRound(new List<double?>(),
                        Players, Deck, DeckOnTable, Dealer + 1, Round.Turn, countSmallBlind, countAnte);
            }

            if (Round == Round.Turn)
            {
                MakeBets();
                return
                    new SingleRound(new List<double?>(),
                        Players, Deck, DeckOnTable, Dealer + 1, Round.River, countSmallBlind, countAnte);
            }

            if (Round == Round.River)
            {
                MakeBets();
                return
                    new SingleRound(new List<double?>(),
                        Players, Deck, DeckOnTable, Dealer + 1, Round.Finish, countSmallBlind, countAnte);
            }

            if (Round == Round.Finish)
                throw new Exception("Попытка сыграть раунд, когда игра была закончена.");
 
            return this;
        }

        private void MakeBets(int shift = 0)
        {
            var betsRound = new List<double?>();
            double? maxBets = 0;
            bool isReapeat;
            do
            {
                isReapeat = false;
                for (var i = shift + 1; i <= PlayersCount; i++)
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

            Pot += Bets.Select(e => e ?? 0).Sum();
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
