using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokerObjects;
using PokerPlayer;
using SingleRoung;
using UserPlayer;

namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var cards = Card.GetAllCards().ToList();

            var rand = new Random(DateTime.Now.DayOfYear);

            var indexCards = rand.Next(cards.Count());
            


            var id = 0;
            var firstPlayer = new UserPlayer.UserPlayer(++id, 300);

            var secondPlayer = new Player(++id, 200) {GetBet = (_, __, ___, ____) => 10};


            var game = 
                new SingleRound(new List<PlayerBase> { firstPlayer, secondPlayer}, cards, 2, 2);

            while (game.Round != Round.Finish)
            {
                Console.WriteLine($"банк: {game.Pot}");
                Console.WriteLine("Карты на столе: ");
                foreach (var card in game.DeckOnTable)
                    Console.Write($"{card} ");
                Console.WriteLine(".");
                game = game.PlayRound();
            }

            Console.WriteLine("Выиграли: ");
            foreach (var winner in game.SelectWinners())
            {
                Console.WriteLine(winner);
            }

            Console.ReadLine();
        }
    }

    public static class ListExtension
    {
        public static T GetAndRemove<T>(this List<T> list, int index)
        {
            var e = list[index];
            list.RemoveAt(index);
            return e;
        }
    }
}
