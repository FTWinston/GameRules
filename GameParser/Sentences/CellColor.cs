using GameParser.Builders;
using NaturalConfiguration;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace GameParser.Sentences
{
    public class CellColor : SentenceParser<GameDefinitionBuilder>
    {
        public override string Name => "Specify the color of board cells.";
        public override string Group => "Board";

        public override string[] Examples => new[]
        {
            "Cells on the board are colored grey",
            "Cells on the board are colored #66cccc",
        };

        protected override string ExpressionText => $"Cells on (?:the )?({WordExpression}) are colou?red ({WordExpression})";

        protected override IEnumerable<ParserError> ParseMatch(GameDefinitionBuilder builder, Match match)
        {
            bool success = true;
            var name = match.Groups[1].Value;
            var board = builder.GetBoard(name);

            if (board == null)
            {
                yield return new ParserError($"No board called {name} has been defined yet.", match.Groups[1]);
                success = false;
            }

            var backgroundColor = match.Groups[2].Value;

            if (!Colors.IsValidColor(backgroundColor))
            {
                yield return new ParserError($"Invalid color: '{backgroundColor}' not recognised.", match.Groups[2]);
                success = false;
            }
            
            if (success)
            {
                board.BackgroundColor = backgroundColor;
            }
        }
    }
}
