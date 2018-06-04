using GameParser.Builders;
using RulesParser;
using System.Text.RegularExpressions;

namespace GameParser.Sentences
{
    class PlayerCount : SentenceParser<GameDefinitionBuilder>
    {
        protected override string Expression => "there (?:are|is) (\\d+)(?:(?: - |-| to )(\\d+))? players?";
        
        protected override string ParseMatch(GameDefinitionBuilder builder, Match match)
        {
            int minPlayers = int.Parse(match.Groups[1].Value);
            bool hasRange = match.Groups[2].Success;

            if (minPlayers <= 0)
            {
                var paramName = hasRange ? "min player count" : "player count";
                return $"Invalid ${paramName}: ${minPlayers}. Must be >= 1.";
            }

            if (hasRange)
            {
                int maxPlayers = int.Parse(match.Groups[2].Value);

                if (maxPlayers <= minPlayers)
                {
                    return $"Invalid max player count: ${maxPlayers}. Must be >= min player count, which is ${minPlayers}.";
                }

                builder.Players.SetPlayerCount(minPlayers, maxPlayers);
            }
            else
            {
                builder.Players.SetPlayerCount(minPlayers);
            }

            return null;
        }
    }
}
