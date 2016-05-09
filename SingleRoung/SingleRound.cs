using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardDeck;
using PokerPlayer;

namespace SingleRoung
{
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
}
