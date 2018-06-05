using GameParser.Builders;
using NaturalConfiguration;
using System.Collections.Generic;

namespace GameParser.Sentences
{
    public class PlayerNames : ListParser<GameDefinitionBuilder>
    {
        protected override string ExpressionPrefix => "player names are ";

        protected override string ParseValues(GameDefinitionBuilder builder, List<string> values)
        {
            if (!builder.Players.HasSpecifiedNumbers)
            {
                return $"Specify the number of players before their names.";
            }
            
            if (builder.Players.MaxPlayers != values.Count)
            {
                var paramName = builder.Players.MinPlayers == builder.Players.MaxPlayers ? "number" : "max number";
                return $"Number of names doesn't match the {paramName} of players. Got {values.Count}, expected {builder.Players.MaxPlayers}.";
            }

            builder.Players.SetPlayerNames(values.ToArray());
            return null;
        }
    }
}
