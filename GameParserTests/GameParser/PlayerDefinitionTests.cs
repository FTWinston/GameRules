using GameParser;
using GameParser.Builders;
using System.Linq;
using Xunit;

namespace GameParserTests.GameParsers
{
    public class PlayerDefinitionTests
    {
        [Fact]
        public void TestCombo()
        {
            var text = @"
There are 3 players.
Player names are attacker, neutral and defender.
Player colors are red, white and blue.
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
            Assert.Equal("white", definition.Players[1].Color);

            Assert.Equal("defender", definition.Players[2].Name);
            Assert.Equal("blue", definition.Players[2].Color);
        }
    }
}
