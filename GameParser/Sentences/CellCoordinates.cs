using GameParser.Builders;
using NaturalConfiguration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace GameParser.Sentences
{
    public class CellCoordinates : SentenceParser<GameDefinitionBuilder>
    {
        public override string Name => "Specify unique references for board cells.";
        public override string Group => "Board";

        public override string[] Examples => new[]
        {
            "Cells on the board are referenced using a letter for their row and a number for their column, starting with A1 at the southwest corner",
        };

        protected override string ExpressionText => $"Cells on (?:the )?({WordExpression}) are referenced using a ({WordExpression}) for their ({WordExpression}) and a ({WordExpression}) for their ({WordExpression}), starting with ({WordExpression}) at the ({WordExpression}) corner";

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

            var rowOrColumn1 = match.Groups[3].Value;
            if (!rowOrColumn1.Equals("row", StringComparison.InvariantCultureIgnoreCase))
            {
                yield return new ParserError($"Specify the row as the first axis for coordinates.", match.Groups[3]);
                success = false;
            }

            var rowOrColumn2 = match.Groups[5].Value;
            if (!rowOrColumn2.Equals("column", StringComparison.InvariantCultureIgnoreCase))
            {
                yield return new ParserError($"Specify the column as the second axis for coordinates.", match.Groups[5]);
                success = false;
            }

            Func<int, string> rowGenerator, columnGenerator;

            var rowLetterOrNumber = match.Groups[2].Value;

            if (rowLetterOrNumber.Equals("number", StringComparison.InvariantCultureIgnoreCase))
            {
                rowGenerator = i => i.ToString();
            }
            else if (rowLetterOrNumber.Equals("letter", StringComparison.InvariantCultureIgnoreCase))
            {
                rowGenerator = i => GetLetterFromNumber(i);
            }
            else
            {
                yield return new ParserError($"Invalid value: '{rowLetterOrNumber}' - expected 'letter' or 'number'.", match.Groups[2]);
                success = false;
            }

            
            var columnLetterOrNumber = match.Groups[4].Value;

            if (columnLetterOrNumber.Equals("number", StringComparison.InvariantCultureIgnoreCase))
            {
                columnGenerator = i => i.ToString();
            }
            else if (columnLetterOrNumber.Equals("letter", StringComparison.InvariantCultureIgnoreCase))
            {
                columnGenerator = i => GetLetterFromNumber(i);
            }
            else
            {
                yield return new ParserError($"Invalid value: '{columnLetterOrNumber}' - expected 'letter' or 'number'.", match.Groups[4]);
                success = false;
            }

            var startingReference = match.Groups[6].Value;
            var startingCorner = match.Groups[7].Value;
            
            // TODO: handle starting reference, determine starting position and direction, somehow
            
            if (success)
            {
                // board.SetReferencesSomehow();
            }
        }

        private static string GetLetterFromNumber(int columnNumber)
        {
            int dividend = columnNumber;
            var columnName = new StringBuilder();
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                columnName.Insert(0, 'A' + modulo);
                dividend = (dividend - modulo) / 26;
            }

            return columnName.ToString();
        }
    }
}
