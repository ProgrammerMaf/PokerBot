using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokerObjects;

namespace PokerPlayer
{
    public interface IPlayer
    {
        double? MakeBet(List<PlayerInRoundState> players, int selfNumber, Card[] hand, Card[] onTable);
    }
}
