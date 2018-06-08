using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameParser;
using GameParser.Builders;
using Microsoft.AspNetCore.Mvc;
using NaturalConfiguration;

namespace WebEditor.Controllers
{
    [Route("api/[controller]")]
    public class ConfigurationController : Controller
    {
        [HttpGet("[action]")]
        public IEnumerable<SentenceParserSummary> Info()
        {
            var parser = new GameDefinitionParser();
            return parser.SentenceParsers.Select(p => new SentenceParserSummary()
            {
                Name = p.Name,
                Group = p.Group,
                Expression = p.Expression.ToString(),
                Examples = p.Examples,
            });
        }

        public class SentenceParserSummary
        {
            public string Name { get; set; }
            public string Group { get; set; }
            public string Expression { get; set; }
            public string[] Examples { get; set; }
        }

        [HttpPost("[action]")]
        public IEnumerable<ParserError> Parse([FromBody] string text)
        {
            if (text == null)
                return null;

            var parser = new GameDefinitionParser();
            var builder = new GameDefinitionBuilder();

            var errors = parser.Parse(builder, text);

            if (errors.Count == 0)
            {
                var definition = builder.Build();
                // TODO: use definition somehow
            }

            return errors;
        }
    }
}
