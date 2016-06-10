using PokerPlayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokerObjects;

namespace UserPlayer
{
    public class UserPlayer : PlayerBase
    {

        public override Bet MakeBet(double cash, int playersOnTable, double callCost, Card[] onTable)
        {
            Console.WriteLine();
            Console.WriteLine("Ваш ход:");
            Console.WriteLine($"Игроков за столом: {playersOnTable}");
            Console.WriteLine($"Денег в банке: {cash}");
            Console.Write("Карты на столе: ");
            foreach (var card in onTable)
                Console.Write($"{card} ");
            Console.WriteLine();
            Console.WriteLine("Карты на руке: ");
            foreach (var card in GetSelfCards())
                Console.Write($"{card} ");
            Console.WriteLine();
            Console.WriteLine("Введите вашу ставку (n - для паса): ");
            var bet = Console.ReadLine();
            if (bet == "n")
                return null;
            var betC = double.Parse(bet);
            cashe -= betC;
            return new Bet(betC, id);
        }

        public UserPlayer(string id, double cashe) 
            : base(id, cashe)
        {
            Console.WriteLine("Игрок создан.");
            GetBet = MakeBet;
        }
    }
}
