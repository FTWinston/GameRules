using GameParser.Builders;
using GameParser.Sentences;
using NaturalConfiguration;
using System.Collections.Generic;
using Xunit;

namespace GameParserTests.SentenceParsers
{
    public class PlayerColorTests : SentenceParserTest<PlayerColors>
    {
        [Theory]
        [InlineData("Player colors are red, white, blue", new[] { "red", "white", "blue" })]
        [InlineData("Player colors are red, white and blue", new[] { "red", "white", "blue" })]
        [InlineData("Player colors are red, #cccccc and blue", new[] { "red", "#cccccc", "blue" })]
        [InlineData("Player colors are black, white", new[] { "black", "white" })]
        [InlineData("Player colors are white and black", new[] { "white", "black" })]
        [InlineData("Player colors are grey", new[] { "grey" })]
        [InlineData("Player colours are red, white, blue", new[] { "red", "white", "blue" })]
        [InlineData("Player colours are red, white and blue", new[] { "red", "white", "blue" })]
        [InlineData("Player colours are red, #cccccc and blue", new[] { "red", "#cccccc", "blue" })]
        [InlineData("Player colours are black, white", new[] { "black", "white" })]
        [InlineData("Player colours are white and black", new[] { "white", "black" })]
        [InlineData("Player colours are grey", new[] { "grey" })]
        public void TestValid(string sentence, string[] colors)
        {
            var parser = CreateParser();
            var builder = new GameDefinitionBuilder();
            
            (new PlayerCount()).Parse(builder, $"There are {colors.Length} players", out _);

            var didMatch = parser.Parse(builder, sentence, out ParserError[] errors);
            Assert.True(didMatch);
            Assert.Empty(errors);

            var definition = builder.Build();
            for (int i=0; i<definition.Players.Length && i < colors.Length; i++)
            {
                Assert.Equal(colors[i], definition.Players[i].Color);
                Assert.Equal(colors[i], definition.Players[i].Name);
            }
        }

        [Theory]
        [InlineData("Player colors are silly and invalid")]
        [InlineData("Player colors are #ccc and black")]
        [InlineData("Player colors are white and #00000g")]
        public override void TestInvalid(string sentence)
        {
            var parser = CreateParser();
            var builder = new GameDefinitionBuilder();

            (new PlayerCount()).Parse(builder, $"There are 2 players", out _);

            var didMatch = parser.Parse(builder, sentence, out ParserError[] errors);
            Assert.True(didMatch);
            Assert.NotEmpty(errors);
        }

        [Theory]
        [InlineData("")]
        [InlineData("Player colors are ")]
        [InlineData("Player color are red, white, blue")]
        [InlineData("Players colors are red, white, blue")]
        [InlineData("Players color are red, white, blue")]
        [InlineData("Player colors are red and white and blue")]
        [InlineData("Player color is grey")]
        public override void TestNotMatching(string sentence)
        {
            var parser = CreateParser();
            var builder = new GameDefinitionBuilder();

            var didMatch = parser.Parse(builder, sentence, out ParserError[] errors);

            Assert.False(didMatch);
        }

        [Theory]
        [InlineData("Player colors are red, white, blue")]
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
            var definition = builder.Build();
            
            Assert.Equal(2, definition.Players.Length);
            Assert.Equal("red", definition.Players[0].Color);
            Assert.Equal("blue", definition.Players[1].Color);
        }
    }
}
