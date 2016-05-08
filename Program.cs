using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot
{
    public class Card
    {
        public readonly char Suit;
        public readonly int Rank;

        public Card(char suit, int rank)
        {
            Suit = suit;
            Rank = rank;
        }
    }

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

    public interface IPlayer
    {
        double? MakeBet(double cash, int playersOnTable, double callCost, Card[] hand, Card[] onTable);
    }
    public class Player : IPlayer
    {
        public string Id;
        public Player(string id)
        {
            Id = id;
        }

        public double? MakeBet(double cash, int playersOnTable, double callCost, Card[] hand, Card[] onTable)
        { 
            throw new NotImplementedException();
        }
    }

    public interface ICardDeck
    {
        Card GiveNextCard();
    }

    public static class CombinationsComparer
    {
        public static int CompareCombinations(Card[] firstPlayerHand, Card[] secondPlayerHand, Card onTable)
        {
            //> 0, if first player wins
            //< 0, if second player wins
            //= 0, otherwise
            throw new NotImplementedException();
        }
    }

    public class SingleRound
    {
        public List<double?> Bets;
        public List<Player> Winners;
        public int PlayersCount;
        public Player Dealer;
        public SingleRound()
        {
            // Инициализировать типы, хранящие инфу о раунде.
            throw new NotImplementedException();
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
        public void PlayRound(ICardDeck deck)
        {
            throw new NotImplementedException();
            // Сбор ставок(опрос игроков), добавление карт на стол и определение победителя.
        }
    }

    public class Game
    {
        // запуск игры
    }

    public class SimplePokerBot : IPlayer
    {
        // бот, анализирующий лишь видимые карты.
        public double? MakeBet(double cash, int playersOnTable, double callCost, Card[] hand, Card[] onTable)
        {
            throw new NotImplementedException();
        }
    }

    //класс, реализующий в каком либо виде игру из консоли.

    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
