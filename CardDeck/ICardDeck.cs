using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokerObjects;

namespace CardDeck
{
    public interface ICardDeck
    {
        Card GiveNextCard();
    }
}
