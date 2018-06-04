using System.Text.RegularExpressions;

namespace RulesParser
{
    public class ParserError
    {
        public ParserError(SentenceParser parser, string sentence, string message)
        {
            Parser = parser;
            Sentence = sentence;
            Message = message;
        }

        public SentenceParser Parser { get; }
        public string Sentence { get; }
        public string Message { get; }
    }
}
