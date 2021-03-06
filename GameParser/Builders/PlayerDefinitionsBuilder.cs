﻿using GameModel.Definition;
using System.Collections.Generic;

namespace GameParser.Builders
{
    public class PlayerDefinitionsBuilder : IBuilder<PlayerDefinition[]>
    {
        private Dictionary<string, PlayerDefinition> PlayersByName = new Dictionary<string, PlayerDefinition>();

        public int MinPlayers { get; private set; } = 2;
        public int MaxPlayers { get; private set; } = 2;
        public bool HasSpecifiedNumbers { get; private set; } = false;

        public string[] PlayerColors { get; private set; } = null;
        public string[] PlayerNames { get; private set; } = null;
        
        public bool CanBuild()
        {
            return HasSpecifiedNumbers;
        }

        public PlayerDefinition[] Build()
        {
            var definitions = new List<PlayerDefinition>();

            for (int i=0; i < MaxPlayers; i++)
            {
                bool required = i < MinPlayers;

                string name = PlayerNames != null && PlayerNames.Length > i
                    ? PlayerNames[i]
                    : $"Player {i + 1}";

                string color = PlayerColors != null && PlayerColors.Length > i
                    ? PlayerColors[i]
                    : Colors.DefaultColors.Length > i ? Colors.DefaultColors[i]
                    : "grey";

                definitions.Add(new PlayerDefinition(required, name, color));
            }

            return definitions.ToArray();
        }

        public PlayerDefinitionsBuilder SetPlayerCount(int min, int max)
        {
            MinPlayers = min;
            MaxPlayers = max;
            HasSpecifiedNumbers = true;
            return this;
        }

        public PlayerDefinitionsBuilder SetPlayerCount(int count)
        {
            MinPlayers = count;
            MaxPlayers = count;
            HasSpecifiedNumbers = true;
            return this;
        }

        public PlayerDefinitionsBuilder SetPlayerColors(params string[] colors)
        {
            PlayerColors = colors;
            if (PlayerNames == null)
            {
                PlayerNames = colors;
            }
            return this;
        }

        public PlayerDefinitionsBuilder SetPlayerNames(params string[] names)
        {
            PlayerNames = names;
            return this;
        }
    }
}
