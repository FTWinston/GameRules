using GameParser.Builders;
using NaturalConfiguration;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace GameParser.Sentences
{
    public class CellColorSingle : SentenceParser<GameDefinitionBuilder>
    {
        protected override string Expression => $"cells on (?:the )?(\\w+) are colou?red ({Colors.ColorExpression})(?:, and have a (thin|thick)? ({Colors.ColorExpression}) border)?";

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

            if (match.Groups[4].Success)
            {
                var borderColor = match.Groups[4].Value;

                if (!Colors.IsValidColor(borderColor))
                {
                    yield return new ParserError($"Invalid color: '{borderColor}' not recognised.", match.Groups[4]);
                    success = false;
                }

                int thickness = 2;
                if (match.Groups[3].Success)
                {
                    thickness = match.Groups[3].Value.Equals("thin", StringComparison.InvariantCultureIgnoreCase) ? 1 : 3;
                }

                if (success)
                {
                    board.BorderColor = borderColor;
                    board.BorderWidth = thickness;
                }
            }

            if (success)
            {
                board.BackgroundColor = backgroundColor;
            }
        }
    }
}
