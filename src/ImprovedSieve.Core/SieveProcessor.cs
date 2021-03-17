using System.Linq;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using ImprovedSieve.Core.Antlr;
using ImprovedSieve.Core.Visitors;

namespace ImprovedSieve.Core
{
    public class SieveProcessor
    {
        public IQueryable<T> ParseFilter<T>(IQueryable<T> query, string filterExpression)
        {
            var inputStream = new AntlrInputStream(filterExpression);
            var lexer = new AutoFilterLexer(inputStream);

            var commonTokenStream = new CommonTokenStream(lexer);
            var parser = new AutoFilterParser(commonTokenStream);

            var templateFile = parser.filter();

            var visitor = new AutoParser<T>(query, query.Expression);

            var expression = visitor.Visit(templateFile);

            return (IQueryable<T>) query.Provider.CreateQuery(expression);
        }

        public IQueryable<T> ParseSort<T>(IQueryable<T> query, string sortByExpression)
        {
            var inputStream = new AntlrInputStream(sortByExpression);
            var lexer = new AutoFilterLexer(inputStream);

            var commonTokenStream = new CommonTokenStream(lexer);
            var parser = new AutoFilterParser(commonTokenStream);

            var templateFile = parser.sortBy();

            var visitor = new AutoParser<T>(query, query.Expression);

            var expression = visitor.Visit(templateFile);

            return (IQueryable<T>) query.Provider.CreateQuery(expression);
        }
    }
}