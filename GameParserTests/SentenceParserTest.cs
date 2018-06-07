using GameParser.Builders;
using NaturalConfiguration;

namespace GameParserTests
{
    public abstract class SentenceParserTest<TParser>
        where TParser : SentenceParser<GameDefinitionBuilder>, new()
    {
        protected TParser CreateParser() => new TParser();

        public virtual void TestInvalid(string sentence) { }
        public virtual void TestNotMatching(string sentence) { }
        public virtual void TestMissingRequirement(string sentence) { }
        public virtual void TestDefaults() { }
    }
}
