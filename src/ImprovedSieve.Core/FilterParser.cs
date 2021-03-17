using Antlr4.Runtime.Tree;

namespace ImprovedSieve.Core
{
    public class FilterParser : ParserBase
    {
        public override IParseTree Parse(string expression)
        {
            var parser = GetParser(expression);

            return parser.filter();
        }
    }
}