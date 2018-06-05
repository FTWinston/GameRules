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
            yield return new PlayerColors();
            yield return new PlayerNames();

            yield return new BoardGrid();
            yield return new CellOccupancy();
            yield return new CellColorSingle();
        }
    }
}
