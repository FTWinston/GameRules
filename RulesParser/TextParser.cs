using System.Collections.Generic;

namespace RulesParser
{
    public abstract class TextParser<TBuilder>
    {
        public List<ParserError> ParseAllText(TBuilder builder, string rulesText)
        {
            if (SentenceParsers == null)
            {
                CreateSentenceParsers();
            }

            var errors = new List<ParserError>();

            string[] sentences = SplitSentences(rulesText);

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

        private string[] SplitSentences(string rulesText)
        {
            // TODO: if sentences can contain a . inside a quote (or as a number separator?), don't split on those.
            return rulesText.Split('.');
        }

        private List<SentenceParser<TBuilder>> SentenceParsers { get; set; }

        private ParserError ParseSentence(TBuilder builder, string sentence)
        {
            foreach (var parser in SentenceParsers)
            {
                if (parser.Parse(builder, sentence.Trim(), out string errorMsg))
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
