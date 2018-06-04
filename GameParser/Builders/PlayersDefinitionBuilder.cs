using GameModel.Definition;
using RulesParser;
using System.Collections.Generic;

namespace GameParser.Builders
{
    public class PlayersDefinitionBuilder : IBuilder<PlayerDefinition[]>
    {
        private Dictionary<string, PlayerDefinition> PlayersByName = new Dictionary<string, PlayerDefinition>();

        public int? MinPlayers { get; private set; }
        public int? MaxPlayers { get; private set; }

        public string[] PlayerColors { get; private set; } = {
            "red",
            "blue",
            "yellow",
            "green",
            "purple",
            "orange",
            "pink",
            "brown",
            "grey",
            "teal",
            "cyan",
            "fuschia",
            "magenta",
            "olive",
            "maroon",
            "silver",
            "indigo",
            "coral",
            "gold",
            "sienna",
            "lime",
            "plum",
            "tan",
            "beige",
            "lavendar",
            "salmon",
            "khaki",
            "linen",
            "crimson",
            "navy",
            "black",
            "white",
        };
        public string[] PlayerNames { get; private set; } = null;
        
        public bool CanBuild()
        {
            return MinPlayers.HasValue;
        }

        public PlayerDefinition[] Build()
        {
            // TODO
            return new PlayerDefinition[] { };
        }

        public PlayersDefinitionBuilder SetPlayerCount(int min, int max)
        {
            MinPlayers = min;
            MaxPlayers = max;
            return this;
        }

        public PlayersDefinitionBuilder SetPlayerCount(int count)
        {
            MinPlayers = count;
            MaxPlayers = count;
            return this;
        }

        public PlayersDefinitionBuilder SetPlayerColors(params string[] colors)
        {
            PlayerColors = colors;
            if (PlayerNames == null)
            {
                PlayerNames = colors;
            }
            return this;
        }

        public PlayersDefinitionBuilder SetPlayerNames(params string[] names)
        {
            PlayerNames = names;
            return this;
        }
    }
}
