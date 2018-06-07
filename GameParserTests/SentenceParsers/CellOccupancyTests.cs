using GameParser.Builders;
using GameParser.Sentences;
using NaturalConfiguration;
using System.Collections.Generic;
using Xunit;

namespace GameParserTests.SentenceParsers
{
    public class CellOccupancyTests : SentenceParserTest<CellOccupancy>
    {
        [Theory]
        [InlineData("Only one piece can occupy a cell on the board", false)]
        [InlineData("Only one piece can occupy a cell on board", false)]
        [InlineData("Multiple pieces can occupy a cell on the board", true)]
        [InlineData("Multiple pieces can occupy a cell on board", true)]
        public void TestValid(string sentence, bool allowMultiple)
        {
            var parser = CreateParser();
            var builder = new GameDefinitionBuilder();

            (new BoardGrid()).Parse(builder, "The board is a 2x2 grid", out _);

            var didMatch = parser.Parse(builder, sentence, out ParserError[] errors);
            Assert.True(didMatch);
            Assert.Empty(errors);

            var definition = builder.Build();
            Assert.Single(definition.Boards);

            var board = definition.Boards[0];
            Assert.Equal(board.AllowMultipleOccupancy, allowMultiple);
        }

        [Theory]
        [InlineData("Only one piece can occupy a cell on something")]
        [InlineData("Multiple pieces can occupy a cell on the board")]
        public override void TestInvalid(string sentence)
        {
            var parser = CreateParser();
            var builder = new GameDefinitionBuilder();

            var didMatch = parser.Parse(builder, sentence, out ParserError[] errors);
            Assert.True(didMatch);
            Assert.NotEmpty(errors);
        }

        [Theory]
        [InlineData("")]
        [InlineData("3 pieces can occupy a cell on the board")]
        [InlineData("Only 1 piece can occupy a cell on the board")]
        public override void TestNotMatching(string sentence)
        {
            var parser = CreateParser();
            var builder = new GameDefinitionBuilder();

            var didMatch = parser.Parse(builder, sentence, out ParserError[] errors);

            Assert.False(didMatch);
        }

        [Theory]
        [InlineData("Only one piece can occupy a cell on the board")]
        public override void TestMissingRequirement(string sentence)
        {
            var parser = CreateParser();
            var builder = new GameDefinitionBuilder();

            var didMatch = parser.Parse(builder, sentence, out ParserError[] errors);
            Assert.True(didMatch);
            Assert.NotEmpty(errors);
        }

        [Fact]
        public override void TestDefaults()
        {
            var builder = new GameDefinitionBuilder();

            (new BoardGrid()).Parse(builder, "The board is a 2x2 grid", out _);

            var definition = builder.Build();

            Assert.Single(definition.Boards);

            var board = definition.Boards[0];
            Assert.True(board.AllowMultipleOccupancy);
        }
    }
}
