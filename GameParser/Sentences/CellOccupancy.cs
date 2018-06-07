using GameParser.Builders;
using NaturalConfiguration;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace GameParser.Sentences
{
    public class CellOccupancy : SentenceParser<GameDefinitionBuilder>
    {
        public override string Name => "Specify whether multiple pieces can occupy the same cell";
        public override string Group => "Board";

        public override string[] Examples => new[]
        {
            "Multiple pieces can occupy a cell on the board",
            "Only one piece can occupy a cell on scoreboard",
        };

        protected override string ExpressionText => $"(multiple pieces|only one piece) can occupy a cell on (?:the )?({WordExpression})";

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
