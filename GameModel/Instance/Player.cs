using GameModel.Definition;
using System.Collections.Generic;

namespace GameModel.Instance
{
    public class Player : IDefinitionInstance<PlayerDefinition>
    {
        public Player(PlayerDefinition definition)
        {
            Definition = definition;
        }

        public PlayerDefinition Definition { get; }
        public List<Piece> Pieces { get; } = new List<Piece>();
    }
}
