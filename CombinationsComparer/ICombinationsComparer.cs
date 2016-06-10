using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokerObjects;

namespace CombinationsComparer
{
    public interface ICombinationsComparer
    {
        int CompareCombinations(Card[] firstPlayerHand, Card[] secondPlayerHand, Card[] onTable);


    }
}
