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
        public List<RandomPlayer> Winners;
        public int PlayersCount;
        public RandomPlayer Dealer;
        public SingleRound()
        {
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
        public void PlayRound(ICardDeck deck, IReadOnlyDictionary<RandomPlayer, double> players)
        {
            CollectBets();
            AddCards(3);
            CollectBets();
            AddCards(1);
            CollectBets();
            AddCards(1);
            CollectBets();
            SelectWinners();
        }
    }
}
