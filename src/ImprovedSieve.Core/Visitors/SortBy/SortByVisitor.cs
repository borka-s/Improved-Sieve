using System.Linq;
using System.Linq.Expressions;
using ImprovedSieve.Core.Antlr;

namespace ImprovedSieve.Core.Visitors.SortBy
{
    public class SortByVisitor<TInput> : VisitorBase<TInput>
    {
        public Expression Visit(IQueryable query, Expression expression, SieveParser.SortByContext context, Expression item = null)
        {
            var parameter = Expression.Parameter(typeof(TInput), "x");

            SieveParser = new SieveParser<TInput>(query, expression, parameter);

            return VisitChildren(context);
        }
    }
}