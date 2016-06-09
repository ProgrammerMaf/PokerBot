using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokerObjects;
using SingleRoung;

namespace AssistanceBot
{
    public class GameState
    {
        public readonly List<PlayerInRoundState> RemainedPlayersState;
        public readonly double Pot;
        public readonly Round Street;
        public GameState(List<PlayerInRoundState> remainedPlayersState, double pot, Round street)
        {
            RemainedPlayersState = remainedPlayersState;
            Pot = pot;
            Street = street;
        }
    }
}
