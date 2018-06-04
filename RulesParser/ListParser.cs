using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RulesParser
{
    public abstract class ListParser<TBuilder>: SentenceParser<TBuilder>
    {
        protected override string Expression => $"{ExpressionPrefix}({ElementExpression})(?:, ({ElementExpression}))*(?: and ({ElementExpression}))?";
        protected abstract string ExpressionPrefix { get; }
        protected virtual string ElementExpression { get; } = "\\w+";

        protected override string ParseMatch(TBuilder builder, Match match)
        {
            var values = new List<string>();
            values.Add(match.Groups[1].Value);
            if (match.Groups[2].Success)
            {
                foreach (Capture capture in match.Groups[2].Captures)
                {
                    values.Add(capture.Value);
                }
            }

            if (match.Groups[3].Success)
            {
                values.Add(match.Groups[3].Value);
            }

            return ParseValues(builder, values);
        }

        protected abstract string ParseValues(TBuilder builder, List<string> values);
    }
}
