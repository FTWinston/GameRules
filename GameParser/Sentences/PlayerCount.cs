using GameParser.Builders;
using NaturalConfiguration;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace GameParser.Sentences
{
    class PlayerCount : SentenceParser<GameDefinitionBuilder>
    {
        protected override string Expression => "there (?:are|is) (\\d+)(?:(?: - |-| to )(\\d+))? players?";
        
        protected override IEnumerable<ParserError> ParseMatch(GameDefinitionBuilder builder, Match match)
        {
            bool success = true;
            int minPlayers = int.Parse(match.Groups[1].Value);
            bool hasRange = match.Groups[2].Success;

            if (minPlayers <= 0)
            {
                var paramName = hasRange ? "min player count" : "player count";
                yield return new ParserError($"Invalid ${paramName}: ${minPlayers}. Must be >= 1.", match.Groups[1]);
                success = false;
            }

            if (hasRange)
            {
                int maxPlayers = int.Parse(match.Groups[2].Value);

                if (maxPlayers <= minPlayers)
                {
                    yield return new ParserError($"Invalid max player count: ${maxPlayers}. Must be >= min player count, which is ${minPlayers}.", match.Groups[2]);
                    success = false;
                }

                if (success)
                {
                    builder.Players.SetPlayerCount(minPlayers, maxPlayers);
                }
            }
            else if (success)
            {
                builder.Players.SetPlayerCount(minPlayers);
            }
        }
    }
}
