using System.Linq;
using System.Linq.Expressions;
using ImprovedSieve.Core.Antlr;

namespace ImprovedSieve.Core.Visitors.Filters
{
    public class OrExpressionVisitor<TInput> : VisitorBase<TInput>
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

            return Expression.AndAlso(aggregate, nextResult);
        }

        public Expression Visit(IQueryable query, Expression expression, SieveParser.OrExpressionContext context, Expression item = null)
        {
            SieveParser = new SieveParser<TInput>(query, expression, item);

            return VisitChildren(context);
        }
    }
}