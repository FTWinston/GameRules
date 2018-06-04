using GameParser.Builders;
using RulesParser;
using System.Collections.Generic;

namespace GameParser.Sentences
{
    public class PlayerColors : ListParser<GameDefinitionBuilder>
    {
        protected override string ExpressionPrefix => "player colou?rs are ";
        protected override string ElementExpression => "#[a-f\\d]{6}|\\w+";

        protected override string ParseValues(GameDefinitionBuilder builder, List<string> values)
        {
            if (!builder.Players.HasSpecifiedNumbers)
            {
                return $"Specify the number of players before their colors.";
            }
            
            if (builder.Players.MaxPlayers != values.Count)
            {
                var paramName = builder.Players.MinPlayers == builder.Players.MaxPlayers ? "number" : "max number";
                return $"Number of colors doesn't match the {paramName} of players. Got {values.Count}, expected {builder.Players.MaxPlayers}.";
            }

            foreach (var value in values)
            {
                if (!builder.Players.IsValidColor(value))
                {
                    return $"Invalid color: '{value}' not recognised.";
                }
            }

            builder.Players.SetPlayerColors(values.ToArray());
            return null;
        }
    }
}
