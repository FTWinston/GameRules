using GameParser.Builders;
using NaturalConfiguration;
using System.Text.RegularExpressions;

namespace GameParser.Sentences
{
    public class BoardGrid : SentenceParser<GameDefinitionBuilder>
    {
        protected override string Expression => "(?:the )?(\\w+) is an? (\\d+)(?:x| by )(\\d+) grid";

        protected override string ParseMatch(GameDefinitionBuilder builder, Match match)
        {
            var name = match.Groups[1].Value;
            
            if (builder.GetBoard(name) != null)
            {
                return $"A board called {name} has already been defined.";
            }
            
            var width = int.Parse(match.Groups[2].Value);
            var height = int.Parse(match.Groups[3].Value);

            if (width < 1 || height < 1)
            {
                return $"Invalid grid size: {width}x{height}. Width and height must both be at least 1.";
            }

            var board = builder.AddBoard(name);
            board.SetGridDimensions(width, height);
            return null;
        }
    }
}
