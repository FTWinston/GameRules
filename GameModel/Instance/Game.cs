using GameModel.Definition;
using System.Collections.Generic;

namespace GameModel.Instance
{
    public class Game : IDefinitionInstance<GameDefinition>
    {
        public Game(GameDefinition definition)
        {
            Definition = definition;
        }

        public GameDefinition Definition { get; }

        public List<Player> Players { get; } = new List<Player>();
        public List<BoardDefinition> Boards { get; } = new List<BoardDefinition>();
        public List<Piece> Pieces { get; } = new List<Piece>();
    }
}
