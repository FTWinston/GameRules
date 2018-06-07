using GameParser.Builders;
using NaturalConfiguration;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace GameParser.Sentences
{
    public class PlayerColors : ListParser<GameDefinitionBuilder>
    {
        public override string Name => "Specify player colors";
        public override string Group => "Players";

        public override string[] Examples => new[]
        {
            "Player colors are black and white",
            "Player colors are red, white and blue",
            "Player colors are #333333, #cccccc",
        };
        
        protected override string ExpressionPrefix => "Player colou?rs are ";

        protected override IEnumerable<ParserError> ParseMatch(GameDefinitionBuilder builder, Match match, List<Capture> values)
        {
            bool success = true;

            if (!builder.Players.HasSpecifiedNumbers)
            {
                yield return new ParserError("Specify the number of players before their colors.");
                success = false;
            }
            else if (builder.Players.MaxPlayers != values.Count)
            {
                var paramName = builder.Players.MinPlayers == builder.Players.MaxPlayers ? "number" : "max number";
                yield return new ParserError($"Number of colors doesn't match the {paramName} of players. Got {values.Count}, expected {builder.Players.MaxPlayers}.");
                success = false;
            }

            foreach (var value in values)
            {
                if (!Colors.IsValidColor(value.Value))
                {
                    yield return new ParserError($"Invalid color: '{value}' not recognised.", value);
                    success = false;
                }
            }

            if (success)
            {
                builder.Players.SetPlayerColors(values.Select(v => v.Value).ToArray());
            }
        }
    }
}
