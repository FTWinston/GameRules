using System.Collections.Generic;
using System.Linq;

namespace GameModel.Definition
{
    public class GameDefinition
    {
        public GameDefinition(PlayerDefinition[] players)
        {
            Players = players;
        }

        public PlayerDefinition[] Players { get; }
    }
}
