using GameParser.Builders;
using NaturalConfiguration;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace GameParser.Sentences
{
    public class CellOccupancy : SentenceParser<GameDefinitionBuilder>
    {
        protected override string Expression => "(multiple pieces|only one piece) can occupy a cell on (?:the )?(\\w+)";

        protected override IEnumerable<ParserError> ParseMatch(GameDefinitionBuilder builder, Match match)
        {
            var name = match.Groups[2].Value;
            var board = builder.GetBoard(name);

            if (board == null)
            {
                yield return new ParserError($"No board called {name} has been defined yet.", match.Groups[2]);
            }
            else
            {
                bool allowMultiple = match.Groups[1].Value.Equals("multiple pieces", StringComparison.InvariantCultureIgnoreCase);
                board.AllowMultipleOccupancy = allowMultiple;
            }
        }
    }
}
