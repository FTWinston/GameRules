using GameParser.Builders;
using GameParser.Sentences;
using System.Linq;
using Xunit;

namespace GameParserTests.SentenceParsers
{
    public class PlayerCountTests
    {
        [Theory]
        [InlineData("There is 1 player", 1, 1)]
        [InlineData("There are 2 players", 2, 2)]
        [InlineData("There are 2-4 players", 2, 4)]
        [InlineData("There are 3 - 5 players", 3, 5)]
        [InlineData("There are 1 - 999 players", 1, 999)]
        [InlineData("There are 4 to 8 players", 4, 8)]
        public void TestValid(string sentence, int min, int max)
        {
            var parser = new PlayerCount();
            var builder = new GameDefinitionBuilder();

            var didMatch = parser.Parse(builder, sentence, out string error);
            Assert.True(didMatch);
            Assert.Null(error);

            var definition = builder.Build();
            Assert.Equal(min, definition.Players.Count(player => player.Required));
            Assert.Equal(max, definition.Players.Length);
        }

        [Theory]
        [InlineData("There are 0 players")]
        [InlineData("There are 0-1 players")]
        [InlineData("There are 0 - 1 players")]
        [InlineData("There are 0 to 1 players")]
        [InlineData("There are 4 - 2 players")]
        [InlineData("There are 4-2 players")]
        [InlineData("There are 4 to 2 players")]
        public void TestInvalid(string sentence)
        {
            var parser = new PlayerCount();
            var builder = new GameDefinitionBuilder();

            var didMatch = parser.Parse(builder, sentence, out string error);
            Assert.True(didMatch);
            Assert.NotNull(error);
        }

        [Theory]
        [InlineData("")]
        [InlineData("There are players")]
        [InlineData("There are -1 players")]
        [InlineData("There are 2- 4 players")]
        [InlineData("There are 2 -4 players")]
        [InlineData("There are 2to 4 players")]
        [InlineData("There are 2 to4 players")]
        [InlineData("There are 2to4 players")]
        public void TestNotMatching(string sentence)
        {
            var parser = new PlayerCount();
            var builder = new GameDefinitionBuilder();

            var didMatch = parser.Parse(builder, sentence, out string error);

            Assert.False(didMatch);
        }

        [Fact]
        public void TestDefaults()
        {
            var builder = new GameDefinitionBuilder();
            var definition = builder.Build();

            Assert.Equal(2, definition.Players.Count(player => player.Required));
            Assert.Equal(2, definition.Players.Length);
        }
    }
}
