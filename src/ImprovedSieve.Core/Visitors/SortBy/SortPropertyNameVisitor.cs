using System.Linq;
using System.Linq.Expressions;
using ImprovedSieve.Core.Antlr;

namespace ImprovedSieve.Core.Visitors.SortBy
{
    public class SortPropertyNameVisitor<TInput> : VisitorBase<TInput>
    {
        public Expression Visit(IQueryable query, Expression expression, SieveParser.SortPropertyNameContext context, Expression item = null)
        {
            AutoParser = new AutoParser<TInput>(query, expression, item);

            var result = VisitChildren(context);

            return result;
        }
    }
}