using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using PokerPlayer;

namespace PokerGame
{
    public class PokerGame
    {
        public static void Main()
        {
            var kernel = new StandardKernel();

            kernel.Bind<IPlayer>().To<UserPlayer.UserPlayer>();
            kernel.Bind<IPlayer>().To<Player>();
        }
    }
}
