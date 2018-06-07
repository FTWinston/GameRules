using GameParser.Builders;
using GameParser.Sentences;
using NaturalConfiguration;
using Xunit;

namespace GameParserTests.SentenceParsers
{
    public class CellColorSingleTests : SentenceParserTest<CellColorSingle>
    {
        [Theory]
        [InlineData("Cells on the board are colored red", "red")]
        [InlineData("Cells on the board are coloured black", "black")]
        [InlineData("Cells on the board are colored #cccccc", "#cccccc")]
        public void TestValid(string sentence, string color)
        {
            var parser = CreateParser();
            var builder = new GameDefinitionBuilder();

            (new BoardGrid()).Parse(builder, "The board is an 8x8 grid", out _);

            var didMatch = parser.Parse(builder, sentence, out ParserError[] errors);
            Assert.True(didMatch);
            Assert.Empty(errors);

            var definition = builder.Build();
            Assert.Single(definition.Boards);

            var board = definition.Boards[0];
            foreach (var cell in board.Cells)
            {
                Assert.Equal(cell.Color, color);
            }
        }

        [Theory]
        [InlineData("Cells on the board are colored sausage")]
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
        [InlineData("Cells on the board are not coloured")]
        public override void TestNotMatching(string sentence)
        {
            var parser = CreateParser();
            var builder = new GameDefinitionBuilder();

            var didMatch = parser.Parse(builder, sentence, out ParserError[] errors);

            Assert.False(didMatch);
        }

        [Theory]
        [InlineData("Cells on the board are colored grey")]
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
            foreach (var cell in board.Cells)
            {
                Assert.Equal("grey", cell.Color);
            }
        }
    }
}
