using GameParser.Builders;
using NaturalConfiguration;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace GameParser.Sentences
{
    public class PlayerNames : ListParser<GameDefinitionBuilder>
    {
        public override string Name => "Specify player names";
        public override string Group => "Players";

        public override string[] Examples => new[]
        {
            "Player names are black and white",
            "Player names are offense, defense and neutral",
        };

        protected override string ExpressionPrefix => "Player names are ";

        protected override IEnumerable<ParserError> ParseMatch(GameDefinitionBuilder builder, Match match, List<Capture> values)
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
