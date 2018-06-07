using GameParser;
using GameParser.Builders;
using Xunit;

namespace GameParserTests
{
    public class ExampleTests
    {
        [Fact]
        public void TestAllExamples()
        {
            var parsers = new GameDefinitionParser().SentenceParsers;

            foreach (var sentenceParser in parsers)
            {
                foreach (var example in sentenceParser.Examples)
                {
                    var builder = new GameDefinitionBuilder();
                    var didMatch = sentenceParser.Parse(builder, example, out _);

                    Assert.True(didMatch); // all sentence parser examples should match their sentence
                    // ... but they might be dependent on others having run first, so we can't be sure that running in isolution produces no errors
                }
            }
        }
    }
}
