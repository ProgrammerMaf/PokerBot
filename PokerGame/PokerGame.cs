using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using PokerObjects;
using PokerPlayer;
using SingleRoung;

namespace PokerGame
{
    public class PokerGame
    {
        private readonly List<PlayerBase> players;

        public PokerGame(List<PlayerBase> players)
        {
            this.players = players;
        }

        public static void Main()
        {
            var kernel = new StandardKernel();
            var id = 0;

            kernel.Bind<PlayerBase>()
                .To<UserPlayer.UserPlayer>()
                .WithConstructorArgument("id",context => id++)
                .WithConstructorArgument("cashe", context => 400.0);

            kernel.Bind<PlayerBase>()
                .To<Player>()
                .WithConstructorArgument("id", context => id++)
                .WithConstructorArgument("cashe", context => 300.0);

            var program = kernel
                .Get<PokerGame>();
            program
                .Run();
            
        }

        public void Run()
        {
            var cards = Card.GetAllCards().ToList();

            var game =
                new SingleRound(players, cards, 2, 2);

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
                Console.Write("С комбинацией: ");
                foreach(var c in game.DeckOnTable.Concat(winner.GetSelfCards()))
                    Console.Write($"{c} ");
                Console.WriteLine();
            }


            Console.ReadLine();
        }
    }
}
