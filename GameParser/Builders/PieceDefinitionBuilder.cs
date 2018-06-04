using GameModel.Definition;
using RulesParser;

namespace GameParser.Builders
{
    public class PieceDefinitionBuilder : IBuilder<PieceDefinition>
    {
        public string Name { get; private set; }

        public PieceDefinition Build()
        {
            // TODO
            return new PieceDefinition(Name);
        }

        public bool CanBuild()
        {
            return Name != null;
        }

        public PieceDefinitionBuilder SetName(string name)
        {
            Name = name;
            return this;
        }
    }
}
