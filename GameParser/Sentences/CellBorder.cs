using GameParser.Builders;
using NaturalConfiguration;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace GameParser.Sentences
{
    public class CellBorder : SentenceParser<GameDefinitionBuilder>
    {
        public override string Name => "Specify the color and width of borders on board cells.";
        public override string Group => "Board";

        public override string[] Examples => new[]
        {
            "Cells on the board have a black border",
            "Cells on the board have a thin grey border",
            "Cells on scoreboard have a thick #cccccc border",
            "Cells on the board have no border",
        };

        // TODO: allow specifying only particular rows or columns
        protected override string ExpressionText => "Cells on (?:the )?(\\w+) have (no|an? (?:(\\w+) )?(\\S+)) border";

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

            string borderColor;
            int thickness = 2;

            if (match.Groups[2].Value.Equals("no", StringComparison.InvariantCultureIgnoreCase))
            {
                borderColor = null;
                thickness = 0;
            }
            else
            {
                borderColor = match.Groups[4].Value;

                if (!Colors.IsValidColor(borderColor))
                {
                    yield return new ParserError($"Invalid color: '{borderColor}' not recognised.", match.Groups[4]);
                    success = false;
                }

                if (match.Groups[3].Success)
                {
                    var value = match.Groups[3].Value.ToLowerInvariant();

                    switch (value)
                    {
                        case "thin":
                            thickness = 1;
                            break;
                        case "medium":
                            thickness = 2;
                            break;
                        case "thick":
                            thickness = 3;
                            break;
                        default:
                            yield return new ParserError($"Invalid line thickness: '{value}' not recognised.", match.Groups[3]);
                            success = false;
                            break;
                    }
                }
            }

            if (success)
            {
                board.BorderColor = borderColor;
                board.BorderWidth = thickness;
            }
        }
    }
}
