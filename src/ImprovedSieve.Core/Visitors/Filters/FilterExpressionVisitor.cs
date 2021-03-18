using System.Linq;
using System.Linq.Expressions;
using ImprovedSieve.Core.Antlr;

namespace ImprovedSieve.Core.Visitors.Filters
{
    public class FilterExpressionVisitor<TInput> : VisitorBase<TInput>
    {
        protected override Expression AggregateResult(Expression aggregate, Expression nextResult)
        {
            if (aggregate == null)
            {
                return nextResult;
            }

            if (nextResult == null)
            {
                return aggregate;
            }

            return Expression.OrElse(aggregate, nextResult);
        }

        public Expression Visit(IQueryable query, Expression expression, SieveParser.FilterExpressionContext context, Expression item = null)
        {
            SieveParser = new SieveParser<TInput>(query, expression, item);

            return VisitChildren(context);
        }
    }
}