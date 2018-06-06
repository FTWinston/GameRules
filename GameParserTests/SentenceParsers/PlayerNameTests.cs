using GameParser.Builders;
using GameParser.Sentences;
using NaturalConfiguration;
using System.Collections.Generic;
using Xunit;

namespace GameParserTests.SentenceParsers
{
    public class PlayerNameTests
    {
        [Theory]
        [InlineData("Player names are Jimmy, Bob", new[] { "Jimmy", "Bob" })]
        [InlineData("Player names are Alpha, Bravo and Charlie", new[] { "Alpha", "Bravo", "Charlie" })]
        [InlineData("Player names are black, white", new[] { "black", "white" })]
        [InlineData("Player names are white and black", new[] { "white", "black" })]
        [InlineData("Player names are Loner", new[] { "Loner" })]
        public void TestValid(string sentence, string[] names)
        {
            var parser = new PlayerNames();
            var builder = new GameDefinitionBuilder();
            
            (new PlayerCount()).Parse(builder, $"There are {names.Length} players", out _);

            var didMatch = parser.Parse(builder, sentence, out ParserError[] errors);
            Assert.True(didMatch);
            Assert.Empty(errors);

            var definition = builder.Build();
            for (int i=0; i<definition.Players.Length && i < names.Length; i++)
            {
                Assert.Equal(names[i], definition.Players[i].Name);
            }
        }

        [Theory]
        [InlineData("")]
        [InlineData("Player names are ")]
        [InlineData("Player name are red, white, blue")]
        [InlineData("Players names are red, white, blue")]
        [InlineData("Players name are red, white, blue")]
        [InlineData("Player names are red and white and blue")]
        [InlineData("Player name is grey")]
        [InlineData("Player names are Alpha,Bravo and Charlie")]
        public void TestNotMatching(string sentence)
        {
            var parser = new PlayerNames();
            var builder = new GameDefinitionBuilder();

            var didMatch = parser.Parse(builder, sentence, out ParserError[] errors);

            Assert.False(didMatch);
        }

        [Theory]
        [InlineData("Player names are red, white, blue")]
        public void TestMissingRequirement(string sentence)
        {
            var parser = new PlayerNames();
            var builder = new GameDefinitionBuilder();

            var didMatch = parser.Parse(builder, sentence, out ParserError[] errors);
            Assert.True(didMatch);
            Assert.NotEmpty(errors);
        }

        [Fact]
        public void TestDefaults()
        {
            var builder = new GameDefinitionBuilder();
            var definition = builder.Build();
            
            Assert.Equal(2, definition.Players.Length);
            Assert.Equal("Player 1", definition.Players[0].Name);
            Assert.Equal("Player 2", definition.Players[1].Name);
        }
    }
}
