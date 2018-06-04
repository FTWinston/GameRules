using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RulesParser
{
    public abstract class SentenceParser
    {

    }

    public abstract class SentenceParser<TBuilder> : SentenceParser
    {
        protected SentenceParser()
        {
            Regex = new Regex($"^{Expression}$", RegexOptions.IgnoreCase | RegexOptions.Singleline);
        }
        
        protected abstract string Expression { get; }
        private Regex Regex { get; }

        public bool Parse(TBuilder builder, string sentence, out string error)
        {
            var match = Regex.Match(sentence);
            if (!match.Success)
            {
                error = null;
                return false;
            }
            
            error = ParseMatch(builder, match);
            return true;
        }

        protected abstract string ParseMatch(TBuilder builder, Match match);
    }
}
