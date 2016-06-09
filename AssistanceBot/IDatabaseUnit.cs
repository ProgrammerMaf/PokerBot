using System.Collections.Generic;
using PokerObjects;

namespace AssistanceBot
{
    public interface IDatabaseUnit
    {
        double GetAgressiveness(List<PlayerInRoundState> remainedPlayersState);
    }
}