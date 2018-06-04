using GameModel.Definition;
using RulesParser;

namespace GameParser.Builders
{
    public class BoardDefinitionBuilder : IBuilder<BoardDefinition>
    {
        public bool CanBuild()
        {
            return true;
        }

        public BoardDefinition Build()
        {
            // TODO
            return new BoardDefinition(new CellDefinition[] { });
        }
    }
}
