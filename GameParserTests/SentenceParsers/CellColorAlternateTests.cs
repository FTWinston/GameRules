using GameParser.Builders;
using GameParser.Sentences;
using NaturalConfiguration;
using Xunit;

namespace GameParserTests.SentenceParsers
{
    public class CellColorAlternateTests : SentenceParserTest<CellColorAlternate>
    {
        [Theory]
        [InlineData("Cells on the board are alternately colored black and white", new[] { "black", "white" })]
        [InlineData("Cells on the board are alternately colored red, white and blue", new[] { "red", "white", "blue" })]
        [InlineData("Cells on the board are alternately colored #cccccc, #333333", new[] { "#cccccc", "#333333" })]
        public void TestValid(string sentence, string[] colors)
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
            int iColor = 0;

            foreach (var cell in board.Cells)
            {
                Assert.Equal(cell.Color, colors[iColor++]);

                if (iColor >= colors.Length)
                    iColor = 0;
            }
        }

        [Theory]
        [InlineData("Cells on the board are alternately colored red, white and nonsense")]
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
        [InlineData("Cells on the board are colored black and white")]
        public override void TestNotMatching(string sentence)
        {
            var parser = CreateParser();
            var builder = new GameDefinitionBuilder();

            var didMatch = parser.Parse(builder, sentence, out ParserError[] errors);

            Assert.False(didMatch);
        }

        [Theory]
        [InlineData("Cells on the board are alternately colored black and white")]
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
