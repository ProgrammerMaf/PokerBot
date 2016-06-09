using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokerObjects;
using PokerPlayer;
using SingleRoung;

namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var id = 0;
            var firstPlayer = new Player(++id, new List<Card>{ new Card(Suit.Club, CardRank.Ace), new Card(Suit.Diamond, CardRank.Ace) }, 300);
            var secondPlayer = new Player(++id, new List<Card> { new Card(Suit.Spade, CardRank.Ace), new Card(Suit.Heart, CardRank.Ace) }, 200);

            firstPlayer.GetBet = (_, __, ___, ____) =>
            {
                Console.WriteLine("1) Сколько поставить?");
                return double.Parse(Console.ReadLine());
            };

            secondPlayer.GetBet = (_, __, ___, ____) =>
            {
                Console.WriteLine("2) Сколько поставить?");
                return double.Parse(Console.ReadLine());
            };

            var game = new SingleRound(new List<double?>(), new List<PlayerBase> { firstPlayer, secondPlayer}, new List<Card>(), 0, Round.Preflop, 2, 2);

            while (game.Round != Round.Finish)
            {
                Console.WriteLine($"банк: {game.pot}");
                Console.WriteLine("Карты на столе: ");
                foreach (var card in game.Deck)
                    Console.Write($"{card} ");
                Console.WriteLine(".");
                game.PlayRound();
            }
        }
    }
}
