using GameParser.Builders;
using GameParser.Sentences;
using NaturalConfiguration;
using Xunit;

namespace GameParserTests.SentenceParsers
{
    public class CellBorderTests
    {
        [Theory]
        [InlineData("Cells on the board have no border", null, 0)]
        [InlineData("Cells on the board have a red border", "red", 2)]
        [InlineData("Cells on the board have a thin black border", "black", 1)]
        [InlineData("Cells on the board have a thick white border", "white", 3)]
        [InlineData("Cells on the board have a medium grey border", "grey", 2)]
        [InlineData("Cells on the board have a medium #cccccc border", "#cccccc", 2)]
        public void TestValid(string sentence, string color, int thickness)
        {
            var parser = new CellBorder();
            var builder = new GameDefinitionBuilder();

            (new BoardGrid()).Parse(builder, "The board is an 8x8 grid", out _);

            var didMatch = parser.Parse(builder, sentence, out ParserError[] errors);
            Assert.True(didMatch);
            Assert.Empty(errors);

            var definition = builder.Build();
            Assert.Single(definition.Boards);

            var board = definition.Boards[0];
            Assert.Equal(board.BorderColor, color);
            Assert.Equal(board.BorderWidth, thickness);
        }

        [Theory]
        [InlineData("Cells on the board have a huge red border")]
        public void TestInvalid(string sentence)
        {
            var parser = new CellBorder();
            var builder = new GameDefinitionBuilder();

            var didMatch = parser.Parse(builder, sentence, out ParserError[] errors);
            Assert.True(didMatch);
            Assert.NotEmpty(errors);
        }

        [Theory]
        [InlineData("")]
        [InlineData("Cells on the board have no black border")]
        [InlineData("Cells on the board have a border")]
        [InlineData("Cells on the board have a  border")]
        public void TestNotMatching(string sentence)
        {
            var parser = new CellBorder();
            var builder = new GameDefinitionBuilder();

            var didMatch = parser.Parse(builder, sentence, out ParserError[] errors);

            Assert.False(didMatch);
        }

        [Theory]
        [InlineData("Cells on the board have a thin black border")]
        public void TestMissingRequirement(string sentence)
        {
            var parser = new CellBorder();
            var builder = new GameDefinitionBuilder();

            var didMatch = parser.Parse(builder, sentence, out ParserError[] errors);
            Assert.True(didMatch);
            Assert.NotEmpty(errors);
        }

        [Fact]
        public void TestDefaults()
        {
            var builder = new GameDefinitionBuilder();

            (new BoardGrid()).Parse(builder, "The board is a 2x2 grid", out _);

            var definition = builder.Build();

            Assert.Single(definition.Boards);

            var board = definition.Boards[0];
            Assert.Equal(0, board.BorderWidth);
            Assert.Null(board.BorderColor);
        }
    }
}
