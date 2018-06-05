using GameModel.Definition;
using System.Collections.Generic;
using System.Linq;

namespace GameParser.Builders
{
    public class GameDefinitionBuilder : IBuilder<GameDefinition>
    {
        public PlayerDefinitionsBuilder Players { get; } = new PlayerDefinitionsBuilder();

        private Dictionary<string, BoardDefinitionBuilder> Boards { get; } = new Dictionary<string, BoardDefinitionBuilder>();

        public BoardDefinitionBuilder GetBoard(string name)
        {
            if (Boards.TryGetValue(name, out BoardDefinitionBuilder board))
                return board;

            return null;
        }

        public BoardDefinitionBuilder AddBoard(string name)
        {
            var board = new BoardDefinitionBuilder();
            Boards.Add(name, board);
            return board;
        }

        public bool CanBuild()
        {
            if (!Players.CanBuild())
                return false;

            foreach (var board in Boards.Values)
                if (!board.CanBuild())
                    return false;

            return true;
        }

        public GameDefinition Build()
        {
            var definition = new GameDefinition(
                Players.Build(),
                Boards.Values.Select(board => board.Build()).ToArray()
            );

            return definition;
        }
    }
}
