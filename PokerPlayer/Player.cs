﻿using PokerObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerPlayer
{
    public class Player : IPlayer
    {
        public string Id;
        public Player(string id)
        {
            Id = id;
        }
        public double? MakeBet(List<PlayerInRoundState> players, int selfNumber, Card[] hand, Card[] onTable)
        {
            //TODO: random bet, Arsenij
            throw new NotImplementedException();
        }
    }
}
