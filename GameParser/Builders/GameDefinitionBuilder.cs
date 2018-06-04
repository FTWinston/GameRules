using GameModel.Definition;
using RulesParser;
using System.Collections.Generic;

namespace GameParser.Builders
{
    public class GameDefinitionBuilder : IBuilder<GameDefinition>
    {
        public PlayerDefinitionsBuilder Players { get; } = new PlayerDefinitionsBuilder();
        /*
        private Dictionary<string, BoardDefinition> BoardsByName = new Dictionary<string, BoardDefinition>();
        private Dictionary<string, CellDefinition> CellsByName = new Dictionary<string, CellDefinition>();
        private Dictionary<string, PieceDefinition> PiecesByName = new Dictionary<string, PieceDefinition>();
        */

        public bool CanBuild()
        {
            return Players.CanBuild();
        }

        public GameDefinition Build()
        {
            var definition = new GameDefinition(Players.Build());

            return definition;
        }
    }
}
