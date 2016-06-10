using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CombinationsComparer;
using PokerObjects;
using PokerPlayer;

namespace SingleRoung
{
    public class RoundBuilder
    {
        List<Card[]> AddCards;
        private List<Card> CardOnTable;
        private List<PlayerBase> players;
        SingleRound game;
        private ICombinationsComparer comparer;
        private int blind;
        private int ante;

        private RoundBuilder(SingleRound game)
        {
            this.game = game;
        }
        public RoundBuilder()
        {
            CardOnTable = new List<Card>();
            players = new List<PlayerBase>();
            AddCards = new List<Card[]>();
        }

        public RoundBuilder AddPlayer(PlayerBase player)
        {
            players.Add(player);
            return this;
        }
        public RoundBuilder GiveCardsToPlayer(Card[] cards)
        {
            AddCards.Add(cards);
            return this;
        }

        public RoundBuilder SetSmallBlind(int count)
        {
            blind = count;
            return this;
        }

        public RoundBuilder SetSmallAnte(int count)
        {
            ante = count;
            return this;
        }

        public RoundBuilder WithComparer(ICombinationsComparer comparer)
        {
            this.comparer = comparer;
            return this;
        }

        public RoundBuilder WithCardsOnTable(List<Card> cards)
        {
            CardOnTable = cards;
            return this;
        }
        public SingleRound CreateGame()
        {
            for (int i = 0; i < players.Count; i++)
                players[i].GetSelfCards = () => AddCards[i];
            
            return new SingleRound(null, players, Card.GetAllCards().ToList(), CardOnTable, 0, 0, Round.Preflop, blind, ante, comparer);
        }
    }
}
