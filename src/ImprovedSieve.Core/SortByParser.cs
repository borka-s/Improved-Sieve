using Antlr4.Runtime.Tree;

namespace ImprovedSieve.Core
{
    public class SortByParser : ParserBase
    {
        public override IParseTree Parse(string expression)
        {
            var parser = GetParser(expression);

            return parser.sortBy();
        }
    }
}