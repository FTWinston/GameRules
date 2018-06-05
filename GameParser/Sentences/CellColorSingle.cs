using GameParser.Builders;
using RulesParser;
using System;
using System.Text.RegularExpressions;

namespace GameParser.Sentences
{
    public class CellColorSingle : SentenceParser<GameDefinitionBuilder>
    {
        protected override string Expression => $"cells on (?:the )?(\\w+) are colou?red ({Colors.ColorExpression})(?:, and have a (thin|thick)? ({Colors.ColorExpression}) border)?";

        protected override string ParseMatch(GameDefinitionBuilder builder, Match match)
        {
            var name = match.Groups[1].Value;
            var board = builder.GetBoard(name);

            if (board == null)
            {
                return $"No board called {name} has been defined yet.";
            }

            var backgroundColor = match.Groups[2].Value;

            if (!Colors.IsValidColor(backgroundColor))
            {
                return $"Invalid color: '{backgroundColor}' not recognised.";
            }

            board.BackgroundColor = backgroundColor;

            if (!match.Groups[4].Success)
            {
                return null;
            }

            var borderColor = match.Groups[4].Value;

            if (!Colors.IsValidColor(borderColor))
            {
                return $"Invalid color: '{borderColor}' not recognised.";
            }

            int thickness = 2;
            if (match.Groups[3].Success)
            {
                thickness = match.Groups[3].Value.Equals("thin", StringComparison.InvariantCultureIgnoreCase) ? 1 : 3;
            }

            board.BorderColor = borderColor;
            board.BorderWidth = thickness;
            return null;
        }
    }
}
