using System.Collections.Generic;
using System.Linq;

namespace RulesParser
{
    public abstract class TextParser<TBuilder>
    {
        public List<ParserError> Parse(TBuilder builder, string rulesText)
        {
            if (SentenceParsers == null)
            {
                SentenceParsers = CreateSentenceParsers().ToArray();
            }

            var errors = new List<ParserError>();

            IEnumerable<string> sentences = SplitSentences(rulesText);

            foreach (var sentence in sentences)
            {
                var error = ParseSentence(builder, sentence);
                if (error != null)
                {
                    errors.Add(error);
                }
            }

            return errors;
        }

        private IEnumerable<string> SplitSentences(string rulesText)
        {
            // TODO: if sentences can contain a . inside a quote (or as a number separator?), don't split on those.
            return rulesText.Split('.')
                .Select(sentence => sentence.Trim(' ', '\t', '\n', '\r'))
                .Where(sentence => sentence.Length > 0);
        }

        private SentenceParser<TBuilder>[] SentenceParsers { get; set; }

        private ParserError ParseSentence(TBuilder builder, string sentence)
        {
            foreach (var parser in SentenceParsers)
            {
                if (parser.Parse(builder, sentence, out string errorMsg))
                {
                    if (errorMsg == null)
                    {
                        return null;
                    }

                    return new ParserError(parser, sentence, errorMsg);
                }
            }

            return new ParserError(null, sentence, "Sentence doesn't match any known rules.");
        }

        protected abstract IEnumerable<SentenceParser<TBuilder>> CreateSentenceParsers();
    }
}
