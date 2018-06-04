using GameModel.Definition;

namespace GameModel.Instance
{
    public class Piece : IDefinitionInstance<PieceDefinition>
    {
        public Piece(PieceDefinition definition, Player owner, CellDefinition location)
        {
            Definition = definition;
            Owner = owner;
            Location = location;
        }

        public PieceDefinition Definition { get; set; }
        public Player Owner { get; set; }
        public CellDefinition Location { get; set; }
    }
}
