using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CombinationsComparer;
using Ninject;
using PokerObjects;
using PokerPlayer;
using SingleRoung;
using CombinationsComparer;
using UserPlayer;
using CombinationsComparer;
using UserPlayer;

namespace PokerGame
{
    public class PokerGame
    {
        private readonly List<PlayerBase> players;
        private SingleRound game;

        public PokerGame(List<PlayerBase> players, SingleRound game)
        {
            this.players = players;
            this.game = game;
        }

        public static void Main()
        {
            var game =
                new RoundBuilder()
                    .SetSmallAnte(5)
                    .SetSmallBlind(10)
                    .AddPlayer(new Player("Petya", 300))
                    .AddPlayer(new Player("Vasya", 300))
                    .GiveCardsToPlayer(new[]
                    {
                        new Card(Suit.Club, CardRank.Ace),
                        new Card(Suit.Spade, CardRank.Queen)
                    })
                    .GiveCardsToPlayer(new[]
                    {
                        new Card(Suit.Diamond, CardRank.Jack),
                        new Card(Suit.Club, CardRank.Nine)
                    })
                    .WithComparer(new CombinationsComparer.CombinationsComparer())
                    .CreateGame();



            game.PlayRound();
            var kernel = new StandardKernel();
            var id = 0;
            var rand = new Random();

            kernel.Bind<PlayerBase>()
                .To<UserPlayer.UserPlayer>()
                .WithConstructorArgument("id",context => "Petya")
                .WithConstructorArgument("cashe", context => 400.0);

            kernel.Bind<PlayerBase>()
                .To<Player>()
                .WithConstructorArgument("id", context => "Vasya")
                .WithConstructorArgument("cashe", context => 300.0);

            kernel
                .Bind<SingleRound>()
                .To<SingleRound>()
                .WithConstructorArgument("deck", 
                context => Card.GetAllCards().ToList().OrderBy(e => rand.Next()).ToList())
                .WithConstructorArgument("countSmallBlind", context => 2)
                .WithConstructorArgument("countAnte", context => 2);

            kernel
                .Bind<ICombinationsComparer>()
                .To<CombinationsComparer.CombinationsComparer>();

            kernel
                .Get<PokerGame>().Run();
            
        }

        public void Run()
        {

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
