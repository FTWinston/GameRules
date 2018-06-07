using GameParser.Builders;
using NaturalConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace GameParser.Sentences
{
    public class CellColorAlternate : ListParser<GameDefinitionBuilder>
    {
        public override string Name => "Specify alternating colors for board cells.";
        public override string Group => "Board";

        public override string[] Examples => new[]
        {
            "Cells on the board are alternately colored black and white",
            "Cells on the board are alternately colored red, #cccccc and blue",
        };

        protected override int ListGroupOffset => 1;
        
        protected override string ExpressionPrefix => $"Cells on (?:the )?({WordExpression}) are alternately colou?red ";

        protected override IEnumerable<ParserError> ParseMatch(GameDefinitionBuilder builder, Match match, List<Capture> values)
        {
            bool success = true;
            var name = match.Groups[1].Value;
            var board = builder.GetBoard(name);

            if (board == null)
            {
                yield return new ParserError($"No board called {name} has been defined yet.", match.Groups[1]);
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
                board.BackgroundColors = values.Select(v => v.Value).ToArray();
            }
        }
    }
}
