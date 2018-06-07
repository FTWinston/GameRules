using GameParser;
using GameParser.Builders;
using System.Linq;
using Xunit;

namespace GameParserTests.GameParsers
{
    public class CombinedTest1
    {
        [Fact]
        public void TestCombo()
        {
            var text = @"
There are 3 players.
Player names are attacker, neutral and defender.
Player colors are red, #cccccc and blue.

The board is an 8x8 grid. Only one piece can occupy a cell on the board.
Cells on the board are alternately colored black and white.
Cells on the board have a thin grey border.
";

            var parser = new GameDefinitionParser();
            var builder = new GameDefinitionBuilder();

            var errors = parser.Parse(builder, text);
            Assert.Empty(errors);

            var definition = builder.Build();
            Assert.Equal(3, definition.Players.Count(player => player.Required));
            Assert.Equal(3, definition.Players.Length);

            Assert.Equal("attacker", definition.Players[0].Name);
            Assert.Equal("red", definition.Players[0].Color);

            Assert.Equal("neutral", definition.Players[1].Name);
            Assert.Equal("#cccccc", definition.Players[1].Color);

            Assert.Equal("defender", definition.Players[2].Name);
            Assert.Equal("blue", definition.Players[2].Color);

            Assert.Single(definition.Boards);

            var board = definition.Boards[0];
            Assert.Equal(64, board.Cells.Length);
            Assert.Equal("grey", board.BorderColor);
            Assert.Equal(1, board.BorderWidth);

            for (int iCell = 0; iCell < board.Cells.Length; iCell++)
            {
                Assert.Equal(board.Cells[iCell].Color, iCell % 2 == 0 ? "black" : "white");
            }
        }
    }
}
