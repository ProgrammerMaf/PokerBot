using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokerObjects;

namespace AssistanceBot
{
    public class GameState
    {
        public readonly List<PlayerInRoundState> RemainedPlayersState;
        public readonly double Pot;
        public GameState(List<PlayerInRoundState> remainedPlayersState, double pot)
        {
            RemainedPlayersState = remainedPlayersState;
            Pot = pot;
        }
    }
}
