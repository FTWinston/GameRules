using GameParser.Builders;
using NaturalConfiguration;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace GameParser.Sentences
{
    public class BoardGrid : SentenceParser<GameDefinitionBuilder>
    {
        public override string Name => "Add a board and specify its size";
        public override string Group => "Board";

        public override string[] Examples => new[]
        {
            "The board is an 8x8 grid",
            "Scoreboard is a 2 by 10 grid",
        };

        protected override string ExpressionText => "(?:The )?(\\w+) is an? (\\d+)(?:x| by )(\\d+) grid";

        protected override IEnumerable<ParserError> ParseMatch(GameDefinitionBuilder builder, Match match)
        {
            bool success = true;

            var name = match.Groups[1].Value;
            if (builder.GetBoard(name) != null)
            {
                success = false;
                yield return new ParserError($"A board called {name} has already been defined.", match.Groups[1]);
            }
            
            var width = int.Parse(match.Groups[2].Value);
            if (width < 1)
            {
                success = false;
                yield return new ParserError($"Invalid grid width: {width}. Width must be at least 1.", match.Groups[2]);
            }

            var height = int.Parse(match.Groups[3].Value);
            if (height < 1)
            {
                success = false;
                yield return new ParserError($"Invalid grid height: {height}. Height must be at least 1.", match.Groups[3]);
            }

            if (success)
            {
                var board = builder.AddBoard(name);
                board.SetGridDimensions(width, height);
            }
        }
    }
}
