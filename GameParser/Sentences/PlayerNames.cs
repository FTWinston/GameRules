using GameParser.Builders;
using NaturalConfiguration;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace GameParser.Sentences
{
    public class PlayerNames : ListParser<GameDefinitionBuilder>
    {
        protected override string ExpressionPrefix => "player names are ";

        protected override IEnumerable<ParserError> ParseValues(GameDefinitionBuilder builder, List<Capture> values)
        {
            if (!builder.Players.HasSpecifiedNumbers)
            {
                yield return new ParserError("Specify the number of players before their names.");
                yield break;
            }
            
            if (builder.Players.MaxPlayers != values.Count)
            {
                var paramName = builder.Players.MinPlayers == builder.Players.MaxPlayers ? "number" : "max number";
                yield return new ParserError($"Number of names doesn't match the {paramName} of players. Got {values.Count}, expected {builder.Players.MaxPlayers}.");
                yield break;
            }

            builder.Players.SetPlayerNames(values.Select(v => v.Value).ToArray());
        }
    }
}
