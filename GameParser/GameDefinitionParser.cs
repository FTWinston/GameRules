using GameParser.Builders;
using GameParser.Sentences;
using RulesParser;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("GameParserTests")]

namespace GameParser
{
    public class GameDefinitionParser : TextParser<GameDefinitionBuilder>
    {
        protected override IEnumerable<SentenceParser<GameDefinitionBuilder>> CreateSentenceParsers()
        {
            yield return new PlayerCount();
        }
    }
}
