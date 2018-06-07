using GameParser.Builders;
using GameParser.Sentences;
using NaturalConfiguration;
using System.Collections.Generic;
using Xunit;

namespace GameParserTests.SentenceParsers
{
    public class BoardGridTests : SentenceParserTest<BoardGrid>
    {
        [Theory]
        [InlineData("The board is an 8x8 grid", 8, 8)]
        [InlineData("The board is a 6x6 grid", 6, 6)]
        [InlineData("The board is an 8 by 8 grid", 8, 8)]
        [InlineData("Overview is a 10 by 10 grid", 10, 10)]
        [InlineData("The narrow is a 20 by 1 grid", 20, 1)]
        public void TestValid(string sentence, int width, int height)
        {
            var parser = CreateParser();
            var builder = new GameDefinitionBuilder();

            var didMatch = parser.Parse(builder, sentence, out ParserError[] errors);
            Assert.True(didMatch);
            Assert.Empty(errors);

            var definition = builder.Build();
            Assert.Single(definition.Boards);

            var board = definition.Boards[0];
            Assert.Equal(width * height, board.Cells.Length);
            // TODO: need separate width and height checks
        }

        [Theory]
        [InlineData("The board is a 0x8 grid")]
        [InlineData("The board is an 51x0 grid")]
        [InlineData("Zero is a 0 by 0 grid")]
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
        [InlineData("Something is a grid")]
        [InlineData("The board is an 88 grid")]
        [InlineData("The board is a 5by4 grid")]
        [InlineData("The board is an 8 by 8 gird")]
        [InlineData("Two words is a 6x6 grid")]
        public override void TestNotMatching(string sentence)
        {
            var parser = CreateParser();
            var builder = new GameDefinitionBuilder();

            var didMatch = parser.Parse(builder, sentence, out ParserError[] errors);

            Assert.False(didMatch);
        }

        [Fact]
        public override void TestDefaults()
        {
            var builder = new GameDefinitionBuilder();
            var definition = builder.Build();

            Assert.Empty(definition.Boards);
        }
    }
}
