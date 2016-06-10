using System;
using System.Collections.Generic;
using System.Linq;
using CombinationsComparer;
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
        public List<Bet> Bets;
        public List<PlayerBase> Players;
        public int PlayersCount;
        public readonly int Dealer;
        public readonly Round Round;
        public double Pot;

        private readonly int countSmallBlind;
        private readonly int countAnte;
        public List<Card> Deck;
        public List<Card> DeckOnTable;

        private ICombinationsComparer comparer;


        public SingleRound(
            List<PlayerBase> players,
            List<Card> deck,
            int countSmallBlind, int countAnte,
            ICombinationsComparer comparer)
        {

            Bets = new List<Bet>();
            Players = players;
            PlayersCount = players.Count;
            Dealer = 0;
            Round = Round.Preflop;

            this.countSmallBlind = countSmallBlind;
            this.countAnte = countAnte;
            Deck = deck;
            DeckOnTable = new List<Card>();

            this.comparer = comparer;

            foreach (var player in Players)
                AddCardsPlayer(player);
           

            Pot = 0;
            if (players.Count < 2)
                throw new Exception("Не достаточное количество игроков.");
        }

        public SingleRound(
            List<Bet> bets, List<PlayerBase> players,
            List<Card> deck,
            List<Card> deckOnTable, 
            double pot,
            int dealer, Round round, 
            int countSmallBlind, int countAnte,
            ICombinationsComparer comparer)
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

            this.comparer = comparer;

            Pot = pot;
            if (players.Count < 2 && round != Round.Finish)
                throw new Exception("Не достаточное количество игроков.");
        }

        private Card GetCardFormDeck()
        {
            var rand = new Random(8);

            var indexCards = rand.Next(Deck.Count);
            var card = Deck[indexCards];
            Deck.RemoveAt(indexCards);

            return card;
        }

        private void AddCardsPlayer(PlayerBase player)
        {
            var cards = new List<Card>();
            for (int i = 0; i < 2; i++)
                cards.Add(GetCardFormDeck());
            player.GetSelfCards = () => cards.ToArray();

        }
        public IEnumerable<PlayerBase> SelectWinners()
        {
            if (Round != Round.Finish)
                throw new Exception("Попытка определить победителя, когда игра еще не была закончена.");
            var playersWin = Players.Take(1).ToArray();
            for (var i = 0; i < playersWin.Length; i++)
                for (var j = 0; j < playersWin.Length; j++)
                {
                    if (comparer.CompareCombinations(
                        Players[i].GetSelfCards(), Players[j].GetSelfCards(), Deck.ToArray()) < 0)
                    {
                        var t = Players[i];
                        Players[i] = Players[j];
                        Players[j] = t;
                    }
                }

            var winer = new List<PlayerBase> {playersWin.First()};
            var winers = playersWin.Where(e =>
                comparer.CompareCombinations(winer.First().GetSelfCards(), e.GetSelfCards(), Deck.ToArray()) == 0);
            foreach (var playerWin in winers)
                playerWin.AddCashe(Pot/winers.Count());

            return playersWin;

        }

        public SingleRound PlayRound()
        {
            if (Round == Round.Finish)
                throw new Exception("Попытка сыграть раунд, когда игра была закончена.");

            Round nRound = Round.Finish;
            if (Round == Round.Preflop)
            {
                foreach (var player in Players)
                    Bets.Add(player.MakeForceBlind(countAnte));
                
                Bets.Add(Players.GetShift(Dealer + 1).MakeForceBlind(countSmallBlind));
                Bets.Add(Players.GetShift(Dealer + 2).MakeForceBlind(countSmallBlind*2));
                MakeBets(2);

                DeckOnTable.Add(GetCardFormDeck());
                DeckOnTable.Add(GetCardFormDeck());
                DeckOnTable.Add(GetCardFormDeck());
                return 
                    new SingleRound(new List<Bet>(), 
                        Players, Deck, DeckOnTable, Pot, Dealer + 1, Round.Flop, countSmallBlind, countAnte, comparer);
            }

            if (Round == Round.Flop)
            {
                DeckOnTable.Add(GetCardFormDeck());
                nRound = Round.Turn;
            }


            if (Round == Round.Turn)
            {
                DeckOnTable.Add(GetCardFormDeck());
                nRound = Round.River;
            }


            if (Round == Round.River)
                nRound = Round.Finish;

            MakeBets();
            GetCardFormDeck();

            if (Players.Count <= 1)
                nRound = Round.Finish;

            return
                new SingleRound(new List<Bet>(),
                    Players, Deck, DeckOnTable, Pot, Dealer + 1, nRound, countSmallBlind, countAnte, comparer);
        }

        private void MakeBets(int shift = 0)
        {
            var betsRound = new List<Bet>();
            double? maxBets = 0;
            bool isReapeat;
            do
            {
                isReapeat = false;
                for (var i = shift + 1; i <= PlayersCount; i++)
                {
                    var bet = Players.GetShift(Dealer + i)
                        .GetBet(
                            Pot, PlayersCount, (double) maxBets, DeckOnTable.ToArray());

                    if (bet == null)
                    {
                        Players.Remove(Players.GetShift(Dealer + i));
                        PlayersCount--;
                    }

                    if (bet.Value > maxBets.Value)
                    {
                        maxBets = bet.Value;
                        isReapeat = true;
                    }
                    Bets.Add(bet);
                    betsRound.Add(bet);
                }
            } while (isReapeat); 

            Pot += Bets.Select(e => e.Value).Sum();
            Bets = new List<Bet>();
        }
    }

    public static class ListExtension
    {
        public static T GetShift<T>(this IEnumerable<T> list, int index)
        {
            var count = list.Count();
            if (index >= count)
            {
                var newIndex = index - (index/count)*count;
                return list.ElementAt(newIndex);
            }
            return list.ElementAt(index);
        }
    }

}
